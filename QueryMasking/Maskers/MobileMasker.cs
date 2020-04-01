using QueryMasking.Maskers.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking.Maskers
{
    public class MobileMasker : BaseMasker
    {
        public override string Mask(char[] raw, IMaskerOption option = null)
        {
            for (int i = 0; i < raw.Length; i++)
            {
                if (i >= 4 && i <= 7)
                    raw[i] = '*';
            }

            return new string(raw);
        }
    }
}
