using System;
using System.Collections.Generic;
using System.Reflection;

namespace Graphics.UI.MouseControllers.Providers
{
    internal static class TypeProvider
    {
        internal static List<Type> LoadEspecificyTypesFromAssembly(Assembly assembly, Type type)
        {
            Type[] types = null;
            List<Type> listToReturn = new List<Type>();

            try
            {
                types = assembly.GetTypes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (types != null)
                foreach (var t in types)
                    if (IsBaseType(t, type))
                        listToReturn.Add(t);

            return listToReturn;
        }

        private static bool IsBaseType(Type type, Type baseType)
        {
            if (baseType.IsInterface)
            {
                if (baseType.GetInterface(baseType.Name) != null) return true;
                if (baseType.BaseType == null) return false;
            }
            else
            {
                if (type.Equals(baseType)) return true;
                if (type.BaseType == null) return false;
            }

            return IsBaseType(type.BaseType, baseType);
        }
    }
}