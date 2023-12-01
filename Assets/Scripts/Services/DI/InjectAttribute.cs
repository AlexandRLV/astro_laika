using System;
using JetBrains.Annotations;

namespace DI
{
    [AttributeUsage(AttributeTargets.Field)]
    [MeansImplicitUse]
    public class InjectAttribute : Attribute
    {
    }
}