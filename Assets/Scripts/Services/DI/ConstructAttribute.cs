using System;

namespace DI
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
    public class ConstructAttribute : Attribute
    {
    }
}