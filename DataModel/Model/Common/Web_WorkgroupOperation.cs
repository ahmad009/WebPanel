using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Model
{
    [Table("Web_WorkgroupOperation", Schema = "web")]
    public class Web_WorkgroupOperation
    {
        [Key]
        public int WorkGroupId { get; set; }
        //[Key]
        public int OperationId { get; set; }
        public bool IsActive { get; set; }
    }
}
