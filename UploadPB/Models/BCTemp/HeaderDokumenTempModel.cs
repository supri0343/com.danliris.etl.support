using System;
using System.Collections.Generic;
using System.Text;

namespace UploadPB.Models
{
    public class HeaderDokumenTempModel
    {
        public HeaderDokumenTempModel(string BCNo, double? Bruto, double? CIF,double? CIF_Rupiah, double? Netto, string NoAju, string NamaSupplier, DateTime? TglBCNO, string Valuta, string JenisBC, double? JumlahBarang,string KodeSupplier,string Vendor)
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

        }
        public string BCNo { get; set; }
        public double? Bruto { get; set; }
        public double? CIF { get; set; }
        public double? CIF_Rupiah { get; set; }
        public double? Netto { get; set; }
        public string NoAju { get; set; }
        public string NamaSupplier { get; set; }
        public DateTime? TglBCNO { get; set; }
        public string Valuta { get; set; }
        public string JenisBC { get; set; }
        public double? JumlahBarang { get; set; }
        public string KodeSupplier { get; set; }
        public string Vendor { get; set; }


    }
}
