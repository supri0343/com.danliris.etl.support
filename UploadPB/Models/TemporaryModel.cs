using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace UploadPB.Models
{
    public class TemporaryModel 
    {
        public TemporaryModel(string bcNo, string barang, double bruto, double cif, double cif_rupiah, string keterangan, double jumlahSatBarang, string kodeBarang, string kodeKemasan, string namaKemasan, double netto, string noAju, string namaSupplier, DateTime tglDaftarAju, DateTime tglBCNo, string valluta, DateTime hari, string jenisBC, string IDheader, string jenisDokumen, string nomorDokumen, DateTime tanggalDokumen, double jumlahBarang, string sat,string kodeSupplier,DateTime tglDatang, string CreateBy) 
        {
            BCNo = bcNo;
            Barang = barang;
            Bruto = bruto;
            CIF = cif;
            CIF_Rupiah = cif_rupiah;
            Keterangan = keterangan;
            JumlahSatBarang = jumlahSatBarang;
            KodeBarang = kodeBarang;
            KodeKemasan = kodeKemasan;
            NamaKemasan = namaKemasan;
            Netto = netto;
            NoAju = noAju;
            NamaSupplier = namaSupplier;
            TglDaftarAju = tglDaftarAju;
            TglBCNO = tglBCNo;
            Valutta = valluta;
            Hari = hari;
            JenisBC = jenisBC;
            IDHeader = IDheader;
            JenisDokumen = jenisDokumen;
            NomorDokumen = nomorDokumen;
            TanggalDokumen = tanggalDokumen;
            JumlahBarang = jumlahBarang;
            Sat = sat;
            KodeSupplier = kodeSupplier;
            TglDatang = tglDatang;
            CreatedBy = CreateBy;

        }

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
        public string Valutta { get; set; }
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

    }
}
