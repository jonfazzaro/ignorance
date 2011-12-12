using System;
using System.Linq.Expressions;

namespace Ignorance.Testing.Data.LinqToSql
{
    public class DepartmentStore : Ignorance.LinqToSql.Store<Department>
    {
        public DepartmentStore(Ignorance.LinqToSql.Work work = null) : base(work) { }
        
        protected override Expression<Func<Department, object>> Key
        {
            get { return p => p.DepartmentID; }
        }
    }
}
