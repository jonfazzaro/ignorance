using System.Data.Linq;
using System.Linq;

namespace Ignorance.LinqToSql
{
    public class Store<T> : Ignorance.Store<T> where T : class
    {
        public Store(Work work) : base(work) { }

        protected virtual DataContext Context
        {
            get { return (this.Work as LinqToSql.Work).DataContext; }
        }

        protected override IQueryable<T> Data
        {
            get { return this.Table as IQueryable<T>; }
        }

        protected virtual Table<T> Table
        {
            get { return this.Context.GetTable<T>(); }
        }
                
        public override void Remove(T entity)
        {
            this.Table.DeleteOnSubmit(entity);
        }

        public override void Attach(T entity)
        {
            this.Table.Attach(entity, true);
        }

        public override void Add(T entity)
        {
            this.Table.InsertOnSubmit(entity);
        }
    }
}
