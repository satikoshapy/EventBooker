using System;

namespace Reservation
{
    public class ValidationException : Exception
    {
       

        public ValidationException(string message) : base(message)
        {
        }
    }
}
