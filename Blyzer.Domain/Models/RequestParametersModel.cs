using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Blyzer.Domain.Filtering;
using Blyzer.Domain.Sorting;

namespace Blyzer.Domain.Models
{
    /// <summary>
    /// Input parameters model
    /// </summary>
    public class RequestParametersModel
    {
        private const string EscapedCommaPattern = @"(?<!($|[^\\])(\\\\)*?\\),";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="page">PAge</param>
        /// <param name="pageSize">PAge size</param>
        /// <param name="filters">Filters</param>
        /// <param name="sorts">Sorts</param>
        public RequestParametersModel(int page = 1, int pageSize = 25, string filters = "", string sorts = "")
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



        List<FilterTerm> FilterTerm => GetFiltersParsed();

        List<SortTerm> SortTerm => GetSortsParsed();

        public List<FilterTerm> GetFiltersParsed()
        {
            if (Filters != null)
            {
                var value = new List<FilterTerm>();

                var filterStrings = Filters.Split(",", StringSplitOptions.RemoveEmptyEntries);
                foreach (var filter in filterStrings)
                {
                    if (string.IsNullOrEmpty(filter.Trim())) continue;

                    value.Add(new FilterTerm { Filter = filter.Trim() });
                }

                return value;
            }

            return null;
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

    }
}