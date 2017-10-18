using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.EventArguments
{
    public class FailedToExecuteCustomQueryEventArgs : EventArgs
    {
        public string ErrorMessage { get; private set; }
        public Enums.FailedToExecute ErrorCode { get; private set; }

        public FailedToExecuteCustomQueryEventArgs(Enums.FailedToExecute errorCode)
        {
            this.ErrorCode = errorCode;

            switch (errorCode)
            {
                case Enums.FailedToExecute.NullQuery:
                    setErrorMessage("Your query is empty");
                    break;
                case Enums.FailedToExecute.ConnectionWasNotOpen:
                    setErrorMessage("Your connection was not open, you may want to Subscribe to ConnectionFailedToOpenEventArgs event to see why the connection failed to open");
                    break;
                case Enums.FailedToExecute.UnknownExecutionOption:
                    setErrorMessage("You have chose an unknown execution option, please verify known ExecutionOptions");
                    break;
                default:
                    setErrorMessage("An unknown error has occured");
                    break;
            }

        }
        public FailedToExecuteCustomQueryEventArgs(Enums.FailedToExecute errorCode, Exception ex)
        {
            this.ErrorCode = errorCode;
            switch (errorCode)
            {
                case Enums.FailedToExecute.MySQLException:
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
