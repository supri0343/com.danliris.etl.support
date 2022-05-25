using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.ViewModels;

namespace UploadPB.Services.Interfaces
{
    public interface IPostBeacukai
    {
        Task PostBeacukai(List<TemporaryViewModel> data);
    }
}
