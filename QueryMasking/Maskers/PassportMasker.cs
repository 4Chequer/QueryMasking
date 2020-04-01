using QueryMasking.Maskers.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking.Maskers
{
    public class PassportMasker : BaseMasker
    {
        public override string Mask(char[] raw, IMaskerOption option = null)
        {
            for (int i = 0; i < raw.Length; i++)
            {
                if (i < 1)
                    continue;

                raw[i] = '*';
            }

            return new string(raw);
        }
    }
}
