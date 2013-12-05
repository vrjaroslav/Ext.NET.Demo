using System;

namespace Uber.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class Securable : Attribute
    {
    }
}
