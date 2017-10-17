using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.Enums
{
    public enum FailedToUpdate
    {
        LengthIsZero = 0,
        ConnectionWasNotOpen = 1,
        MySQLException = 2
    }
}
