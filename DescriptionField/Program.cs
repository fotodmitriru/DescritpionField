using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DescriptionField
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new MyClass();
            string ageDescription = GetDescription<MyClass>(nameof(x.Age));
            Console.WriteLine(ageDescription);
            Console.ReadLine();
        }

        public static string GetDescription<T>(string fieldName)
        {
            FieldInfo fi = typeof(T).GetField(fieldName);
            PropertyInfo propertyInfo = typeof(T).GetProperty(fieldName);
            if (propertyInfo != null)
            {
                try
                {
                    object[] descriptionAttrs = propertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (descriptionAttrs[0] is DescriptionAttribute descriptionAttr)
                        return descriptionAttr.Description;
                }
                catch
                {
                    return string.Empty;
                }
            }

            return string.Empty;
        }
    }

    public class MyClass
    {
        public string Name { get; set; }

        [Description("The value")] public int Age { get; set; }
    }
}