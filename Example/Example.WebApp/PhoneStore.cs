using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.WebApp
{
    public class PhoneStore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Phone> Phones { get; set; }
    }

}