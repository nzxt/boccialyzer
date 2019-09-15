
namespace Blyzer.Domain.Enums
{
    /// <summary>
    /// Filter Operators
    /// </summary>
    public enum FilterOperator
    {
        /// <summary>
        /// Targets an object in which the property's value is equal to the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        EqualTo,

        /// <summary>
        /// Targets an object in which the property's value contains part of the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        Contains,

        /// <summary>
        /// Targets an object in which the property's value starts with the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        StartsWith,

        /// <summary>
        /// Targets an object in which the property's value ends with the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        EndsWith,

        /// <summary>
        /// Targets an object in which the property's value is not equal to the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        NotEqualTo,

        /// <summary>
        /// Targets an object in which the property's value is greater than the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        GreaterThan,

        /// <summary>
        /// Targets an object in which the property's value is greater than or equal to the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        GreaterThanOrEqualTo,

        /// <summary>
        /// Targets an object in which the property's value is less than the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        LessThan,

        /// <summary>
        /// Targets an object in which the property's value is less than or equal to the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        LessThanOrEqualTo,

        /// <summary>
        /// Targets an object in which the property's value is null.
        /// </summary>
        /// <remarks>Accepts no value at all.</remarks>
        IsNull,

        /// <summary>
        /// Targets an object in which the property's value is an empty string.
        /// </summary>
        /// <remarks>Accepts no value at all.</remarks>
        IsEmpty,

        /// <summary>
        /// Targets an object in which the property's value is null or an empty string.
        /// </summary>
        /// <remarks>Accepts no value at all.</remarks>
        IsNullOrWhiteSpace,

        /// <summary>
        /// Targets an object in which the property's value is not null.
        /// </summary>
        /// <remarks>Accepts no value at all.</remarks>
        IsNotNull,

        /// <summary>
        /// Targets an object in which the property's value is not an empty string.
        /// </summary>
        /// <remarks>Accepts no value at all.</remarks>
        IsNotEmpty,

        /// <summary>
        /// Targets an object in which the property's value is neither null nor an empty string.
        /// </summary>
        /// <remarks>Accepts no value at all.</remarks>
        IsNotNullNorWhiteSpace,

        /// <summary>
        /// Targets an object in which the provided value is presented in the property's value (as a list).
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        In
    }

    ///// <summary>
    ///// Groups types into simple groups and map the supported FiltersDictionary to each group.
    ///// </summary>
    //public enum TypeGroup
    //{
    //    /// <summary>
    //    /// Default type group, only supports EqualTo and NotEqualTo.
    //    /// </summary>
    //    [SupportedOperations(FilterOperator.EqualTo, FilterOperator.NotEqualTo)]
    //    Default,

    //    /// <summary>
    //    /// Supports all text related FiltersDictionary.
    //    /// </summary>
    //    [SupportedOperations(FilterOperator.Contains, FilterOperator.EndsWith, FilterOperator.EqualTo,
    //                         FilterOperator.IsEmpty, FilterOperator.IsNotEmpty, FilterOperator.IsNotNull, FilterOperator.IsNotNullNorWhiteSpace,
    //                         FilterOperator.IsNull, FilterOperator.IsNullOrWhiteSpace, FilterOperator.NotEqualTo, FilterOperator.StartsWith)]
    //    Text,

    //    /// <summary>
    //    /// Supports all numeric related FiltersDictionary.
    //    /// </summary>
    //    [SupportedOperations(FilterOperator.Between, FilterOperator.EqualTo, FilterOperator.GreaterThan, FilterOperator.GreaterThanOrEqualTo,
    //                         FilterOperator.LessThan, FilterOperator.LessThanOrEqualTo, FilterOperator.NotEqualTo)]
    //    Number,

    //    /// <summary>
    //    /// Supports boolean related FiltersDictionary.
    //    /// </summary>
    //    [SupportedOperations(FilterOperator.EqualTo, FilterOperator.NotEqualTo)]
    //    Boolean,

    //    /// <summary>
    //    /// Supports all date related FiltersDictionary.
    //    /// </summary>
    //    [SupportedOperations(FilterOperator.Between, FilterOperator.EqualTo, FilterOperator.GreaterThan, FilterOperator.GreaterThanOrEqualTo,
    //                         FilterOperator.LessThan, FilterOperator.LessThanOrEqualTo, FilterOperator.NotEqualTo)]
    //    Date,

    //    /// <summary>
    //    /// Supports nullable related FiltersDictionary.
    //    /// </summary>
    //    [SupportedOperations(FilterOperator.IsNotNull, FilterOperator.IsNull)]
    //    Nullable
    //}
}