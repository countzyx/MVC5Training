using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain.Entities {
    public class Cart {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity) {
            var line = lineCollection
                .Where(prod => prod.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (line == null) {
                lineCollection.Add(new CartLine {
                    Product = product,
                    Quantity = quantity
                });
            } else {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product) {
            lineCollection.RemoveAll(line => line.Product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalValue() {
            return lineCollection.Sum(line => line.Product.Price * line.Quantity);
        }

        public void Clear() {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines {
            get { return lineCollection; }
        }
    }
}
