﻿using System.ComponentModel.DataAnnotations;

namespace SC.Web.Models
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double CartTotal { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Phones { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
