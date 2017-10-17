using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.Helpers
{
    public struct AssignmentList
    {
        public int Length { get; private set; }
        public Field[] Fields { get; private set; }

        public AssignmentList(params Field[] fields)
        {
            this.Length = fields.Length;
            this.Fields = fields;
        }
    }
}
