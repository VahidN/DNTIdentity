using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ASPNETCoreIdentitySample.Common.EnumToolKit
{
    public static class EnumHelper
    {

        /// <summary>
        /// 
        /// Example:
        /// public enum EnumGrades
        ///{
        /// [Description("Passed")]
        /// Pass,
        /// [Description("Failed")]
        /// Failed,
        /// [Description("Promoted")]
        /// Promoted
        ///}
        ///
        /// string description = EnumHelper<![CDATA[<EnumGrades>]]>.GetEnumDescription("pass");
        /// </summary>
        /// <typeparam name="T">Enum Type</typeparam>
        public static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Any() ? ((DescriptionAttribute)customAttribute.FirstOrDefault()).Description : name;
        }
        public static Expected GetAttributeValue<T, Expected>(this Enum enumeration, Func<T, Expected> expression)
        where T : Attribute
        {
            T attribute =
              enumeration
                .GetType()
                .GetMember(enumeration.ToString())
                .Where(member => member.MemberType == MemberTypes.Field)
                .FirstOrDefault()
                .GetCustomAttributes(typeof(T), false)
                .Cast<T>()
                .SingleOrDefault();

            if (attribute == null)
                return default(Expected);

            return expression(attribute);
        }
        public static string EnumToDisplayText(Enum en)
        {
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());
            if (memInfo.Length <= 0) return en.ToString();
            var attrs = memInfo[0].GetCustomAttributes(typeof(DisplayText), false);
            return attrs.Any() ? ((DisplayText)attrs.FirstOrDefault()).Text : en.ToString();
        }
        /// <summary>
        /// Parse a string value to the given Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            Debug.Assert(!string.IsNullOrEmpty(value));
            return (T)Enum.Parse(typeof(T), value, true);
        }
        /// <summary>
        /// Converts Enumeration type into a dictionary of names and values
        /// </summary>
        /// <param name="t">Enum type</param>
        public static IDictionary<string, int> EnumToDictionary(this Type t)
        {
            if (t == null) throw new NullReferenceException();
            if (!t.GetTypeInfo().IsEnum) throw new InvalidCastException("object is not an Enumeration");

            string[] names = Enum.GetNames(t);
            Array values = Enum.GetValues(t);

            return (from i in Enumerable.Range(0, names.Length)
                    select new { Key = names[i], Value = (int)values.GetValue(i) })
                .ToDictionary(k => k.Key, k => k.Value);
        }
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static List<SelectListItem> EnumsConvertToListItems<TEnum>()
        {
            var enumType = typeof(TEnum);
            var enumValues = Enum.GetValues(enumType).Cast<Enum>();
            var selectItems = enumValues.Select(s => new SelectListItem
            {
                Text = s.GetDescription(),
                Value = s.ToString()
            });

            return selectItems.ToList();
        }
        private static string GetName(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        private static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.GetName());
            var descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return descriptionAttribute == null ? value.GetName() : descriptionAttribute.Description;
        }

    }
}
