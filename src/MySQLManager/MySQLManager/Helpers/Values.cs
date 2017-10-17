using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.Helpers
{
    public struct Values
    {
        public int length { get; private set; }
        public string[] values { get; private set; }

        public Values(params string[] values)
        {
            this.length = values.Length;
            this.values = values;
        }
    }
}
