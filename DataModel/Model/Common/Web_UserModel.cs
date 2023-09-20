using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Model
{
    [Table("Web_User", Schema = "web")]
    public class Web_UserModel
    {
        [Key]
        public System.Guid Id { get; set; }
        public string AccountName { get; set; }
        public string? DomainName { get; set; }
        public string? DisplayName { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
