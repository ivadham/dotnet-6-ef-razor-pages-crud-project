using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using UserRolesStatusEFCore.Data;
using UserRolesStatusEFCore.Models;

namespace UsersRolesStatus.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public List<SelectListItem> statusList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> roleList { get; set; } = new List<SelectListItem>();
        public CreateModel()
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
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.user_status_id = int.Parse(Request.Form["user_status_id"]);
            clientInfo.user_role_id = int.Parse(Request.Form["user_role_id"]);

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0)
            {
                errorMessage = "All fields are required to be filled!";
                return;
            }


            //save the new client into the database
            try
            {
                using URSEFcontext context = new URSEFcontext();

                var newUser = new User()
                {
                    UserName = clientInfo.name,
                    Email = clientInfo.email,
                    Created_at = DateTime.Now
                };
                context.Users.Add(newUser);
                context.SaveChanges();
                int newUserId = newUser.Id;


                var newUserRole = new UserRoles()
                {
                    UserId = newUserId,
                    RoleId = clientInfo.user_role_id
                    
                };
                context.UserRoles.Add(newUserRole);
                context.SaveChanges();


                var newUserStatus = new UserStatus()
                {
                    UserId = newUserId,
                    StatusId = clientInfo.user_status_id

                };
                context.UserStatus.Add(newUserStatus);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }


            clientInfo.name = "";
            clientInfo.email = "";
            successMessage = "New client added successfully!";

            Response.Redirect("/Clients/ClientsIndex");

        }
    }
}
