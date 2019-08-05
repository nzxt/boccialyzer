using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Blyzer.Domain.Models.Fsp
{
    /// <summary>
    /// Input parameters model
    /// </summary>
    public class GetParametersModel
    {
        private const string EscapedCommaPattern = @"(?<!($|[^\\])(\\\\)*?\\),";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="page">PAge</param>
        /// <param name="pageSize">PAge size</param>
        /// <param name="filters">Filters</param>
        /// <param name="sorts">Sorts</param>
        public GetParametersModel(int page = 1, int pageSize = 25, string filters = "", string sorts = "")
        {
            Page = page;
            PageSize = pageSize;
            Filters = filters;
            Sorts = sorts;
        }
        /// <summary>
        /// Filters
        /// </summary>
        public string Filters { get; set; }
        /// <summary>
        /// Sorts
        /// </summary>
        public string Sorts { get; set; }
        /// <summary>
        /// Page
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        public string SortString
        {
            get { return SortTerms(); }
        }

        List<FilterTerm> FilterTerm
        {
            get { return GetFiltersParsed(); }
        }

        List<SortTerm> SortTerm
        {
            get { return GetSortsParsed(); }
        }

        public List<FilterTerm> GetFiltersParsed()
        {
            if (Filters != null)
            {
                var value = new List<FilterTerm>();
                foreach (var filter in Regex.Split(Filters, EscapedCommaPattern))
                {
                    if (string.IsNullOrWhiteSpace(filter)) continue;

                    if (filter.StartsWith("("))
                    {
                        var filterOpAndVal = filter.Substring(filter.LastIndexOf(")", StringComparison.Ordinal) + 1);
                        var subfilters = filter.Replace(filterOpAndVal, "").Replace("(", "").Replace(")", "");
                        var filterTerm = new FilterTerm
                        {
                            Filter = subfilters + filterOpAndVal
                        };
                        if (!value.Any(f => f.Names.Any(n => filterTerm.Names.Any(n2 => n2 == n))))
                        {
                            value.Add(filterTerm);
                        }
                    }
                    else
                    {
                        var filterTerm = new FilterTerm
                        {
                            Filter = filter
                        };
                        value.Add(filterTerm);
                    }
                }

                return value;
            }
            else
            {
                return null;
            }
        }

        public List<SortTerm> GetSortsParsed()
        {
            if (Sorts != null)
            {
                var value = new List<SortTerm>();
                foreach (var sort in Regex.Split(Sorts, EscapedCommaPattern))
                {
                    if (string.IsNullOrWhiteSpace(sort)) continue;

                    var sortTerm = new SortTerm()
                    {
                        Sort = sort
                    };
                    if (value.All(s => s.Name != sortTerm.Name))
                    {
                        value.Add(sortTerm);
                    }
                }

                return value;
            }

            return null;
        }

        public string SortTerms()
        {
            if (Sorts != null)
            {
                var values = new List<string>();

                foreach (var sort in Sorts.Split(",", StringSplitOptions.RemoveEmptyEntries))
                {
                    values.Add(sort.StartsWith("-") ? $"{sort.Substring(1)} DESC" : sort);
                }

                return string.Join(",", values);
            }

            return "id";
        }
    }
}