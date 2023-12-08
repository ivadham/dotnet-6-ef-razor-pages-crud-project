using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using UserRolesStatusEFCore.Data;
using UserRolesStatusEFCore.Models;

namespace UsersRolesStatus.Pages.Clients
{
    public class ClientsIndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();

        public void OnGet()
        {
            try
            {
                using URSEFcontext context = new URSEFcontext();

                var fullList = from user in context.Users
                               join userStatus in context.UserStatus on user.Id equals userStatus.UserId
                               join userRole in context.UserRoles on user.Id equals userRole.UserId
                               join status in context.Status on userStatus.StatusId equals status.Id
                               join role in context.Roles on userRole.RoleId equals role.Id
                               orderby user.Id descending
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

                foreach( var item in fullList )
                {
                    var clientInfo = new ClientInfo
                    {
                        id = item.id,
                        name = item.name,
                        email = item.email,
                        created_at = item.created_at,
                        user_status_id = item.user_status_id,
                        user_role_id = item.user_role_id,
                        user_status = item.user_status,
                        user_role = item.user_role,
                    };
                    listClients.Add(clientInfo);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public int id;
        public string name;
        public string email;
        public DateTime created_at;
        public int user_status_id;
        public int user_role_id;
        public string user_status;
        public string user_role;

    }
}
