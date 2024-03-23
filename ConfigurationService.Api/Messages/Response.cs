using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Api.Messages
{
    public class Response
    {
        public bool IsSuccses { get; set; }
        public ErrorMessage? Error { get; set; }
        public object Result { get; set; }
        public Enum ErrorType { get; set; }

        public static Response CreateSuccesResponce(object result = null)
        {
            return new Response
            {
                IsSuccses = true,
                Result = result
            };
        }
        public static Response CreateFailResponce(string message, Enum errorType)
        {
            return new Response
            {
                IsSuccses = true,
                Error = new ErrorMessage()
                {
                    Message = message
                },
                ErrorType = errorType

            };
        }
    }
}
