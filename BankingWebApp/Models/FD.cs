using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankingWebApp.Models
{
    public class FD
    {
        [Key]
        public int FdId { get; set; }
        [Display(Name ="Investing Money")]
        [Range(1000,10000000),Required(ErrorMessage ="Enter the amount as per our bank instructions")]
        public double FdInvMon { get; set; }

        [Display(Name ="Tenure Period(In Months only)")]
        [Range(1,1200),Required(ErrorMessage ="You can create fd from 1 month to max 10 years")]
        public double Month { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }

        public AccountUser AccountUser { get; set; }
        [Display(Name ="Maturity Amount ")]
        public Double FdMAmount { get; set; }
        [Display(Name ="Interested Amount")]
        public Double FdInMoney { get; set; }
    }
}
