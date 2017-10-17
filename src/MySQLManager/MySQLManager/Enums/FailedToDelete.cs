using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.Enums
{
    public enum FailedToDelete
    {
        LengthIsZero = 0,
        ConnectionWasNotOpen = 1,
        MySQLException = 2
    }
}
