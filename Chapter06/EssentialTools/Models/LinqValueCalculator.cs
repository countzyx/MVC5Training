﻿using System.Collections.Generic;
using System.Linq;

namespace EssentialTools.Models {
    public class LinqValueCalculator {
        public decimal ValueProducts(IEnumerable<Product> products) {
            return products.Sum(prod => prod.Price);
        }
    }
}