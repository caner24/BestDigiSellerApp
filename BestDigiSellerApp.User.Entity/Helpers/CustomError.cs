using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Helpers
{
    public sealed record CustomError(string Code, string Message)
    {
        private static readonly string RecordNotFoundCode = "RecordNotFound";
        private static readonly string ValidationErrorCode = "ValidationError";
        private static readonly string WrongTwoStepCode = "TwoStepError";

        public static readonly CustomError None = new(string.Empty, string.Empty);
        public static CustomError RecordNotFound(string message)
        {
            return new CustomError(RecordNotFoundCode, message);
        }
        public static CustomError WrongTwoStep(string message)
        {
            return new CustomError(RecordNotFoundCode, message);
        }
        public static CustomError ValidationError(string message)
        {
            return new CustomError(ValidationErrorCode, message);
        }
    }
}
