using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Api.Messages
{
    public class Responce
    {
        public bool IsSuccses { get; set; }
        public ErrorMessage? Error { get; set; }
        public object Result { get; set; }
        public Enum ErrorType { get; set; }

        public static Responce CreateSuccesResponce(object result = null)
        {
            return new Responce
            {
                IsSuccses = true,
                Result = result
            };
        }
        public static Responce CreateFailResponce(string message, Enum errorType)
        {
            return new Responce
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
