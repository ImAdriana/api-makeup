using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_MakeupCRUD.Models
{
    public class MakeupProduct
    {
        [Key] // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Type { get; set; }
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Volume { get; set; }

        public int BrandID { get; set; }
        [ForeignKey("BrandID")]
        public virtual Brand Brand { get; set; }

    }
}
