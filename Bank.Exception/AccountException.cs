using Bank.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Exceptions
{
    /// <summary>
    /// Exception class that represents error raised in Account class
    /// </summary>
    public class AccountException : ApplicationException
    {
        /// <summary>
        /// Constrcutor that initializes exception message
        /// </summary>
        /// <param name="message">exception message</param>
        public AccountException(string message) : base(message) { }
        /// <summary>
        /// Constructor that initializes exception message and inner exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AccountException(string message, Exception innerException) : base(message, innerException) { } 
    }
}
