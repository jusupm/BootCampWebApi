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
        List<PhoneStore> Get();
        PhoneStore Get(Guid id);
        bool Post(PhoneStore store);
        bool Post(string name, string address);
        bool Put(Guid id, PhoneStore store);
        bool Delete(Guid id);
    }
}
