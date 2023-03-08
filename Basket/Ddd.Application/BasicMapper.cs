using System.Reflection;

namespace Ddd.Application;

public static class Mapper { // primitive mapper
    public static TTo? Map<TTo>(object source) where TTo : class, new() {
        if (source == null) return null;
        return Map(source, new TTo());
    }

    public static object? Map(object? source, Type? toType) {
        if (source == null) return null;
        if (toType == null) return source;

        var dest = Activator.CreateInstance(toType);
        if (dest == null) throw new Exception("cannot create type of dest");
        return CopyProps(source, dest);
    }

    public static TTo? Map<TTo>(object source, TTo dest) where TTo : class, new() {
        if (source == null) return null;
        return (TTo)CopyProps(source, dest);
    }

    private static object CopyProps(object source, object dest) {
        foreach (PropertyInfo sourceProp in source.GetType().GetProperties()) {
            PropertyInfo? destProp = dest.GetType().GetProperty(sourceProp.Name);
            if (destProp != null && destProp.CanWrite)
                destProp.SetValue(dest, sourceProp.GetValue(source));
        }
        return dest;
    }
}
