using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace MethodButton.VK.Abstract
{
    public abstract class VkArguments
    {
        protected string GetStringResult(Type t, object obj)
        {
            string result = "";
            foreach (var property in t.GetProperties())
            {
                var attribute = property.GetCustomAttribute<DescriptionAttribute>(false);
                if (attribute == null)
                    continue;
                string argument = attribute.Description;
                var value = property.GetValue(obj);
                if (value != null)
                {
                    result += argument + "=" + value + "&";
                }
            }
            return result;
        }
    }
}
