using Blyzer.Domain.Filtering;
using Blyzer.Domain.Paging;
using Blyzer.Domain.Sorting;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Blyzer.Domain.Models;

namespace Blyzer.Domain.Extensions
{
    /// <summary>
    /// Common Repository LINQ Extentions
    /// </summary>
    public static class BaseRepositoryExtentions
    {
        #region # ApplyFiltering(List<FilterTerm> filters)

        /// <summary>
        /// Apply filters
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <param name="result"></param>
        /// <param name="filters">Filters list</param>
        /// <returns></returns>
        public static IQueryable<TEntity> ApplyFiltering<TEntity>(this IQueryable<TEntity> result, List<FilterTerm> filters)
        {
            if (filters == null) return result;

            var parameter = Expression.Parameter(typeof(TEntity), "e");
            Expression expression = null;

            foreach (var filterTerm in filters)
            {
                MemberExpression member = FilterHelper.GetMemberExpression(parameter, typeof(TEntity), filterTerm.Name);
                Expression innerExpression = FilterHelper.GetBodyExpression(member, filterTerm);

                if (innerExpression == null) continue;

                expression = expression == null
                    ? innerExpression
                    : Expression.And(expression, innerExpression);
            }
            return expression == null
                ? result
                : result.Where(Expression.Lambda<Func<TEntity, bool>>(expression, parameter));
        }

        #endregion

        #region # ApplySorting<TEntity>(List<SortTerm> sortTerms)

        public static IQueryable<TEntity> ApplySorting<TEntity>(this IQueryable<TEntity> result, List<SortTerm> sortTerms)
        {
            if (sortTerms == null) return result;

            var useThenBy = false;
            foreach (var sortTerm in sortTerms)
            {
                var propertyInfo = typeof(TEntity).GetProperties().FirstOrDefault(x => x.Name.ToUpper() == sortTerm.Name.ToUpper());
                if (propertyInfo != null)
                    result = result.OrderByDynamic(propertyInfo, sortTerm.Descending, useThenBy);
                useThenBy = true;
            }
            return result;
        }

        #endregion

        private static IQueryable<TEntity> OrderByDynamic<TEntity>(this IQueryable<TEntity> source
            , PropertyInfo propertyInfo
            , bool desc, bool useThenBy)
        {
            string command = desc ?
                (useThenBy ? "ThenByDescending" : "OrderByDescending") :
                (useThenBy ? "ThenBy" : "OrderBy");
            try
            {
                var type = typeof(TEntity);
                var parameter = Expression.Parameter(type, "p");

                dynamic propertyValue = parameter;
                propertyValue = Expression.PropertyOrField(propertyValue, propertyInfo.Name);

                var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                var resultExpression = Expression.Call(typeof(Queryable), command
                    , new Type[] { type, propertyInfo.PropertyType },
                    source.Expression, Expression.Quote(orderByExpression));
                return source.Provider.CreateQuery<TEntity>(resultExpression);
            }
            catch (Exception ex)
            {
                Log.Error("{Error}", ex.Message);
            }

            return source;
        }


        /// <summary>
        /// Apply Pagination
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="result"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<Page<TEntity>> ApplyPagination<TEntity>(this IQueryable<TEntity> result, RequestParametersModel model)
        {
            var page = model?.Page ?? 1;
            var pageSize = model?.PageSize ?? 25;
            var totalItem = await result.CountAsync();

            result = result.Skip((page - 1) * pageSize).Take(pageSize);

            return new Page<TEntity>(items: result, pageNumber: page, pageSize: pageSize, totalItem: totalItem);
        }


    }
}
