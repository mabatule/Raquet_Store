using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Exceptions
{
    public class ExistElementsException:Exception
    {
        public ExistElementsException(string message):base(message)
        {

        }
    }
}
