using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.Enums
{
    public enum FailedToCreate
    {
        IncorrectLength = 0,         //Columnslength doesnt match values length
        LengthIsZero = 1,
        ConnectionWasNotOpen = 2,
        MySQLException = 3,
        Unknown

    }
}
