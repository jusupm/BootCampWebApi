using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Common
{
    public class Filtering
    {
        public string FilterString { get; set; }
        public char? FirstLetter { get; set; }
        public string FilterAddress { get; set; }

        public Filtering(string filterString, char? firstLetter, string filterAddress) {
            FilterString = filterString;
            FirstLetter = firstLetter;
            FilterAddress = filterAddress;
        }
    }
}
