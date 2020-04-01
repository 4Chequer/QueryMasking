using QueryMasking.Extension;
using QueryMasking.Maskers;
using QueryMasking.Maskers.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking
{
    public static class MaskingTool
    {
        public static List<MaskingRule> MaskingRules = new List<MaskingRule>();
        public static Dictionary<string, BaseMasker> Maskers = new Dictionary<string, BaseMasker>();
        public static Dictionary<Type, IMaskerOption> Options = new Dictionary<Type, IMaskerOption>();
        static MaskingTool()
        {
            Maskers.Add("cardno", new CardNoMasker());
            Maskers.Add("driver", new DriverMasker());
            Maskers.Add("email", new EmailMasker());
            Maskers.Add("mobile", new MobileMasker());
            Maskers.Add("phone", new MobileMasker());
            Maskers.Add("name", new NameMasker());
            Maskers.Add("passport", new PassportMasker());
            Maskers.Add("regno", new RegNoMasker());

            Options[typeof(EmailMasker)] = new EmailMaskerOption()
            {
                Rule = EmailMaskerRule.BetweenName,
            };

            Options[typeof(RegNoMasker)] = new RegNoMaskerOption()
            {
                WithGender = false,
            };
        }

        public static string GetMaskingType(string database, string table, string column)
        {
            for (int i = 0; i < MaskingRules.Count; i++)
            {
                var rule = MaskingRules[i];

                bool dbMatch = rule.DataBaseName == database;
                bool tableMatch = rule.TableName == table;
                bool fieldMatch = rule.FieldName == column;

                if (dbMatch && tableMatch && fieldMatch)
                    return rule.MaskingType;
            }

            return "";
        }

        public static string Mask(string data, string database = "", string table = "", string fieldName = "")
        {
            var maskingType = GetMaskingType(database, table, fieldName);

            if (maskingType == "" || !Maskers.ContainsKey(maskingType))
                return data;

            BaseMasker masker = Maskers[maskingType];

            IMaskerOption option;

            if (!Options.TryGetValue(masker.GetType(), out option))
                option = null;

            try
            {
                return masker.Mask(data, option);
            }
            catch (Exception)
            {
                return data;
            }
        }
    }
}
