using System.Collections.Generic;
using System.Linq;

namespace EssentialTools.Models {
    public class LinqValueCalculator : IValueCalculator {
        private IDiscountHelper discounter;

        public LinqValueCalculator(IDiscountHelper discountParam) {
            discounter = discountParam;
        }

        public decimal ValueProducts(IEnumerable<Product> products) {
            return products.Sum(prod => prod.Price);
        }
    }
}