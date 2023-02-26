using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Models
{
    public class CarModel
    {

        [Key]
        public int Id { get; set; }
        
        public string Model { get; set; }
        public string Status { get; set; }

       
    }
}
