using System;
using System.Collections.Generic;
using System.Text;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// The PagingManager class encapsulates functionality required to handle paging a dataset.
    /// </summary>
    public class PagingManager : CECompositeControl
    {
        private bool _useItems = false;

        private int _currentPage = 1;
        private int _totalPages = 0;
        private int _itemsPerPage = 1;
        private int _totalItems = 0;

        private bool _changed = false;

        /// <summary>
        /// EventHandler used to raise a notification when the state of paging (e.g. number of pages) changes.
        /// </summary>
        public event EventHandler PagingStateChanged;

        /// <summary>
        /// Gets or sets a value indicating whether [use items].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use items]; otherwise, <c>false</c>.
        /// </value>
        public bool UseItems
        {
            get
            {
                return _useItems;
            }

            set
            {
                _useItems = value;

                UpdateCounters();
            }
        }

        /// <summary>
        /// Gets or sets the current Page number
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }

            set
            {
                _currentPage = value;

                UpdateCounters();
            }
        }

        /// <summary>
        /// Gets or sets the total number of pages
        /// </summary>
        public int TotalPages
        {
            get
            {
                return _totalPages;
            }

            set
            {
                _totalPages = value;

                UpdateCounters();
            }
        }

        /// <summary>
        /// Gets or sets the number of items to show per page
        /// </summary>
        public int ItemsPerPage
        {
            get
            {
                return _itemsPerPage;
            }

            set
            {
                _itemsPerPage = value;

                UpdateCounters();
            }
        }

        /// <summary>
        /// Gets or sets the total number of items
        /// </summary>
        public int TotalItems
        {
            get
            {
                return _totalItems;
            }

            set
            {
                if (_totalItems != value)
                {
                    _changed = true;
                }

                _totalItems = value;

                UpdateCounters();
            }
        }

        /// <summary>
        /// Save the state of this control to page viewstate
        /// </summary>
        /// <returns>The object</returns>
        protected override object SaveViewState()
        {
            object[] state = new object[4];
            state[0] = _currentPage;
            state[1] = _totalPages;
            state[2] = _itemsPerPage;
            state[3] = _totalItems;

            return (object)state;
        }

        /// <summary>
        /// Load the saved state of this control
        /// </summary>
        /// <param name="savedState">state to load</param>
        protected override void LoadViewState(object savedState)
        {
            object[] state = (object[])savedState;

            if (Convert.ToInt32(state[0]) != _currentPage)
            {
                _currentPage = Convert.ToInt32(state[0]);
                _changed = true;
            }

            _totalPages = Convert.ToInt32(state[1]);

            if (Convert.ToInt32(state[2]) != _itemsPerPage)
            {
                _itemsPerPage = Convert.ToInt32(state[2]);
                _changed = true;
            }

            if (Convert.ToInt32(state[3]) != _totalItems)
            {
                _totalItems = Convert.ToInt32(state[3]);
                _changed = true;
            }

            UpdateCounters();
        }
        
        private void UpdateCounters()
        {
            if (_useItems)
            {
                int oldTotalPages = _totalPages;
                int oldCurrentPage = _currentPage;

                if (_totalItems > 0 && _itemsPerPage > 0)
                {
                    _totalPages = (_totalItems / _itemsPerPage) + ((_totalItems % _itemsPerPage) > 0 ? 1 : 0);

                    if (_currentPage > _totalPages && _totalPages > 0)
                    {
                        _currentPage = _totalPages;
                    }

                    if (_currentPage < 0)
                    {
                        _currentPage = 1;
                    }
                }
                else
                {
                    _totalPages = 1;
                    _currentPage = 1;
                }

                if (_changed || oldTotalPages != _totalPages || oldCurrentPage != _currentPage)
                {
                    _changed = false;

                    if (PagingStateChanged != null)
                    {
                        PagingStateChanged(this, new EventArgs());
                    }
                }
            }
        }
    }
}
