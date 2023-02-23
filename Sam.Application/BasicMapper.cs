using System.Reflection;

namespace Sam.Application {
    public static class Mapper { // primitive mapper
        public static TTo Map<TTo>(object source) where TTo : class, new() {
            TTo dest = new();

            foreach (PropertyInfo sourceProp in source.GetType().GetProperties()) {
                PropertyInfo? destProp = typeof(TTo).GetProperty(sourceProp.Name);
                if (destProp != null && destProp.CanWrite)
                    destProp.SetValue(dest, sourceProp.GetValue(source));
            }

            return dest;
        }

        public static object? Map(object? source, Type toType) {
            if (source == null)
                return null;
            var dest = Activator.CreateInstance(toType)!;

            foreach (PropertyInfo sourceProp in source.GetType().GetProperties()) {
                PropertyInfo? destProp = toType.GetProperty(sourceProp.Name);
                if (destProp != null && destProp.CanWrite)
                    destProp.SetValue(dest, sourceProp.GetValue(source));
            }

            return dest;
        }
    }
}
