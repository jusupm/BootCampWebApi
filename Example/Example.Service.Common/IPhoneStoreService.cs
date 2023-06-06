using Example.Common;
using Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Common
{
    public interface IPhoneStoreService
    {
        Task<PagedList<PhoneStore>> GetAsync(Paging paging, Sorting sorting, Filtering filtering);
        Task<PhoneStore> GetAsync(Guid id);
        Task<bool> PostAsync(PhoneStore store);
        Task<bool> PostAsync(string name, string address);
        Task<bool> PutAsync(Guid id, PhoneStore store);
        Task<bool> DeleteAsync(Guid id);
    }
}
