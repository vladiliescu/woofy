using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace Woofy.DatabaseAccess
{
    public class DatabaseParameter
    {
        public DbParameter DbParameter { get; private set; }
        public ParameterPurposes Purpose { get; private set; }
        /// <summary>
        /// Only applies to OrderBy parameters.
        /// </summary>
        public bool? SortAscending { get; private set; }

        public DatabaseParameter(DbParameter dbParameter, ParameterPurposes purpose)
            : this(dbParameter, purpose, null)
        {
        }

        public DatabaseParameter(DbParameter dbParameter, ParameterPurposes purpose, bool? sortAscending)
        {
            DbParameter = dbParameter;
            Purpose = purpose;
            SortAscending = sortAscending;
        }
    }
}
