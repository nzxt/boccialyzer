using Blyzer.Domain.Enums;
using System;
using System.Linq;

namespace Blyzer.Domain.Filtering
{
    /// <summary>
    /// Filter term interface
    /// </summary>
    public interface IFilterTerm
    {
        /// <summary>
        /// Filter
        /// </summary>
        string Filter { set; }
        /// <summary>
        /// Property name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Filter operator
        /// </summary>
        FilterOperator Operator { get; }
        /// <summary>
        /// Filter Criteria
        /// </summary>
        string[] Values { get; }
    }

    /// <summary>
    /// Filter term
    /// </summary>
    public class FilterTerm : IFilterTerm
    {
        /// <inheritdoc/>
        public string Filter
        {
            set
            {
                var filterSplits = value.Split(FilterHelper.GetFilterOperators.Keys.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                Name = filterSplits[0];
                Values = filterSplits.Length > 1
                    ? filterSplits[1].Split("|", StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray()
                    : null;
                Operator = FilterHelper.GetFilterOperators[Array.Find(FilterHelper.GetFilterOperators.Keys.ToArray(), value.Contains) ?? "=="];
            }

        }
        /// <inheritdoc/>
        public string Name { get; private set; }
        /// <inheritdoc/>
        public string[] Values { get; private set; }
        /// <inheritdoc/>
        public FilterOperator Operator { get; private set; }
    }
}
