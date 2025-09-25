using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestFeatures
{
    public class RequestParameters
    {
        public const int _maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > _maxPageSize ? _maxPageSize : value); }
        }
    }
}
