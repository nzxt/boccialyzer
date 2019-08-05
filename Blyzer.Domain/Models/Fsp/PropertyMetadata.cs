using System;
using System.Collections.Generic;
using System.Text;

namespace Blyzer.Domain.Models.Fsp
{
    public interface IPropertyMetadata
    {
        string Name { get; set; }
        string FullName { get; }
        bool CanFilter { get; set; }
        bool CanSort { get; set; }
    }

    public class PropertyMetadata : IPropertyMetadata
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool CanFilter { get; set; }
        public bool CanSort { get; set; }
    }
}
