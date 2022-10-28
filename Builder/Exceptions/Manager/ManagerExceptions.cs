using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Exceptions.Manager
{
    public class InvalidProjectTypeException : Exception
    {
        public InvalidProjectTypeException()
        {
        }

        public InvalidProjectTypeException(string message)
            : base(message)
        {
        }

        public InvalidProjectTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
