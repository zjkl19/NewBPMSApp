using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewBPMSApp.IServices
{
    /// <summary>
    /// 一般数据提取的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommonDataStore<T>
    {
        Task<T> GetItemAsync(Guid id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        Task<bool> UpdateItemAsync(T item);
    }
}
