using System;

namespace PrywatnaPrzychodniaLekarska.Exeptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
