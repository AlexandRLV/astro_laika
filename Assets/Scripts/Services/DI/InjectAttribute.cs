using System;

namespace DI
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Constructor | AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    {
        
    }
}