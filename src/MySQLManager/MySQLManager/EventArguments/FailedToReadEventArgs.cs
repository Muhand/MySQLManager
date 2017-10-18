using MySQLManager.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.EventArguments
{
    public class FailedToReadEventArgs : EventArgs
    {
        public string ErrorMessage { get; private set; }
        public FailedToRead ErrorCode { get; private set; }

        public FailedToReadEventArgs(FailedToRead errorCode)
        {
            this.ErrorCode = errorCode;
            switch (errorCode)
            {
                case FailedToRead.EmptyTableName:
                    setErrorMessage("Can not read when table name is empty");
                    break;
                case FailedToRead.ConnectionWasNotOpen:
                    setErrorMessage("Your connection was not open, you may want to Subscribe to ConnectionFailedToOpenEventArgs event to see why the connection failed to open");
                    break;
                default:
                    setErrorMessage("An unknown error has occured");
                    break;
            }
        }
        public FailedToReadEventArgs(FailedToRead errorCode, Exception ex)
        {
            this.ErrorCode = errorCode;
            switch (errorCode)
            {
                case FailedToRead.MySQLException:
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
