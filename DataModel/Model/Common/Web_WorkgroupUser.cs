using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Model
{
    [Table("Web_WorkgroupUser", Schema = "web")]
    public class Web_WorkgroupUser
    {
        [Key]
        public int WorkGroupId { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public Guid? AssignerId { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
