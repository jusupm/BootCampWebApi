using Example.Common;
using Example.Model;
using Example.Repository.Common;
using Example.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service
{
    public class PhoneStoreService : IPhoneStoreService
    {
        private IPhoneStoreRepository Repository { get; }
        public PhoneStoreService(IPhoneStoreRepository repository)
        {
            Repository = repository;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync(id);
        }
        
        public async Task<PagedList<PhoneStore>> GetAsync(Paging paging, Sorting sorting,Filtering filtering)
        {
            return await Repository.GetAsync(paging,sorting,filtering);
        }

        public async Task<PhoneStore> GetAsync(Guid id)
        {
            return await Repository.GetAsync(id);
        }

        public async Task<bool> PostAsync(PhoneStore store)
        {
            return await Repository.PostAsync(store);
        }

        public async Task<bool> PostAsync(string name, string address)
        {
            return await Repository.PostAsync(name,address);
        }

        public async Task<bool> PutAsync(Guid id, PhoneStore store)
        {
            return await Repository.PutAsync(id,store);
        }
    }
}
