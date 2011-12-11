using System.Data.Linq;
using System.Linq;
using System;
using System.Linq.Expressions;
using AutoMapper;

namespace Ignorance.LinqToSql
{
    public abstract class Store<T> : Ignorance.Store<T> where T : class
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
                
        protected virtual T GetAttachedEntity(T entity)
        {
            var key = GetKey.Invoke(entity);
            return this.Table.ToList().FirstOrDefault(p => GetKey.Invoke(p) == key);
        }

        public override void Remove(T entity)
        {
            var original = GetAttachedEntity(entity);
            if (original != null)
                this.Table.DeleteOnSubmit(original);
        }

        protected abstract Func<T, object> GetKey { get; }

        public override void Attach(T entity)
        {
            var original = GetAttachedEntity(entity);
            if (original != null)
                Mapper.Map<T, T>(entity, original);
        }

        public override void Add(T entity)
        {
            this.Table.InsertOnSubmit(entity);
        }
    }
}
