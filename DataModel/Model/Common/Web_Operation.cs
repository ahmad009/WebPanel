using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Model
{
    [Table("Web_Operation", Schema = "web")]
    public class Web_Operation
    {
        [Key]
        public int OperationId { get; set; }
        public string? Title { get; set; }
        public int ApplicationId { get; set; }
        public int CategoryId { get; set; }
        public string? Area { get; set; }
        public string? Controller { get; set; }
        public string? ActionName { get; set; }
        public string? OperationKey { get; set; }
        public bool HasUI { get; set; }
        public bool IsTechniker { get; set; }
        public bool HasParameter { get; set; }
    }
}
