using System.ComponentModel.DataAnnotations;

namespace DevTest_CostAccounting.Models
{
    public class TransactionModel
    {
      
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int TypeId { get; set; }
        public int Shares { get; set; }
        public decimal Rate { get; set; }
        public decimal SaleProfit { get; set; }
        public decimal SaleCostBasis { get; set; }
    }
}
