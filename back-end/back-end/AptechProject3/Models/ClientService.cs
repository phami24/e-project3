﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptechProject3.Models
{
    public class ClientService
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientServiceId { get; set; }
        [Required]
        public Client Client { get; set; }
        [Required]
        public Service Service { get; set; }
        [Required]
        public string Status { get; set; }
        public string StartDay { get; set; }
        public string ExpiredDay { get; set; }
    }
}
