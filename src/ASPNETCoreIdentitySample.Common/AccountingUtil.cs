using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ASPNETCoreIdentitySample.Common
{
    public class DecimalFormatConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal));
        }


        public override void WriteJson(JsonWriter writer, object value,
                                       JsonSerializer serializer)
        {
            writer.WriteValue(string.Format("{0:N2}", value));
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType,
                                     object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    public static class AccountingUtil
    {

        public static decimal RoundOff(decimal number, int interval)
        {
            decimal remainder = number % interval;
            number += (remainder < interval / 2) ? -remainder : (interval - remainder);
            return number;
        }
        public static decimal RoundingNumber(decimal num,int len=0)
        {
            //You Can Improve This Code Better ;)
            
            var numLen = len;
            if(numLen==0)
            {
                numLen = num.ToString().Length;
            }
            var numFloor = Math.Floor(num);
            if (numLen >= 4)
            {
                //Return Round to Thousends 
                return RoundOff(numFloor,1000);
            }
            else
            {
                switch (numLen)
                {
                    case 3:
                        //Return Round to Houndred 
                        return RoundOff(numFloor,100);
                    case 2:
                        //Return Round to Tens
                        return RoundOff(numFloor,10);
                    case 1:
                        //Return Round to Ones
                        return RoundOff(numFloor,1);
                    default:
                        //Return Round Decimal
                        return numFloor;

                }

            }
        }
        public static string ConvertMoneyToRialsWithComma(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return String.Format(new CultureInfo("fa-IR"), "{0:C0}", Convert.ToInt64(str)).Replace("ريال", "") + " ریال";
        }

        public static string ConvertMoneyToRialsWithComma(decimal str)
        {
            return String.Format(new CultureInfo("fa-IR"), "{0:C0}", str).Replace("ريال", "") + " ریال";
        }
        public static string ConvertMoneyToRialsWithComma(long str)
        {
            return String.Format(new CultureInfo("fa-IR"), "{0:C0}", str).Replace("ريال", "") + " ریال";
        }

        public static string ConvertMoneyToRialsWithComma(double str)
        {
            return String.Format(new CultureInfo("fa-IR"), "{0:C0}", str).Replace("ريال", "") + " ریال";
        }

        public static string RemoveSpecialCharacterMoney(string str)
        {
            return Regex.Replace(str, @"[^0-9\.]", string.Empty);
        }

        public static string RemoveSpecialCharacterMoney(double str)
        {
            return Regex.Replace(str.ToString(CultureInfo.CurrentCulture), @"[^0-9\.]", string.Empty);
        }

        public static double[] ConvertStringsToDoubles(string[] values)
        {
            if (!values.Any()) return null;
            return values.Select(value => Convert.ToDouble(RemoveSpecialCharacterMoney(value))).ToArray();
        }
        public static double ConvertStringToDouble(string str)
        {
            return string.IsNullOrEmpty(str) ? 0 : Convert.ToDouble(RemoveSpecialCharacterMoney(str));
        }
    }
}
