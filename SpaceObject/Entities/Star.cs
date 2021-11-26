using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceObjectApi.Entities
{
    [Table("stars")]
    public class Star : SpaceObject
    {
        [Column("number_of_years")]
        public string NumberOfYears { get; set; }
    }
}
