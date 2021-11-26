using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceObjectApi.Entities
{
    [Table("planets")]
    public class Planet : SpaceObject
    {
        [Column("distance_from_earth")]
        public string DistanceFromEarth { get; set; }
    }
}
