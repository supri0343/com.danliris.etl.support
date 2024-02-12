using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations.Schema;

namespace UploadPB.Models.Temporary.AGSupport
{
    [Table("BEACUKAI20_TEMPORARY")]
    public class Beacukai20Temporary
    {
        public long ID { get; set; }
        public string BCId { get; set; }
        public string BCNo { get; set; }
        public string Barang { get; set; }
        public decimal? Bruto { get; set; }
        public decimal? CIF { get; set; }
        public decimal? CIF_Rupiah { get; set; }
        public string Keterangan { get; set; }
        public decimal? JumlahSatBarang { get; set; }
        public int? SeriBarang { get; set; }
        public string KodeBarang { get; set; }
        public string KodeKemasan { get; set; }
        public string NamaKemasan { get; set; }
        public decimal? Netto { get; set; }
        public string NoAju { get; set; }
        public string NamaSupplier { get; set; }
        public DateTime? TglDaftarAju { get; set; }
        public DateTimeOffset? TglBCNO { get; set; }
        public string Valuta { get; set; }
        public DateTime? Hari { get; set; }
        public string JenisBC { get; set; }
        public string IDHeader { get; set; }
        public string JenisDokumen { get; set; }
        public string NomorDokumen { get; set; }
        public DateTimeOffset? TanggalDokumen { get; set; }
        public int? JumlahBarang { get; set; }
        public string Sat { get; set; }
        public string KodeSupplier { get; set; }
        public DateTime? TglDatang { get; set; }
        public string CreatedBy { get; set; }
        public string Vendor { get; set; }
    }
}
