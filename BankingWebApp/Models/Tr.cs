using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankingWebApp.Models
{
    public class Tr
    {
        [Key]
        public int TrId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TrDate { get; set; }

        public int amount { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }

        public AccountUser AccountUser { get; set; }

    }
}
