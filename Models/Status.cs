using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserRolesStatusEFCore.Models
{
    public class Status
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string StatusName { get; set; } = null!;

    }
}
