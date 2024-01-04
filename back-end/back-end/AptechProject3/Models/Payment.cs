﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptechProject3.Models
{
    public class Payment
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string PaymentDate { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public Client Client { get; set; }
    }
}
