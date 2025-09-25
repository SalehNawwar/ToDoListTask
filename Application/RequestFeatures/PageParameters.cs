using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestFeatures
{
    public class PageParameters
    {
        private int _pageNumber;
        public int PageNumber { 
            get => _pageNumber;
            set { _pageNumber = value < 1 ? 1 : value; } 
        }

        private const int _maxSize = 50;
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set { _pageSize = value < 1 ? 1 : value > _maxSize ? _maxSize : value; }
        }
        
    }
}
