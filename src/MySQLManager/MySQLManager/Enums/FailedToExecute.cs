using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.Enums
{
    public enum FailedToExecute
    {
        NullQuery = 0,
        ConnectionWasNotOpen = 1,
        MySQLException = 2,
        UnknownExecutionOption = 3
    }
}
