namespace Common.Library
{

    public class CommonResponse : BaseResponse
    {
        public static CommonResponse CreateError(string message)
        {
            var response = new CommonResponse();

            response.Error(message);

            return response;
        }

       

        public static CommonResponse CreateError(string message, string messageCode, object result)
        {
            var response = new CommonResponse();

            response.Error(message, messageCode,result);

            return response;
        }

        public static CommonResponse CreateSuccess(string message, string messageCode, object result)
        {
            var response = new CommonResponse();

            response.Success(message, messageCode, result);

            return response;
        }

        public static CommonResponse CreateWarning(string message)
        {
            var response = new CommonResponse();

            response.Warning(message);

            return response;
        }

        public static T CreateError<T>(string message) where T : BaseResponse, new()
        {
            T response = new T();

            response.Error(message);

            return response;
        }

        public static T CreateError<T>(string message, string messageCode, object result) where T : BaseResponse, new()
        {
            T response = new T();

            response.Error(message, messageCode, result);

            return response;
        }

        public static T CreateSuccess<T>(string message, string messageCode, object result) where T : BaseResponse, new()
        {
            T response = new T();

            response.Success(message, messageCode, result);

            return response;
        }

        public static T CreateWarning<T>(string message) where T : BaseResponse, new()
        {
            T response = new T();

            response.Warning(message);

            return response;
        }
    }
}
