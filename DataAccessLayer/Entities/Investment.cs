using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Investment
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
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public int Shares { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Cost { get; set; }

        public virtual Client Client { get; set; }
        public virtual Company Company { get; set; }

        public Investment() { }
    }
}
