using System;
using System.Collections.Generic;
using System.Text;

namespace UploadPB.Models.BCTemp
{
    public class EntitasTemp
    {
        public EntitasTemp(string NamaSupplier, string KodeSupplier,string Vendor,string NoAju)
        {
            this.NamaSupplier = NamaSupplier;
            this.KodeSupplier = KodeSupplier;
            this.Vendor = Vendor;
            this.NoAju = NoAju;
        }
        public string NamaSupplier { get; set; }
        public string KodeSupplier { get; set; }
        public string Vendor { get; set; }
        public string NoAju { get; set; }
    }
}
