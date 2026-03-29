using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Shared.Models
{
    public class PagedResult<T> : PageResultBase where T : class
    {
        public IList<T> Results { get; set; }
        public PagedResult()
        {  Results = new List<T>(); }
    }
}
