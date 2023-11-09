using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Entities.Contracts
{
    /// <summary>
    /// Represents Account of Customer
    /// </summary>
    public interface IAccount : ICloneable
    {
        #region Properties
        Guid AccountID { get; set; }
        long AccountNumber { get; set; }
        long CustomerCode { get; set; }
        double Balance { get; set; }        
        #endregion
    }
}
