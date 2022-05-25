using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UploadPB
{
    public interface IBaseAdapters<TModel>
    {
        Task Insert(IEnumerable<TModel> model);
        // Task Delete(TModel model);
        // Task Update(TModel model);
        Task DeleteBulk();
        //Task InsertBulk(IEnumerable<TModel> models);
    }
}
