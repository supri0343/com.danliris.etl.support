using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.ViewModels;

namespace UploadPB.Services.Interfaces.IPostBC262Service
{
    public interface IPostBeacukai262
    {
        Task<int> PostBeacukai(List<TemporaryViewModel> data, string Username);
    }
}
