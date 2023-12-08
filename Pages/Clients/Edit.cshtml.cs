using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using UserRolesStatusEFCore.Data;
using UserRolesStatusEFCore.Models;

namespace UsersRolesStatus.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public List<SelectListItem> statusList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> roleList { get; set; } = new List<SelectListItem>();
        public EditModel()
        {
            try
            {
                using URSEFcontext context = new URSEFcontext();

                var sqlAllStatus = from status in context.Status
                                   select new AllStatus
                                   {
                                       id = status.Id,
                                       statusName = status.StatusName
                                   };

                foreach (var item in sqlAllStatus)
                {
                    SelectListItem itemReturn = new SelectListItem(item.statusName, "" + item.id);
                    statusList.Add(itemReturn);
                }

                var sqlAllRoles = from role in context.Roles
                                  select new AllRoles
                                  {
                                      id = role.Id,
                                      roleName = role.RoleName
                                  };

                foreach (var item in sqlAllRoles)
                {
                    SelectListItem itemReturn = new SelectListItem(item.roleName, "" + item.id);
                    roleList.Add(itemReturn);
                }

            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }
        }

        public class AllStatus
        {
            public int id;
            public string statusName;
        }
        public class AllRoles
        {
            public int id;
            public string roleName;
        }
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                using URSEFcontext context = new URSEFcontext();

                var fullList = from user in context.Users
                               join userStatus in context.UserStatus on user.Id equals userStatus.UserId
                               join userRole in context.UserRoles on user.Id equals userRole.UserId
                               join status in context.Status on userStatus.StatusId equals status.Id
                               join role in context.Roles on userRole.RoleId equals role.Id
                               where user.Id == int.Parse(id)
                               select new ClientInfo
                               {
                                   id = user.Id,
                                   name = user.UserName,
                                   email = user.Email,
                                   created_at = user.Created_at,
                                   user_status_id = userStatus.StatusId,
                                   user_role_id = userRole.RoleId,
                                   user_status = status.StatusName,
                                   user_role = role.RoleName,
                               };

                clientInfo = fullList.FirstOrDefault();

                //clientInfo
                //    {
                //        id = fullList.id,
                //        name = item.name,
                //        email = item.email,
                //        created_at = item.created_at,
                //        user_status_id = item.user_status_id,
                //        user_role_id = item.user_role_id,
                //        user_status = item.user_status,
                //        user_role = item.user_role
                //    };
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            clientInfo.id = int.Parse(Request.Form["id"]);
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.user_status_id = int.Parse(Request.Form["user_status_id"]);
            clientInfo.user_role_id = int.Parse(Request.Form["user_role_id"]);

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0)
            {
                errorMessage = "All fields are required to be filled!";
                return;
            }

            try
            {
                string id = Request.Query["id"];
                using URSEFcontext context = new URSEFcontext();

                var editUser = new User()
                {
                    Id = int.Parse(id),
                    UserName = clientInfo.name,
                    Email = clientInfo.email,
                    Created_at = DateTime.Now
                };

                context.Update(editUser);
                context.SaveChanges();

                var editUserRole = context.UserRoles.Where(p => p.UserId == int.Parse(id)).FirstOrDefault();
                editUserRole.RoleId = clientInfo.user_role_id;
                context.SaveChanges();

                var editUserStatus = context.UserStatus.Where(p => p.UserId == int.Parse(id)).FirstOrDefault();
                editUserStatus.StatusId = clientInfo.user_status_id;
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clients/ClientsIndex");
        }
    }
}
