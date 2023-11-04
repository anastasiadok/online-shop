using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.db.Models
{
    public enum UserType
    {
        Admin,
        User
    }

    public enum TransactionStatus
    {
        InReview,
        InDelivery,
        Completed,
        Cancelled
    }
}
