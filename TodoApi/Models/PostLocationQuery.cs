using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class PostLocationQuery
    {
        [Required]
        public Location Location { get; set; }
        [Required]
        public int MaxDistance { get; set; }
        [Required]
        public int MaxResults { get; set; }
    }
}
