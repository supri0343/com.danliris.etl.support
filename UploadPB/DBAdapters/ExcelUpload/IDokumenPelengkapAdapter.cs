using System;
using System.Collections.Generic;
using System.Text;
using UploadPB.Models.BCTemp;

namespace UploadPB.DBAdapters.Insert
{
    public interface IDokumenPelengkapAdapter <Tmodel>
    {
    }

    public interface IDokumenPelengkapAdapter : IBaseAdapters<DokumenPelengkapTemp>, IDokumenPelengkapAdapter<DokumenPelengkapTemp>
    {
    }
}
