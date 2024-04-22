using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcCreditApp.Models
{
    public class Credit
    {
        // ID кредита
        public virtual int CreditId { get; set; }

        // Название
        [DisplayName("Тип кредита")]
        [Required]
        public virtual string Head { get; set; }

        // Период, на который выдается кредит
        [DisplayName("Период кредитования")]
        public virtual int Period { get; set; }

        // Максимальная сумма кредита
        [DisplayName("Максимальная сумма")]
        [Required]
        public virtual int Sum { get; set; }

        // Процентная ставка
        [DisplayName("Ставка %")]
        [Required]
        public virtual int Procent { get; set; }
    }
}
