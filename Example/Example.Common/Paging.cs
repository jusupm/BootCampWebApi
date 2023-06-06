using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Common
{
    public class Paging
    {
        const int maxPageSize = 50;
        public int pageSize = 10;
        public int PageNumber { get; set; }

        public int PageSize
        {
          get 
            { 
                return pageSize;
            }     
            set
            {
                pageSize = (value>maxPageSize) ? maxPageSize : value;
            }
        }
        public Paging() 
        {
            this.PageSize = 10;
            this.PageNumber = 1;
        }
        public Paging(int pageSize, int pageNumber)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
