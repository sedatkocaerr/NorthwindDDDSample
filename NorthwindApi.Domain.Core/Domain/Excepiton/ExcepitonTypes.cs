using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Core.Domain.Excepiton
{
    public enum ExcepitonTypes
    {
        InvalidRequiredDate = 0,
        InvalidProductPrice = 1,
        InvalidProductUnitsInStock = 2,
        FirstNameShort = 3,
        LastNameShort = 4,
        TitleShortException = 5,
        InvalidHireDateException = 6,
        InvalidBirthDateException = 7,
        InvalidQuantity=8,
        InvalidUnitPrice=9,
        OrderDetailAlreadyExist=10,
        InvalidaEmail = 11,
        InvalidPasswordHash = 12,
        InvalidPasswordSalt = 13
    }
}
