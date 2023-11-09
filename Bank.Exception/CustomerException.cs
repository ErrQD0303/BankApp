using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
    /// <summary>
    /// Exception class that represents error raised in Customer class
    /// </summary>
    public class CustomerException : ApplicationException
    {
        /// <summary>
        /// Constructor that initializes exception message
        /// </summary>
        /// <param name="message">exception message</param>
        public CustomerException(string message) : base(message) { }
        /// <summary>
        /// Constructor that initializes exception message and inner exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CustomerException(string message, Exception innerException) : base(message, innerException) { }
    }
}
