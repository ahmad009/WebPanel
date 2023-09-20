using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Model
{
    [Table("Web_MenuItem", Schema = "web")]
    public class Web_MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }
        public string? Title { get; set; }
        public int? OperationId { get; set; }
        public Guid? ImageId { get; set; }
        public int? ParentMenuItemId { get; set; }
        public bool? IsLeaf { get; set; }
        public int? OrderNo { get; set; }
        public int? MenuItemTypeId { get; set; }
        public string? Url { get; set; }
        public bool IsActive { get; set; }
    }
}
