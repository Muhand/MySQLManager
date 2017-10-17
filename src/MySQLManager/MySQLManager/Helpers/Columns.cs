using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.Helpers
{
    public struct Columns
    {
        public int length { get; private set; }
        public string[] columns { get; private set; }

        public Columns(params string[] columns)
        {
            this.length = columns.Length;
            this.columns = columns;
        }
    }
}
