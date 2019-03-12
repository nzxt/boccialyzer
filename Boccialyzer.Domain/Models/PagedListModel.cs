using System.Collections.Generic;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Paged List
    /// </summary>
    /// <typeparam name="TEntity">TEntity</typeparam>
    public class PagedList<TEntity>
    {
        /// <summary>
        /// PagedList конструктор
        /// </summary>
        public PagedList()
        {
            PageCount = 0;
            PageNumber = 0;
            PageSize = 0;
            HasPreviousPage = false;
            HasNextPage = false;
            IsFirstPage = false;
            IsLastPage = false;
            ItemCount = 0;
        }
        /// <summary>
        /// Загальна кількіть сторінок результату
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// Номер поточної сторінки
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// Розмір сторінки (кількість записів на сторінці)
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Загальна кількість записів результату запиту
        /// </summary>
        public int ItemCount { get; set; }
        /// <summary>
        /// Чи існує попередня сторінка
        /// </summary>
        public bool HasPreviousPage { get; set; }
        /// <summary>
        /// Чи існує наступна сторінка
        /// </summary>
        public bool HasNextPage { get; set; }
        /// <summary>
        /// Чи поточна сторінка перша?
        /// </summary>
        public bool IsFirstPage { get; set; }
        /// <summary>
        /// Чи поточна сторінка остання?
        /// </summary>
        public bool IsLastPage { get; set; }
        /// <summary>
        /// Записи поточної сторінки
        /// </summary>
        public IEnumerable<TEntity> Items { get; set; }
    }
}
