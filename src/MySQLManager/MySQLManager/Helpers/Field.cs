using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.Helpers
{
    public struct Field
    {
        public string FieldName { get; private set; }
        public string FieldValue { get; private set; }

        public string Formatted { get; private set; }

        public Field(string fieldName, string fieldValue)
        {
            this.FieldName = fieldName;
            this.FieldValue = fieldValue;
            this.Formatted = String.Format("{0}='{1}'",fieldName,fieldValue);
        }
    }
}
