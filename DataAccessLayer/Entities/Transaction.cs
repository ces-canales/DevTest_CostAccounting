using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Required]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("TransactionTypes")]
        public int TypeId { get; set; }

        [Required]
        public int Shares { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Rate { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal SaleProfit { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal SaleCostBasis { get;set; }

        public virtual Client Client { get; set; }
        public virtual Company Company { get; set; }
    }

    public class TransactionType
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

}
