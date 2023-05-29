using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.WebApp
{
    public class Phone
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int PhoneStoreId { get; set; }
        public int ManufacturerId { get; set; }
    }

}