﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core.Requests
{
    public record DeleteSalesPersonRequest(int DistrictId, int SalesPersonId);
}
