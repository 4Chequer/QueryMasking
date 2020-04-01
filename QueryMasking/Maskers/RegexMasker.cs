using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using QueryMasking.Maskers.Options;

namespace QueryMasking.Maskers
{
    public class RegexMasker : BaseMasker
    {
        public override string Mask(char[] raw, IMaskerOption option = null)
        {
            string s = raw.ToString();
            if (option is RegexMaskerOption regexOption)
            {
                var sb = new StringBuilder(s);

                var regex = new Regex(regexOption.Pattern);

                foreach (Match m in regex.Matches(s))
                {
                    sb.Remove(m.Index, m.Length);
                    sb.Insert(m.Index, new string('*', m.Length));
                }

                return sb.ToString();
            }
            else
            {
                return s;
            }
        }
    }
}
