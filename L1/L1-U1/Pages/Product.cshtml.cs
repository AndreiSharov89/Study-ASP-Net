using L1_U3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace L1_U3.Pages
{
    public class ProductModel : PageModel
    {
        public string? MessageRezult { get; private set; }
        public Product Product { get; set; }
        public int amount { get; set; }
        public void OnGet()
        {
            MessageRezult = "Для товара можно определить скидку с учетом опта";
        }
        public void OnPostOpt(string name, decimal? price, int amount)
        {
            Product = new Product();
            if (price == null || price < 0 || string.IsNullOrEmpty(name) || amount <= 0)
            {
                MessageRezult = "Переданы некорректные данные. Повторите ввод";
                return;
            }
            double discount = 0.05;
            if (amount > 9) { discount += 0.05; }
            if (amount > 19) { discount += 0.05; }
            if (amount > 49) { discount += 0.05; }
            if (amount > 99) { discount += 0.05; }
            if (amount > 199) { discount += 0.05; }
            if (amount > 499) { discount += 0.05; }
            if (amount > 999) { discount += 0.05; }
            if (amount > 1999) { discount += 0.05; }

            double result = (double)price * (1-discount);
            MessageRezult = $"Для товара {name} с ценой {price} цена со скидкой получится {result:F2}; при покупке {amount} шт. скидка составляет {(discount*100):F2}%";
            Product.Price = price;
            Product.Name = name;
        }
        /*public void OnPostDiscont(string name, decimal? price, double discont)
        {
            Product = new Product();
            var result = price - price * (decimal?)discont / 100;
            MessageRezult = $"Для товара {name} с ценой {price} и скидкой {discont} получится { result}";
            Product.Price = price;
            Product.Name = name;
        }*/
    }
}
