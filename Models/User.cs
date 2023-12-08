using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace UserRolesStatusEFCore.Models
{
    public class User
    {
        public User()
        {
            Created_at = DateTime.Now;
        }
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        public DateTime Created_at { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; } = null!;
        public ICollection<UserStatus> UserStatus { get; set; } = null!;
    }
}
