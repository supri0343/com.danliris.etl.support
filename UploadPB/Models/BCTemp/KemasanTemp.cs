using System;
using System.Collections.Generic;
using System.Text;

namespace UploadPB.Models.BCTemp
{
    public class KemasanTemp
    {
        public KemasanTemp(string NoAju,string KodeKemasan)
        {
            this.NoAju = NoAju;
            this.KodeKemasan = KodeKemasan;
        }
        public string NoAju { get; set; }
        public string KodeKemasan { get; set; }

        
    }
}
