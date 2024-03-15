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
            MessageRezult = "��� ������ ����� ���������� ������ � ������ ����";
        }
        public void OnPostOpt(string name, decimal? price, int amount)
        {
            Product = new Product();
            if (price == null || price < 0 || string.IsNullOrEmpty(name) || amount <= 0)
            {
                MessageRezult = "�������� ������������ ������. ��������� ����";
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
            MessageRezult = $"��� ������ {name} � ����� {price} ���� �� ������� ��������� {result:F2}; ��� ������� {amount} ��. ������ ���������� {(discount*100):F2}%";
            Product.Price = price;
            Product.Name = name;
        }
        /*public void OnPostDiscont(string name, decimal? price, double discont)
        {
            Product = new Product();
            var result = price - price * (decimal?)discont / 100;
            MessageRezult = $"��� ������ {name} � ����� {price} � ������� {discont} ��������� { result}";
            Product.Price = price;
            Product.Name = name;
        }*/
    }
}
