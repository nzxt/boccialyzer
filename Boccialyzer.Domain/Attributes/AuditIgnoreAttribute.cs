using System;

namespace Boccialyzer.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    class AuditIgnoreAttribute : Attribute
    {
        //public bool IsIgnore { get; set; }
    }
}
