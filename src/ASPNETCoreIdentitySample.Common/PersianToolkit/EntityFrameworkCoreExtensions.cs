using ASPNETCoreIdentitySample.Common.ReflectionToolkit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using DNTPersianUtils.Core;

namespace ASPNETCoreIdentitySample.Common.PersianToolkit
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/465
    /// And http://www.dotnettips.info/post/2507
    /// </summary>
    public static class EntityFrameworkCoreExtensions
    {
        public static void ApplyCorrectYeKe(this DbContext dbContext)
        {
            if (dbContext == null)
            {
                return;
            }

            //پیدا کردن موجودیت‌های تغییر کرده
            var changedEntities = dbContext.ChangeTracker
                                      .Entries()
                                      .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var item in changedEntities)
            {
                var entity = item.Entity;
                if (item.Entity == null)
                {
                    continue;
                }

                //یافتن خواص قابل تنظیم و رشته‌ای این موجودیت‌ها
                var propertyInfos = entity.GetType().GetProperties(
                    BindingFlags.Public | BindingFlags.Instance
                    ).Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                var propertyReflector = new PropertyReflector();

                //اعمال یکپارچگی نهایی
                foreach (var propertyInfo in propertyInfos)
                {
                    var propName = propertyInfo.Name;
                    var value = propertyReflector.GetValue(entity, propName);
                    if (value != null)
                    {
                        var strValue = value.ToString();
                        var newVal = strValue.ApplyCorrectYeKe();
                        if (newVal == strValue)
                        {
                            continue;
                        }
                        propertyReflector.SetValue(entity, propName, newVal);
                    }
                }
            }
        }
    }
}