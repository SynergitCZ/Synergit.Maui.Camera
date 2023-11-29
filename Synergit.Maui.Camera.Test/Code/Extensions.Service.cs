using System.Reflection;

namespace Synergit.Maui.Camera.Test;

public static partial class Extensions
{
    public static void UseResolver(this IServiceProvider sp)
    {
        Resolver.RegisterServiceProvider(sp);
    }

    public static IServiceCollection AddTransientAllDescendantsOf<TType>(this IServiceCollection services) where TType : class
    {
        var currentAssembly = Assembly.GetExecutingAssembly();

        foreach (var type in currentAssembly.DefinedTypes.Where(e => e.IsSubclassOf(typeof(TType)) && !e.IsAbstract))
        {
            services.AddTransient(type.AsType());
        }

        return services;
    }
}
