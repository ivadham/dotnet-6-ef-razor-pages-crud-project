using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserRolesStatusEFCore.Models
{
    public class UserStatus
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public User User { get; set; }
        public Status Status { get; set; }
    }
}
