using System;
using System.Collections.Generic;
using System.Text;

namespace UploadPB.Models.BCTemp
{
    public class BarangTemp
    {
        public BarangTemp(string noaju, string barang, double? jumlahsatbarang, string kodebarang,string sat)
        {
            NoAju = noaju;
            Barang = barang;
            JumlahSatBarang = jumlahsatbarang;
            KodeBarang = kodebarang;
            Sat = sat;
        }
        public string NoAju { get; set; }
        public string Barang { get; set; }
        public double? JumlahSatBarang { get; set; }
        public string KodeBarang { get; set; }
        public string Sat { get; set; }
    }
}
