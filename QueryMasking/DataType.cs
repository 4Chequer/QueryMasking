using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking
{
    public enum DataType
    {
        Name = 1,
        RegNo = 2,
        Mobile = 4,
        Phone = 8,
        Email = 16,
        CardNo = 32,
        Driver = 64,
        Passport = 128,
        None = 256,
    }
}
