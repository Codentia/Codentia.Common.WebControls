using System;
using System.Collections.Generic;
using System.Text;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// Enumerated type describing different paging operations which can be represented by PagingEventArgs
    /// </summary>
    public enum PagingOperationType
    {
        /// <summary>
        /// Go to first page
        /// </summary>
        First,

        /// <summary>
        /// Go to previous page
        /// </summary>
        Previous,

        /// <summary>
        /// Go to next page
        /// </summary>
        Next,

        /// <summary>
        /// Go to last page
        /// </summary>
        Last,

        /// <summary>
        /// Items per page amended
        /// </summary>
        ItemsPerPageChange
    }

    /// <summary>
    /// Event Arguments class describing a Paging Event (PagingToolbar)
    /// </summary>
    public class PagingEventArgs : EventArgs
    {
        private PagingOperationType _eventType;
        private int _currentPage;
        private int _totalPages;
        private int _itemsPerPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagingEventArgs"/> class.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalPages">The total pages.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="eventType">Type of the event.</param>
        public PagingEventArgs(int currentPage, int totalPages, int itemsPerPage, PagingOperationType eventType)
        {
            _currentPage = currentPage;
            _totalPages = totalPages;
            _itemsPerPage = itemsPerPage;
            _eventType = eventType;
        }

        /// <summary>
        /// Gets the current page
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
        }

        /// <summary>
        /// Gets the total pages
        /// </summary>
        public int TotalPages
        {
            get
            {
                return _totalPages;
            }
        }

        /// <summary>
        /// Gets the number of items per page
        /// </summary>
        public int ItemsPerPage
        {
            get
            {
                return _itemsPerPage;
            }
        }

        /// <summary>
        /// Gets the event type (PagingOperationType)
        /// </summary>
        public PagingOperationType EventType
        {
            get
            {
                return _eventType;
            }
        }
    }
}
