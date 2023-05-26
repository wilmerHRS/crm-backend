using crm_backend.Models;

namespace crm_backend.Helpers
{
    public class HandleHttpError
    {
        public BaseApiResponse ApiResponse(Exception error, string message)
        {
            int status = 500;
            string title = "Internal Server Error";

            switch (error)
            {
                case AppException e:
                    status = 400;
                    title = "Bad Request";
                    break;
                case KeyNotFoundException e:
                    status = 404;
                    title = "Not Found";
                    break;
                default:
                    break;
            }

            return new BaseApiResponse
            {
                status = status,
                title = title,
                message = message,
                detail = error?.Message
            };
        }
    }
}
