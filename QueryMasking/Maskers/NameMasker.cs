using QueryMasking.Maskers.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking.Maskers
{
    public class NameMasker : BaseMasker
    {
        public override string Mask(char[] raw, IMaskerOption option = null)
        {
            if (raw.Length == 2)
                return new string(new char[] { raw[0], '*' });
            if (raw.Length == 3)
                return new string(new char[] { raw[0], '*', raw[2] });
            return raw[0] + new string('*', raw.Length - 2) + raw[raw.Length - 1];
        }
    }
}
