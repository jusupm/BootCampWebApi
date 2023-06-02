﻿using Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Common
{
    public interface IPhoneStoreService
    {
        Task<List<PhoneStore>> GetAsync();
        Task<PhoneStore> GetAsync(Guid id);
        Task<bool> PostAsync(PhoneStore store);
        Task<bool> PostAsync(string name, string address);
        Task<bool> PutAsync(Guid id, PhoneStore store);
        Task<bool> DeleteAsync(Guid id);
    }
}
