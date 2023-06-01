using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example;
using Example.Model.Common;

namespace Example.Model
{
    public class PhoneStore : IPhoneStore
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
