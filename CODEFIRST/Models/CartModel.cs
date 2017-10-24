using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CODEFIRST.Models
{
    public class CartModel
    {
        [Key]
        public int CartID { get; set; }
        public int Quantity { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

    }
}