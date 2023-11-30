using System;
using System.Collections.Generic;
using System.Text;

namespace UploadPB.Models
{
    public class HeaderDokumenTempModel30
    {
        public HeaderDokumenTempModel30(string BCNo, decimal? Bruto, decimal? CIF, decimal? CIF_Rupiah, decimal? Netto, string NoAju, string NamaSupplier, DateTimeOffset? TglBCNO, string Valuta, string JenisBC, int? JumlahBarang,string KodeSupplier,string Vendor, string Country)
        {
            this.BCNo = BCNo;
            this.Bruto = Bruto;
            this.CIF = CIF;
            this.CIF_Rupiah = CIF_Rupiah;
            this.Netto = Netto;
            this.NoAju = NoAju;
            this.NamaSupplier = NamaSupplier;
            this.TglBCNO = TglBCNO;
            this.Valuta = Valuta;
            this.JenisBC = JenisBC;
            this.JumlahBarang = JumlahBarang;
            this.KodeSupplier = KodeSupplier;
            this.Vendor = Vendor;
            this.Country = Country;
        }
        public string BCNo { get; set; }
        public decimal? Bruto { get; set; }
        public decimal? CIF { get; set; }
        public decimal? CIF_Rupiah { get; set; }
        public decimal? Netto { get; set; }
        public string NoAju { get; set; }
        public string NamaSupplier { get; set; }
        public DateTimeOffset? TglBCNO { get; set; }
        public string Valuta { get; set; }
        public string JenisBC { get; set; }
        public int? JumlahBarang { get; set; }
        public string KodeSupplier { get; set; }
        public string Vendor { get; set; }
        public string Country { get; set; }

    }
}
