using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.ViewModels;

namespace UploadPB.Services.Interfaces.IPostBC33Service
{
    public interface IPostBeacukai33
    {
        Task<int> PostBeacukai(List<TemporaryViewModel> data, string Username);
    }
}
