using MySQLManager.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.EventArguments
{
    public class FailedToDeleteEventArgs : EventArgs
    {
        public string ErrorMessage { get; private set; }
        public FailedToDelete ErrorCode { get; private set; }

        public FailedToDeleteEventArgs(FailedToDelete errorCode)
        {
            this.ErrorCode = errorCode;
            switch (errorCode)
            {
                case FailedToDelete.LengthIsZero:
                    setErrorMessage("Can not delete when there is no condition");
                    break;
                case FailedToDelete.ConnectionWasNotOpen:
                    setErrorMessage("Your connection was not open, you may want to Subscribe to ConnectionFailedToOpenEventArgs event to see why the connection failed to open");
                    break;
                default:
                    setErrorMessage("An unknown error has occured");
                    break;
            }
        }

        public FailedToDeleteEventArgs(FailedToDelete errorCode, Exception ex)
        {
            this.ErrorCode = errorCode;
            switch (errorCode)
            {
                case FailedToDelete.MySQLException:
                    setErrorMessage(String.Format("This error occured in MYSQL with error message: {0}", ex.Message));
                    break;
                default:
                    setErrorMessage("An unknown error has occured");
                    break;
            }
        }

        private void setErrorMessage(string msg)
        {
            this.ErrorMessage = string.Format("Failed to create with error code {0}: {1}.", (int)this.ErrorCode, msg);
        }
    }
}
