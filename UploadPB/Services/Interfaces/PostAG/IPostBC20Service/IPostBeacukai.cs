using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.ViewModels;

namespace UploadPB.Services.Interfaces.IPostBC20Service.PostAG
{
    public interface IPostBeacukai20
    {
        Task<int> PostBeacukai(List<TemporaryViewModel> data,string Username);
    }
}
