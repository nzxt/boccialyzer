using Blyzer.Domain.Enums;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Blyzer.Domain.Models.Fsp
{
    public interface IFilterTerm
    {
        string Filter { set; }
        string[] Names { get; }
        string Operator { get; }
        //bool OperatorIsCaseInsensitive { get; }
        bool OperatorIsNegated { get; }
        FilterOperator OperatorParsed { get; }
        string[] Values { get; }
        string FilterString { get; }
    }

    public class FilterTerm : IFilterTerm, IEquatable<FilterTerm>
    {
        public string FilterString { get; private set; }
        public FilterTerm() { }

        private const string EscapedPipePattern = @"(?<!($|[^\\])(\\\\)*?\\)\|";

        private static readonly string[] Operators = new string[] {
                    "!@=*",
                    "!_=*",
                    "!@=",
                    "!_=",
                    "==*",
                    "@=*",
                    "_=*",
                    "==",
                    "!=",
                    ">=",
                    "<=",
                    ">",
                    "<",
                    "@=",
                    "_="
        };

        public string Filter
        {
            set
            {
                var filterSplits = value.Split(Operators, StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Trim()).ToArray();
                Names = Regex.Split(filterSplits[0], EscapedPipePattern).Select(t => t.Trim()).ToArray();
                Values = filterSplits.Length > 1 ? Regex.Split(filterSplits[1], EscapedPipePattern).Select(t => t.Trim()).ToArray() : null;
                Operator = Array.Find(Operators, o => value.Contains(o)) ?? "==";
                OperatorParsed = GetOperatorParsed(Operator);
                //OperatorIsCaseInsensitive = Operator.EndsWith("*");
                OperatorIsNegated = OperatorParsed != FilterOperator.NotEquals && Operator.StartsWith("!");
                var filterStrings = value.Split(",");

                FilterString= String.Join("&&",filterSplits);
            }

        }

        public string[] Names { get; private set; }

        public FilterOperator OperatorParsed { get; private set; }

        public string[] Values { get; private set; }

        public string Operator { get; private set; }

        private FilterOperator GetOperatorParsed(string @operator)
        {
            switch (@operator.TrimEnd('*'))
            {
                case "==":
                    return FilterOperator.Equals;
                case "!=":
                    return FilterOperator.NotEquals;
                case "<":
                    return FilterOperator.LessThan;
                case ">":
                    return FilterOperator.GreaterThan;
                case ">=":
                    return FilterOperator.GreaterThanOrEqualTo;
                case "<=":
                    return FilterOperator.LessThanOrEqualTo;
                case "@=":
                case "!@=":
                    return FilterOperator.Contains;
                case "_=":
                case "!_=":
                    return FilterOperator.StartsWith;
                default:
                    return FilterOperator.Equals;
            }
        }

        //public bool OperatorIsCaseInsensitive { get; private set; }

        public bool OperatorIsNegated { get; private set; }

        public bool Equals(FilterTerm other)
        {
            return Names.SequenceEqual(other.Names)
                && Values.SequenceEqual(other.Values)
                && Operator == other.Operator;
        }

    }
}
