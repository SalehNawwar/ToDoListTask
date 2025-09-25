using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestFeatures
{
    public class ToDoItemRequestParameters:RequestParameters
    {
        public int? AssignedUserId { get; set; }
        public int? CreatedByUserId { get; set; }
        
    }
}
