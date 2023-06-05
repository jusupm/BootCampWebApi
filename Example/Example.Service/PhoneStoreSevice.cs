using Example.Common;
using Example.Model;
using Example.Repository;
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
        PhoneStoreRepository repository = new PhoneStoreRepository();
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await repository.DeleteAsync(id);
        }
        
        public async Task<List<PhoneStore>> GetAsync(Paging paging, Sorting sorting,Filtering filtering)
        {
            return await repository.GetAsync(paging,sorting,filtering);
        }

        public async Task<PhoneStore> GetAsync(Guid id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<bool> PostAsync(PhoneStore store)
        {
            return await repository.PostAsync(store);
        }

        public async Task<bool> PostAsync(string name, string address)
        {
            return await repository.PostAsync(name,address);
        }

        public async Task<bool> PutAsync(Guid id, PhoneStore store)
        {
            return await repository.PutAsync(id,store);
        }
    }
}
