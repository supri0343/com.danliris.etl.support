using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.ViewModels;

namespace UploadPB.Services.Interfaces.Post40
{
    public interface IPostBeacukai
    {
        Task<int> PostBeacukai(List<TemporaryViewModel> data,string Username);
    }
}
