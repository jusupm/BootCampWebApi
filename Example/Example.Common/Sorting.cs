﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Common
{
    public class Sorting
    {
        public string SortBy { get; set; }
        public string SortOrder { get; set; }

        public Sorting(string sortBy, string sortOrder) 
        {
            this.SortBy = sortBy;
            this.SortOrder = sortOrder;
        }
    }
}
