using NewBPMSApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewBPMSApp.IServices
{
    public interface ILoginDataStore
    {
        Task<bool> LoginAsync(Login login);
    }
}
