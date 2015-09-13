using Refit;
using System;
using System.Reflection;

namespace OpenPr0gramm
{
    internal class EnumsAsIntegersParameterFormatter : IUrlParameterFormatter
    {
        // See: https://github.com/paulcbetts/refit/issues/184#issuecomment-137324961
        public string Format(object parameterValue, ParameterInfo parameterInfo)
        {
            if (parameterValue == null)
                return null;
            var parameterType = Denullify(parameterInfo.ParameterType);
            if (parameterType.IsEnum && parameterType == typeof(ItemFlags))
                return ((int)((ItemFlags)parameterValue)).ToString();
            return parameterValue.ToString();
        }

        // Just makes sure you have the generic type arg for Nullable<T>
        static Type Denullify(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return type.GetGenericArguments()[0];
            return type;
        }
    }
}
