using System;
using System.Data.Linq;
using System.Linq;
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

        protected abstract Expression<Func<T, object>> Key { get; }

        protected virtual T GetAttachedEntity(T entity)
        {
            var key = Key.Compile().Invoke(entity);
            Expression<Func<T, bool>> exp = (p) => Key.Compile().Invoke(p).Equals(key);

            return this.Table.SingleOrDefault(exp.Compile());
        }

        public override void Remove(T entity)
        {
            var original = GetAttachedEntity(entity);
            if (original != null)
                this.Table.DeleteOnSubmit(original);
        }
        
        public override void Attach(T entity)
        {
            var original = GetAttachedEntity(entity);
            if (original != null)
            {
                Mapper.CreateMap<T, T>().ForMember(Key, o => o.Ignore());
                Mapper.AssertConfigurationIsValid();
                Mapper.Map<T, T>(entity, original);
            }
        }

        public override void Add(T entity)
        {
            this.Table.InsertOnSubmit(entity);
        }
    }
}
