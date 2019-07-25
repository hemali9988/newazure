using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAPI.Models
{
    [Table("Events")]
    public class EventInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]

        public string Speaker { get; set; }

        [Required]
        [DataType(DataType.Date)]

        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]

        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public string StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]

        public string EndTime { get; set; }

        [Required]
        [MinLength(3)]
        public string Host { get; set; }


    }
}
