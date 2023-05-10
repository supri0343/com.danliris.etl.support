using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UploadPB.Models.Temporary
{
    [Table("BEACUKAI30HEADER_TEMPORARY")]
    public class Beacukai30HeaderTemporary
    {
        [Key]
        public string BCId { get; set; }
        public string BCType { get; set; }
        public string BCNo { get; set; }
        public string CAR { get; set; }
        public DateTime BCDate { get; set; }
        public string ExpenditureNo { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public double Netto { get; set; }
        public double Bruto { get; set; }
        public string Pack { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Vendor { get; set; }
        //public List<Beacukai30ItemsTemporary> Items { get; set; }
    }
}
