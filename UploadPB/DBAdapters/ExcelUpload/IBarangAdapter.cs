using System;
using System.Collections.Generic;
using System.Text;
using UploadPB.Models.BCTemp;

namespace UploadPB.DBAdapters.Insert
{
    public interface IBarangAdapter <Tmodel>
    {
    }
    public interface IBarangAdapter : IBaseAdapters<BarangTemp>, IBarangAdapter<BarangTemp>
    {
    }
}
