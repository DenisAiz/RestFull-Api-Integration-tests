using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceObjectApi.Entities
{
    [Table("black_holes")]
    public class BlackHole : SpaceObject
    {
        [Column("diameter")]
        public string Diameter { get; set; }
    }
}
