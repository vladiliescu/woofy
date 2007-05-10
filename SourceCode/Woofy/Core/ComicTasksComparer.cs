using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public class ComicTasksComparer : IComparer<ComicTask>
    {
        #region IComparer<ComicTask> Members

        public int Compare(ComicTask x, ComicTask y)
        {
            return x.OrderNumber.CompareTo(y.OrderNumber);
        }

        #endregion
    }
}
