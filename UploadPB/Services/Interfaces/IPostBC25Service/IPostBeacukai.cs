using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.ViewModels;

namespace UploadPB.Services.Interfaces.IPostBC25Service
{
    public interface IPostBeacukai25
    {
        Task<string> PostBeacukai(List<TemporaryViewModel> data,string Username);
    }
}
