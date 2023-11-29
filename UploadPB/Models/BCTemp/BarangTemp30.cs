using System;
using System.Collections.Generic;
using System.Text;

namespace UploadPB.Models.BCTemp
{
    public class BarangTemp30
    {
        public BarangTemp30(string noaju, string barang, decimal? jumlahsatbarang, string kodebarang,string sat,decimal? cif_rupiah,string pack, decimal? netto, decimal? bruto)
        {
            NoAju = noaju;
            Barang = barang;
            JumlahSatBarang = jumlahsatbarang;
            KodeBarang = kodebarang;
            Sat = sat;
            CIF_Rupiah = cif_rupiah;
            Pack = pack;
            Netto = netto;
            Bruto = bruto;
        }
        public string NoAju { get; set; }
        public string Barang { get; set; }
        public decimal? JumlahSatBarang { get; set; }
        public string KodeBarang { get; set; }
        public string Sat { get; set; }
        public decimal? CIF_Rupiah { get; set; }
        public string Pack { get; set; }
        public decimal? Netto { get; set; }
        public decimal? Bruto { get; set; }
    }
}
