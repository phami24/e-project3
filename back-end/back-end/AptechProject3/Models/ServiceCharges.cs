﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptechProject3.Models
{
    public class ServiceCharges
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceChargesId { get; set; }
        [Required]
        public  string ServiceChargesName { get; set; }
        [Required]
        public double Price{ get; set; }
        [Required]
        public string ServiceChargesDescription { get; set; }
    }

}
