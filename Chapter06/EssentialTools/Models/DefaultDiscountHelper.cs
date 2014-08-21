using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models {
    public class DefaultDiscountHelper : IDiscountHelper {
        public decimal DiscountSize { get; set; }

        public decimal ApplyDiscount(decimal totalParam) {
            return (totalParam - (DiscountSize / 100m * totalParam));
        }
    }
}