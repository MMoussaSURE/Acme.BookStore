

using System.Collections.Generic;
using System.Reflection;
using System;

namespace Acme.BookStore.Common.Extentions
{
    public class PermissionUtility
    {
        public static List<string> GetConstants(Type type)
        {
            List<string> constants = new List<string>();

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(string) && field.IsLiteral && !field.IsInitOnly)
                {
                    constants.Add((string)field.GetValue(null));
                }
            }

            return constants;
        }
    }
}
