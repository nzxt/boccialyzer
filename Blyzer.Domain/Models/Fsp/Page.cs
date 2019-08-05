using System.Collections.Generic;

namespace Blyzer.Domain.Models.Fsp
{
    /// <summary>
    /// Entities page
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Page<TEntity>
    {
        /// <summary>
        /// Page constructor
        /// </summary>
        /// <param name="items">Entities list</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalItem">Total entities</param>
        public Page(IEnumerable<TEntity> items, int pageNumber, int pageSize,int totalItem)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItem = totalItem;
        }
        /// <summary>
        /// Entities list
        /// </summary>
        public IEnumerable<TEntity> Items { get; set; }
        /// <summary>
        /// Page number
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Total items
        /// </summary>
        public int TotalItem { get; set; }
        /// <summary>
        /// HasPreviousPage
        /// </summary>
        public bool HasPreviousPage => PageNumber != 1;
        /// <summary>
        /// HasNextPage
        /// </summary>
        public bool HasNextPage => PageNumber != TotalPage;
        /// <summary>
        /// Total page
        /// </summary>
        public int TotalPage => TotalItem / PageSize;
    }
}
