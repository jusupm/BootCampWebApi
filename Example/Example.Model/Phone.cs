using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Model
{
    internal class Phone
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int PhoneStoreId { get; set; }
        public int ManufacturerId { get; set; }
    }
}
