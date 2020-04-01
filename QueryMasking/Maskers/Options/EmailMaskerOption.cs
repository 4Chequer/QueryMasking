using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking.Maskers.Options
{
    public enum EmailMaskerRule
    {
        All,
        BetweenName
    }

    public class EmailMaskerOption : IMaskerOption
    {
        public EmailMaskerRule Rule { get; set; }
    }
}
