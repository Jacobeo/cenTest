using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AddSalesPersonToDistrictRequest
    {
        public int DistrictId { get; set; }
        public int SalesPersonId { get; set; }
        public bool IsPrimary { get; set; }
    }

}
