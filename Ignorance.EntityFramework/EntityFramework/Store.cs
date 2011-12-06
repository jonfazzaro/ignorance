using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Ignorance.EntityFramework
{
    public class Store<T> : Ignorance.Store<T>
        where T : class
    {
        public Store(Work work) : base(work) { }

        protected virtual DbContext Context
        {
            get { return (this.Work as EntityFramework.Work).DataContext; }
        }

        protected override IQueryable<T> Data
        {
            get { return this.Set as IQueryable<T>; }
        }

        protected virtual DbSet<T> Set
        {
            get { return this.Context.Set<T>(); }
        }
                
        public override void Remove(T entity)
        {
            this.Set.Remove(entity);
        }
                
        public override void Add(T entity)
        {
            SetState(entity, EntityState.Added);
        }

        public override void Attach(T entity)
        {
            SetState(entity, EntityState.Modified);
        }

        protected virtual void SetState(T entity, EntityState state)
        {
            this.Context.Entry<T>(entity).State = state;
        }
    }
}
