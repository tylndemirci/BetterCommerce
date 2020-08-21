using System;
using BetterCommerce.Core.Utilities.Results;

namespace BetterCommerce.Core.Extensions
{
    public static class ClassExtensions
    {
         public static string FormalizeDecimal(this string value)
        {
            return value.Replace(",", "v").Replace(".", ",").Replace("v", ".");
        }

        public static string NormalizeStr(this string value)
        {
            return (value??"").ToLower()    
                    .Replace("ı", "i")
                    .Replace("ü", "u")
                    .Replace("ö", "o")
                    .Replace("ç", "c")
                    .Replace("ğ", "g")
                    .Replace("ş", "s")
                ;
        }

        public static string ToSlug(this string value)
        {
            var returnVal = (value??"").ToLower()
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace(".", "")
                    .Replace(",", "")
                    .Replace("'", "")
                    .Replace("$", "")
                    .Replace("+", "")
                    .Replace("*", "")
                    .Replace("?", "")
                    .Replace("/", "")
                    .Replace("\\", "")
                    .Replace("ı", "i")
                    .Replace("ü", "u")
                    .Replace("ö", "o")
                    .Replace("ç", "c")
                    .Replace("ğ", "g")
                    .Replace("ş", "s")
                    .Replace(" ", "-")
                ;

            while (!string.IsNullOrWhiteSpace(returnVal) && returnVal.Substring(0, 1) == "-")
            {
                returnVal = returnVal.Substring(1);
            }

            return returnVal;
        }

        public static string FirstCharToUpper(this string value)
        {
            if (value.IsNullS()) return "";
            value = value.ToLower();
            var array = value.ToCharArray();
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }

            for (var i = 1; i < array.Length; i++)
            {
                if (array[i - 1] != ' ') continue;
                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }

            return new string(array);
        }


        public static bool IsNullT<T>(this T value)
        {
            if (value == null) return true;
            return false;
        }

        public static bool IsNullS<T>(this T value)
        {
            if (value == null) return true;
            try
            {
                if (string.IsNullOrWhiteSpace(value.ToString())) return true;
            }
            catch
            {
            }

            return false;
        }

        public static int? ToInt32<T>(this T value)
        {
            if (value == null) return new int();
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return new int();
            }
        }

        public static IResult GetSaveResult(this int value)
        {
            if (value > 0)
                return new SuccessResult();
            else
                return new ErrorResult();
        }

        public static decimal ToPositive(this decimal value)
        {
            return value > 0 ? value : -value;
        }

        public static int ToPositive(this int value)
        {
            return value > 0 ? value : -value;
        }

        public static decimal GetDiscountRate(this decimal value, decimal lastPrice)
        {
            return value == 0 ? 0 :
                lastPrice == 0 ? 100 :
                100 - (lastPrice /
                       (value / 100));
        }

        public static decimal GetDiscountVolume(this decimal value, decimal discount)
        {
            return value == 0 ? 0 :
                discount == 0 ? 0 :
                (
                    value / 100 * discount
                )
                ;
        }

        public static decimal GetDiscountVolume(this decimal value, int discount)
        {
            return value == 0 ? 0 :
                discount == 0 ? 0 :
                (
                    value / 100 * discount
                )
                ;
        }

        public static decimal GetDiscountedVolume(this decimal value, decimal discount)
        {
            return value == 0 ? 0 :
                discount == 0 ? value :
                value - (
                    value / 100 * discount
                )
                ;
        }

        public static decimal GetTotalDiscountVolume(this decimal value, decimal lastvalue)
        {
            return value == 0 ? 0 :
                lastvalue == 0 ? 100 :
                100 - (lastvalue / (value / 100));
        }

        public static decimal GetDiscountedVolume(this decimal value, int discount)
        {
            return value == 0 ? 0 :
                discount == 0 ? value :
                value - (
                    value / 100 * discount
                )
                ;
        }

        public static decimal GetTaxedVolume(this decimal value, int tax)
        {
            return value == 0 ? 0 :
                tax == 0 ? value :
                value * 100 / (100 + tax)
                ;
        }

        public static int ToInt32<T>(this T value, int defaultvalue)
        {
            if (value == null) return defaultvalue;
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return defaultvalue;
            }
        }

        public static decimal ToDecimal<T>(this T value, decimal defaultvalue)
        {
            if (value == null) return defaultvalue;
            try
            {
                return Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                return defaultvalue;
            }
        }

        public static string ToStringDefault<T>(this T value, string defaultvalue)
        {
            if (value == null) return defaultvalue;
            try
            {
                return value.ToString();
            }
            catch (Exception)
            {
                return defaultvalue;
            }
        }

        public static decimal? ToDecimal<T>(this T value)
        {
            if (value == null) return new decimal();
            try
            {
                return Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                return new decimal();
            }
        }

        public static decimal ToDecimal<T>(this T value, int defaultvalue)
        {
            if (value == null) return defaultvalue;
            try
            {
                return Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                return defaultvalue;
            }
        }

        public static DateTime? ToDateTime<T>(this T value)
        {
            if (value == null) return new DateTime();
            try
            {
                return Convert.ToDateTime(value);
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }


        public static DateTime ToDateTime<T>(this T value, DateTime defaultvalue)
        {
            if (value == null) return defaultvalue;
            try
            {
                return Convert.ToDateTime(value);
            }
            catch (Exception)
            {
                return defaultvalue;
            }
        }

        public static DateTime ToDateTimeToOrder(this DateTime value)
        {
            return Convert.ToDateTime(value.ToString("dd.MM.yyyy"));
        }
    }
}