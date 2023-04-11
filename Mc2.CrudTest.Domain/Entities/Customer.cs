using Mc2.CrudTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mc2.CrudTest.Domain.Entities
{
    
    public class Customer:AuditableWithBaseEntity<int>
    {
       
        [Required]
        [StringLength(500)]
        public string FirstName { get; set; } 

        [Required]
        [StringLength(500)]
        public string LastName { get; set; } 

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string DateOfBirth { get; set; } 

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string? Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? BankAccountNumber { get; set; }
    }
}
