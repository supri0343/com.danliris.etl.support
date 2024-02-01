using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.ViewModels;

namespace UploadPB.Services.Interfaces.IPostBC41Service
{
    public interface IPostBeacukai41
    {
        Task<int> PostBeacukai(List<TemporaryViewModel> data,string Username);
    }
}
