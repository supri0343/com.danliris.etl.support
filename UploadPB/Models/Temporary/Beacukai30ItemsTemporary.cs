using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UploadPB.Models.Temporary
{
    [Table("BEACUKAI30ITEMS_TEMPORARY")]
    public class Beacukai30ItemsTemporary
    {
        [Key]
        public string DetailBCId { get; set; }
        public string BCId { get; set; }
        //[ForeignKey("BCId")]
        //public virtual Beacukai30HeaderTemporary Beacukai30HeaderTemporary { get; set; }

        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitQtyCode { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public string CurrencyCode { get; set; }
        public string CAR { get; set; }
        public string UomUnit { get; set; }
    }
}
