using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Exceptions
{
    public class NotFoundFileException : Exception
    {
        public NotFoundFileException(string message)
            : base(message)
        {
        }
    }
}
