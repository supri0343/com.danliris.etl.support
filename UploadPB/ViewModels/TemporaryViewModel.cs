using System;
using System.Collections.Generic;
using System.Text;

namespace UploadPB.ViewModels
{
    public class TemporaryViewModel
    {
        public int ID { get; set; }
        public string BCId { get; set; }
        public string BCNo { get; set; }
        public string Barang { get; set; }
        public double? Bruto { get; set; }
        public double? CIF { get; set; }
        public double? CIF_Rupiah { get; set; }
        public string Keterangan { get; set; }
        public double? JumlahSatBarang { get; set; }
        public string KodeBarang { get; set; }
        public string KodeKemasan { get; set; }
        public string NamaKemasan { get; set; }
        public double? Netto { get; set; }
        public string NoAju { get; set; }
        public string NamaSupplier { get; set; }
        public DateTime? TglDaftarAju { get; set; }
        public DateTime? TglBCNO { get; set; }
        public string Valuta { get; set; }
        public DateTime? Hari { get; set; }
        public string JenisBC { get; set; }
        public string IDHeader { get; set; }
        public string JenisDokumen { get; set; }
        public string NomorDokumen { get; set; }
        public DateTime? TanggalDokumen { get; set; }
        public double? JumlahBarang { get; set; }
        public string Sat { get; set; }
        public string KodeSupplier { get; set; }
        public DateTime? TglDatang { get; set; }
        public string CreatedBy { get; set; }
        public string Vendor { get; set; }

    }
}
