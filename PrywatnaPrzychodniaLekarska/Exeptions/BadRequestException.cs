using System;

namespace PrywatnaPrzychodniaLekarska.Exeptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base (message)
        {
        }
    }
}
