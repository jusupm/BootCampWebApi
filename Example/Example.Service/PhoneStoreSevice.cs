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
        public bool Delete(Guid id)
        {
            return repository.Delete(id);
        }

        public List<PhoneStore> Get()
        {
            return repository.Get();
        }

        public PhoneStore Get(Guid id)
        {
            return repository.Get(id);
        }

        public bool Post(PhoneStore store)
        {
            return repository.Post(store);
        }

        public bool Post(string name, string address)
        {
            return repository.Post(name,address);
        }

        public bool Put(Guid id, PhoneStore store)
        {
            return repository.Put(id,store);
        }
    }
}
