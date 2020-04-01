using QueryMasking.Maskers.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking.Maskers
{
    public class RegNoMasker : BaseMasker
    {
        public override string Mask(char[] raw, IMaskerOption option = null)
        {
            bool withGender = false;

            if (option is RegNoMaskerOption regNoOption)
            {
                withGender = regNoOption.WithGender;
            }

            bool mask = false;

            for (int i = 0; i < raw.Length; i++)
            {
                if (mask)
                    raw[i] = '*';
                else if (withGender && i == 7)
                    mask = true;
                else if (!withGender && i == 6)
                    mask = true;
            }

            return new string(raw);
        }
    }
}
