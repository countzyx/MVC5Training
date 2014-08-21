using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models {
    public interface IDiscountHelper {
        decimal ApplyDiscount(decimal totalParam);
    }
}