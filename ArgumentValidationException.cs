using System;

namespace JwtTokenGenerator
{
    class ArgumentValidationException : Exception
    {
        public ArgumentValidationException(string message) : base(message) { }
    }
}
