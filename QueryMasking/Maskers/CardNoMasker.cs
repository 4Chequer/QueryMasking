using QueryMasking.Maskers.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking.Maskers
{
    public class CardNoMasker : BaseMasker
    {
        public override string Mask(char[] raw, IMaskerOption option = null)
        {
            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i] == ' ')
                    continue;
                else if (i > 6 && i < 14)
                    raw[i] = '*';
            }

            return new string(raw);
        }
    }
}
