using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking
{
    public class MaskingRule
    {
        public string DataBaseName { get; set; }

        public string TableName { get; set; }

        public string FieldName { get; set; }

        public string MaskingType { get; set; }

        public override string ToString()
        {
            return $"{DataBaseName}.{TableName}.{FieldName} ({MaskingType})";
        }
    }
}
