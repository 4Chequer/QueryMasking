using QueryMasking.Maskers.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking.Maskers
{
    public class EmailMasker : BaseMasker
    {
        public override string Mask(char[] raw, IMaskerOption option = null)
        {
            var rule = EmailMaskerRule.All;

            if (option is EmailMaskerOption emailOption)
            {
                rule = emailOption.Rule;
            }

            bool mask = true;

            if (rule == EmailMaskerRule.All)
            {
                for (int i = 0; i < raw.Length; i++)
                {
                    if (raw[i] == '@')
                        continue;
                    else if (raw[i] == '.')
                        mask = false;
                    else if (mask)
                        raw[i] = '*';
                }

                return new string(raw);
            }
            else
            {
                for (int i = 0; i < raw.Length; i++)
                {
                    if (raw[i] == '@')
                    {
                        int start = i / 4, length = i / 2;
                        for (int j = start; j < start + length; j++)
                        {
                            raw[j] = '*';
                        }
                        break;
                    }
                }

                return new string(raw);
            }
        }
    }
}
