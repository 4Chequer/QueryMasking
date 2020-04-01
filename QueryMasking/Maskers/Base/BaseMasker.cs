using QueryMasking.Maskers.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking.Maskers
{
    public abstract class BaseMasker
    {
        public string Mask(StringBuilder raw, IMaskerOption option = null) => Mask(raw.ToString(), option);

        public string Mask(string raw, IMaskerOption option = null) => Mask(raw.ToCharArray(), option);

        public abstract string Mask(char[] raw, IMaskerOption option = null);
    }
}
