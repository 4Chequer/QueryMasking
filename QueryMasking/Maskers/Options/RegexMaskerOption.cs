using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking.Maskers.Options
{
    public class RegexMaskerOption : IMaskerOption
    {
        public string Pattern { get; set; }
    }
}
