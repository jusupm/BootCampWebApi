using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Model.Common
{
    public interface IPhoneStore
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Address { get; set; }
    }
}
