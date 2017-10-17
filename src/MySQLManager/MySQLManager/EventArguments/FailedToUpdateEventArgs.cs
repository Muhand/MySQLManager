using MySQLManager.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.EventArguments
{
    public class FailedToUpdateEventArgs : EventArgs
    {
        public string ErrorMessage { get; private set; }
        public FailedToUpdate ErrorCode { get; private set; }

        public FailedToUpdateEventArgs(FailedToUpdate errorCode)
        {
            this.ErrorCode = errorCode;
            switch (errorCode)
            {
                case FailedToUpdate.LengthIsZero:
                    setErrorMessage("Can not update when there is nothing to update");
                    break;
                case FailedToUpdate.ConnectionWasNotOpen:
                    setErrorMessage("Your connection was not open, you may want to Subscribe to ConnectionFailedToOpenEventArgs event to see why the connection failed to open");
                    break;
                default:
                    setErrorMessage("An unknown error has occured");
                    break;
            }
        }

        public FailedToUpdateEventArgs(FailedToUpdate errorCode, Exception ex)
        {
            this.ErrorCode = errorCode;
            switch (errorCode)
            {
                case FailedToUpdate.MySQLException:
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
