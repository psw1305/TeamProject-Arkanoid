
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static readonly Dictionary<System.Type, Object> _services =
        new Dictionary<System.Type, Object>();

    public static void RegisterService<T>(T service) where T : Object
    {
        _services[service.GetType()] = service;
    }

    public static T GetService<T>() where T : Object
    {
        if (_services.TryGetValue(typeof(T), out Object service))
        {
            return service as T;
        }
        return null;
    }
}