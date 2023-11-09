using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Contracts
{
    /// <summary>
    /// Represent   Transaction
    /// </summary>
    /// 
    public interface ITransaction : ICloneable
    {
        #region Properties
        Guid TransactionID { get; set; }
        DateTime TransactionDate { get; set; }
        long SourceAccountNumber { get; set; }
        long DestinationAccountNumber { get; set; }
        double Amount { get; set; }
        TransactionType transactionType { get; set; }
        #endregion
    }
}
