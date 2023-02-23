using System.Reflection;

namespace Sam.Application {
    public static class Mapper { // primitive mapper
        public static TTo? Map<TTo>(object source) where TTo : class, new() {
            if (source == null)
                return null;

            TTo result = new();
            foreach (PropertyInfo sourceProp in source.GetType().GetProperties()) {
                PropertyInfo? destProp = typeof(TTo).GetProperty(sourceProp.Name);
                if (destProp != null && destProp.CanWrite)
                    destProp.SetValue(result, sourceProp.GetValue(source));
            }

            return result;
        }

        public static object? Map(object? source, Type? toType) {
            if (source == null) return null;
            if (toType == null) return source;

            var result = Activator.CreateInstance(toType);
            foreach (PropertyInfo sourceProp in source.GetType().GetProperties()) {
                PropertyInfo? destProp = toType.GetProperty(sourceProp.Name);
                if (destProp != null && destProp.CanWrite)
                    destProp.SetValue(result, sourceProp.GetValue(source));
            }

            return result;
        }
    }
}
