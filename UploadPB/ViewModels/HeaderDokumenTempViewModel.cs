using System;
using System.Collections.Generic;
using System.Text;

namespace UploadPB.ViewModels
{
    public class HeaderDokumenTempViewModel
    {
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
    }
}
