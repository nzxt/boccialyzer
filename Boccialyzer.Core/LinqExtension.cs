using Microsoft.EntityFrameworkCore;
using Boccialyzer.Domain.Models;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boccialyzer.Core
{
    public static class LinqExtension
    {
        #region # Task<PagedList<T>> Paged<T>(this IQueryable<T> query, int pageNumber, int pageSize, string filter, string order)

        /// <summary>
        /// Пагінація
        /// </summary>
        public static async Task<PagedList<T>> Paged<T>(this IQueryable<T> query, int pageNumber, int pageSize, string filter, string order) where T : class
        {
            if (!string.IsNullOrEmpty(filter))
            {
                LambdaExpression lambdaExpression = DynamicExpressionParser.ParseLambda(
                    typeof(T), typeof(bool),
                    filter.Trim());
                query = query.Where("@0(it)", lambdaExpression);
            }

            if (!string.IsNullOrEmpty(order.Trim())) query = query.OrderBy(order.Trim());

            var result = new PagedList<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                ItemCount = await query.CountAsync(),
                Items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
            };
            result.PageCount = (int)Math.Ceiling((double)result.ItemCount / pageSize);
            if (pageNumber == 1)
            {
                result.HasPreviousPage = false;
                result.IsFirstPage = true;
            }
            else
            {
                result.HasPreviousPage = true;
                result.IsFirstPage = false;
            }
            if (pageNumber == result.PageCount)
            {
                result.HasNextPage = false;
                result.IsLastPage = true;
            }
            else
            {
                result.HasNextPage = true;
                result.IsLastPage = false;
            }
            return result;
        }

        #endregion
        #region # int GetDaysLeft(this DateTime? dateTime)

        /// <summary>
        /// Отримати кількість днів, що залишилось
        /// </summary>
        public static int GetDaysLeft(this DateTime? dateTime)
        {
            if (!dateTime.HasValue) return 0;
            var subsDay = dateTime.Value.Date.Subtract(DateTime.UtcNow.Date).Days;
            if (subsDay > 0) return subsDay;
            return 0;
        }

        #endregion
        #region # int GetDaysInWork(this DateTime? dateTime)

        /// <summary>
        /// Отримати кількість днів в роботі
        /// </summary>
        public static int GetDaysInWork(this DateTime? dateTime)
        {
            var today = DateTime.Today.AddHours(9);
            var subsDay = today.Date.AddDays(1).Subtract(dateTime.Value).Days;
            return subsDay;
        }

        #endregion

        public static string TrimEnd(this string input, string suffixToRemove)
        {

            if (input != null && suffixToRemove != null && input.EndsWith(suffixToRemove, StringComparison.OrdinalIgnoreCase))
                return input.Substring(0, input.Length - suffixToRemove.Length);
            else return input;
        }
    }
}
