using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.Enums
{
    public enum FailedToRead
    {
        ConnectionWasNotOpen = 0,
        MySQLException = 1,
        EmptyTableName = 2
    }
}
