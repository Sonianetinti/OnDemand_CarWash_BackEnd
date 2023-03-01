using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Models
{
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }
        public string  WashingInstructions { get; set; }
        public DateTime Date { get; set; }
        public string status { get; set; }
        public string packageName { get; set; }
        public string description { get; set; }
        public double price{ get; set; }
        public string city{ get; set; }
        public string pincode{ get; set; }




    }
}
