using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceObjectApi.Entities
{
    [Table("space_objects")]
    public abstract class SpaceObject
    {
        [Column("id"), JsonIgnore]
        public int Id { get; set; }

        [Column("name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Maximum 50 characters")]
        public string Name { get; set; }

        [Column("description")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Maximum 1000 characters")]
        public string Description { get; set; }
    }
}
