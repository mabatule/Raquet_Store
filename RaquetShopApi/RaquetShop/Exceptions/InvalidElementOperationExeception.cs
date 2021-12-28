using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Exceptions
{ 
    public class InvalidElementOperationExeception:Exception
    {
        public InvalidElementOperationExeception(string message) : base(message)
        {

        }
    }
}
