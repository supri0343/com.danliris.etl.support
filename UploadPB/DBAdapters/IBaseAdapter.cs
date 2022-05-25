using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UploadPB
{
    public interface IBaseAdapter<TModel>
    {
        //Task Insert(TModel model);
        // Task Delete(TModel model);
        // Task Update(TModel model);
        //Task DeleteByMonthAndYear(DateTime periode);
        Task InsertBulk(IEnumerable<TModel> models);
    }
}
