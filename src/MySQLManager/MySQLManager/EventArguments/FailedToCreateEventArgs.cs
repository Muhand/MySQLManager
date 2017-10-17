using MySQLManager.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.EventArguments
{
    public class FailedToCreateEventArgs : EventArgs
    {
        public string ErrorMessage { get; private set; }
        public FailedToCreate ErrorCode { get; private set; }

        public FailedToCreateEventArgs(FailedToCreate errorCode)
        {
            this.ErrorCode = errorCode;
            switch (errorCode)
            {
                case FailedToCreate.IncorrectLength:
                    setErrorMessage("Columns length doesn't match values length");
                    break;
                case FailedToCreate.LengthIsZero:
                    setErrorMessage("Can not insert when there is nothing to insert");
                    break;
                case FailedToCreate.ConnectionWasNotOpen:
                    setErrorMessage("Your connection was not open, you may want to Subscribe to ConnectionFailedToOpenEventArgs event to see why the connection failed to open");
                    break;
                default:
                    setErrorMessage("An unknown error has occured");
                    break;
            }
        }

        public FailedToCreateEventArgs(FailedToCreate errorCode, Exception ex)
        {
            this.ErrorCode = errorCode;
            switch (errorCode)
            {
                case FailedToCreate.MySQLException:
                    setErrorMessage(String.Format("This error occured in MYSQL with error message: {0}",ex.Message));
                    break;
                default:
                    setErrorMessage("An unknown error has occured");
                    break;
            }
        }


        private void setErrorMessage(string msg)
        {
            this.ErrorMessage = string.Format("Failed to create with error code {0}: {1}.", (int)this.ErrorCode,msg);
        }

    }
}
