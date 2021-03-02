using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Core
{
    public class TypeDataCheck
    {
        public static T IsNull<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            return value;
        }

        public static string IsNullOrEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            return value;
        }

        public static bool checkId(Guid Id)
        {
            if(Id == null || Id == Guid.Empty)
            {
                return false;
            }
            return true;
        }

    }
}
