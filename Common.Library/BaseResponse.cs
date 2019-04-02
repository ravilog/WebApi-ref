
namespace Common.Library
{
    using System.Collections.Generic;
    using System.Text;

    public enum ResponseTypes
    {
        Success = 0,
        Warning = 1,
        Error = 2
    }
    public class BaseResponse
    {
        private string successMessage;
        private string warningMessage;
        private CommmonMessage commonMessages; 

        public BaseResponse()
        {
            this.commonMessages = new CommmonMessage();
        }

        public CommmonMessage Response 
        {
            get 
            {
                return this.commonMessages;
            }

            private set { }
        }

       

        //public ResponseTypes ResponseType
        //{
        //    get
        //    {
        //        //if (this.IsOk)
        //        //{
        //        //    if (!string.IsNullOrWhiteSpace(this.successMessage))
        //        //    {
        //        //        return ResponseTypes.Success;
        //        //    }
        //        //    else if (!string.IsNullOrWhiteSpace(this.warningMessage))
        //        //    {
        //        //        return ResponseTypes.Warning;
        //        //    }
        //        //}

        //        return ResponseTypes.Success;
        //    }

        //    private set
        //    {
        //    }
        //}

        public void Error(string message)
        {
            this.commonMessages = new CommmonMessage { messageCode = string.Empty, message = message };
        }

        public void Error(string message, string messageCode, object result) 
        {
            this.commonMessages = new CommmonMessage {status=false, messageCode = messageCode, message = message, result=result };
        }

        public void Success(string message, string messageCode, object result)
        {
            this.commonMessages= new CommmonMessage { status = true, messageCode = messageCode, message = message, result = result };
        }

        public void Warning(string message)
        {
            this.warningMessage = message;
        }

    }
}
