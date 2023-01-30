using System;

namespace PrywatnaPrzychodniaLekarska.Exeptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
