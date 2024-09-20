using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsole.Exceptions
{
    public class AgendaException : Exception
    {
        public AgendaException() { }

        public AgendaException(string message)
        : base(message)
        {
        }
    }
}
