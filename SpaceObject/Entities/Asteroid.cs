using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceObjectApi.Entities
{
    [Table("asteroids")]
    public class Asteroid : SpaceObject
    {
        [Column("weight")]
        public string Weight { get; set; }
    }
}
