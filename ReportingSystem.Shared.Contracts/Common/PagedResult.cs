using System;
using System.Collections.Generic;

namespace ReportingSystem.Shared.Contracts.Common
{
    /// <summary>
    /// Specifies a generic, standardized structure for returning paginated data from list endpoints.
    /// </summary>
    /// <typeparam name="T">The type of the items in the paginated list.</typeparam>
    /// <param name="Items">Specifies the collection of items for the current page.</param>
    /// <param name="PageNumber">Specifies the current page number (1-based).</param>
    /// <param name="PageSize">Specifies the number of items per page.</param>
    /// <param name="TotalCount">Specifies the total number of items across all pages.</param>
    public record PagedResult<T>(
        IReadOnlyList<T> Items,
        int PageNumber,
        int PageSize,
        long TotalCount)
    {
        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>
        /// Gets a value indicating whether there is a previous page.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Gets a value indicating whether there is a next page.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;
    }
}