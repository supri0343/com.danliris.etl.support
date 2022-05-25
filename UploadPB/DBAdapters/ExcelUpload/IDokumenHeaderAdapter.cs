using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.Models;

namespace UploadPB.DBAdapters
{
    public interface IDokumenHeaderAdapter<TModel>
    {
         
    }
    public interface IDokumenHeaderAdapter : IBaseAdapters<HeaderDokumenTempModel>, IDokumenHeaderAdapter<HeaderDokumenTempModel>
    {
    }
}
