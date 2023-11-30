﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UploadPB.Models.AGSupport
{
    public class Beacukai_Temp
    {

        [Key]
        public long ID { get; set; }
        public string? BCId { get; set; }
        public string? BCNo { get; set; }
        public string? Barang { get; set; }
        public Decimal? Bruto { get; set; }
        public Decimal? CIF { get; set; }
        public Decimal? CIF_Rupiah { get; set; }
        public string? Keterangan { get; set; }
        public decimal? JumlahSatBarang { get; set; }
        public string? KodeBarang { get; set; }
        public string? KodeKemasan { get; set; }
        public string? NamaKemasan { get; set; }
        public Decimal? Netto { get; set; }
        public string? NoAju { get; set; }
        public string? NamaSupplier { get; set; }
        public DateTime? TglDaftarAju { get; set; }
        public DateTime? TglBCNO { get; set; }
        public string? Valuta { get; set; }
        public DateTime? Hari { get; set; }
        public string? JenisBC { get; set; }
        public long? IDHeader { get; set; }
        public string? JenisDokumen { get; set; }
        public string? NomorDokumen { get; set; }
        public DateTime? TanggalDokumen { get; set; }
        public int? JumlahBarang { get; set; }
        public string? Sat { get; set; }
        public string? KodeSupplier { get; set; }
        public DateTime? TglDatang { get; set; }
        public string? CreatedBy { get; set; }
        public string? Vendor { get; set; }
    }
        
    
}
