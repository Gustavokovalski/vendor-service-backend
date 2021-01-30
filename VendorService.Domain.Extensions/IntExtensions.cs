using System;

namespace VendorService.Domain.Extensions
{
    public static class IntExtensions
    {
        public static T GetEnum<T>(this int n)
        {
            if (!Enum.IsDefined(typeof(T), n))
                throw new ArgumentOutOfRangeException(typeof(T).ToString(), "not found");

            return (T)Enum.ToObject(typeof(T), n);
        }
    }
}
