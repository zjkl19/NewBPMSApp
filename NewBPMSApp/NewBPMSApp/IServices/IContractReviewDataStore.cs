using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewBPMSApp.IServices
{
    /// <summary>
    /// 合同审核数据提取的接口
    /// </summary>
    public interface IContractReviewDataStore<T1,T2>
        where T1:class
        where T2:class
    {
        //Task<T1> GetItemAsync(Guid id);
        Task<IEnumerable<T1>> GetItemsAsync(bool forceRefresh = false);

        Task<bool> UpdateItemAsync(T2 item);
    }
}
