using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.Models;
using UploadPB.ViewModels;
using UploadPB.DBAdapters;

namespace UploadPB.DBAdapters.BeacukaiTemp
{
    public interface IBeacukaiTemp <TViewModel>
    {
    }
    public interface IBeacukaiTemp : IBaseAdapters<TemporaryViewModel>, IBeacukaiTemp<TemporaryViewModel>
    {
    }

}
