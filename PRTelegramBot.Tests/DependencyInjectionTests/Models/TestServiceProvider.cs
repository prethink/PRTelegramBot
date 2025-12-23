using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Варианты времени жизни сервиса.
/// </summary>
public enum TestServiceLifetime
{
    Singleton,
    Scoped,
    Transient
}

public sealed class TestServiceProvider :
    IServiceProvider,
    IServiceScopeFactory,
    IDisposable
{
    private readonly Dictionary<Type, List<ServiceDescriptor>> descriptors;
    private readonly Dictionary<ServiceDescriptor, object> singletons = new();
    private readonly Dictionary<ServiceDescriptor, object> scopedInstances = new();

    public TestServiceProvider()
    {
        descriptors = new();
    }

    private TestServiceProvider(
        Dictionary<Type, List<ServiceDescriptor>> descriptors,
        Dictionary<ServiceDescriptor, object> singletons)
    {
        this.descriptors = descriptors;
        this.singletons = singletons;
    }

    // ================= REGISTRATION =================

    public void AddSingleton<TService>(TService instance) where TService : class
    {
        AddDescriptor(new ServiceDescriptor(
            typeof(TService),
            _ => instance,
            TestServiceLifetime.Singleton));
    }

    public void AddSingleton<TService, TImpl>()
        where TService : class
        where TImpl : class, TService
    {
        Add<TService, TImpl>(TestServiceLifetime.Singleton);
    }

    public void AddScoped<TService, TImpl>()
        where TService : class
        where TImpl : class, TService
    {
        Add<TService, TImpl>(TestServiceLifetime.Scoped);
    }

    public void AddTransient<TService, TImpl>()
        where TService : class
        where TImpl : class, TService
    {
        Add<TService, TImpl>(TestServiceLifetime.Transient);
    }

    private void Add<TService, TImpl>(TestServiceLifetime lifetime)
        where TImpl : class
    {
        AddDescriptor(new ServiceDescriptor(
            typeof(TService),
            sp => CreateInstance(typeof(TImpl)),
            lifetime));
    }

    private void AddDescriptor(ServiceDescriptor descriptor)
    {
        if (!descriptors.TryGetValue(descriptor.ServiceType, out var list))
        {
            list = new List<ServiceDescriptor>();
            descriptors[descriptor.ServiceType] = list;
        }

        list.Add(descriptor);
    }

    // ================= RESOLUTION =================

    public object? GetService(Type serviceType)
    {
        // 🔥 IServiceScopeFactory должен резолвиться всегда
        if (serviceType == typeof(IServiceScopeFactory))
            return this;

        // IEnumerable<T>
        if (serviceType.IsGenericType &&
            serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            var elementType = serviceType.GetGenericArguments()[0];
            return ResolveEnumerable(elementType);
        }

        if (!descriptors.TryGetValue(serviceType, out var list))
            return null;

        // ASP.NET DI: возвращается ПОСЛЕДНЯЯ регистрация
        return Resolve(list.Last());
    }

    private object Resolve(ServiceDescriptor descriptor)
    {
        return descriptor.Lifetime switch
        {
            TestServiceLifetime.Singleton => ResolveSingleton(descriptor),
            TestServiceLifetime.Scoped => ResolveScoped(descriptor),
            TestServiceLifetime.Transient => descriptor.Factory(this),
            _ => throw new NotSupportedException()
        };
    }

    private object ResolveSingleton(ServiceDescriptor descriptor)
    {
        if (!singletons.TryGetValue(descriptor, out var instance))
        {
            instance = descriptor.Factory(this);
            singletons[descriptor] = instance;
        }
        return instance;
    }

    private object ResolveScoped(ServiceDescriptor descriptor)
    {
        if (!scopedInstances.TryGetValue(descriptor, out var instance))
        {
            instance = descriptor.Factory(this);
            scopedInstances[descriptor] = instance;
        }
        return instance;
    }

    private object ResolveEnumerable(Type serviceType)
    {
        if (!descriptors.TryGetValue(serviceType, out var list))
            return Array.CreateInstance(serviceType, 0);

        var array = Array.CreateInstance(serviceType, list.Count);
        for (int i = 0; i < list.Count; i++)
        {
            array.SetValue(Resolve(list[i]), i);
        }
        return array;
    }

    // ================= DI =================

    private object CreateInstance(Type type)
    {
        var ctor = type.GetConstructors()
            .OrderByDescending(c => c.GetParameters().Length)
            .First();

        var args = ctor.GetParameters()
            .Select(p => GetService(p.ParameterType)
                ?? throw new InvalidOperationException(
                    $"Cannot resolve {p.ParameterType}"))
            .ToArray();

        return Activator.CreateInstance(type, args)!;
    }

    // ================= SCOPES =================

    public IServiceScope CreateScope()
        => new TestServiceScope(new TestServiceProvider(descriptors, singletons));

    private sealed class TestServiceScope : IServiceScope
    {
        public IServiceProvider ServiceProvider { get; }

        public TestServiceScope(IServiceProvider provider)
        {
            ServiceProvider = provider;
        }

        public void Dispose() { }
    }

    public void Dispose()
    {
        scopedInstances.Clear();
    }

    private sealed class ServiceDescriptor
    {
        public Type ServiceType { get; }
        public Func<IServiceProvider, object> Factory { get; }
        public TestServiceLifetime Lifetime { get; }

        public ServiceDescriptor(
            Type serviceType,
            Func<IServiceProvider, object> factory,
            TestServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            Factory = factory;
            Lifetime = lifetime;
        }
    }
}
