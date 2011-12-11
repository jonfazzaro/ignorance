using System;

namespace Ignorance.Testing.Data.LinqToSql
{
    public class DepartmentStore : Ignorance.LinqToSql.Store<Department>
    {
        public DepartmentStore(Ignorance.LinqToSql.Work work = null) : base(work) { }
        
        protected override Func<Department, object> GetKey
        {
            get { return (p) => p.DepartmentID; }
        }
    }
}
