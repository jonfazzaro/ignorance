using System;
using System.Collections.Generic;
using System.Linq;

namespace Ignorance.IsolatedStorage
{
    public abstract class Store<T> : Ignorance.Store<T> where T : class
    {
        public Store(Work work) : base(work) { }

        protected abstract string Filename { get; }

        protected override IQueryable<T> Data
        { 
            get {
                return this.File as IQueryable<T>;
            } 
        }

        protected ICollection<T> File
        {
            get
            {
                return (this.Work as Work).Data[this.Filename] as ICollection<T>;
            }
        }

        public abstract Func<T, object> GetKey { get; }
        
        public override void Remove(T entity)
        {
            this.File.Remove(entity);
        }
        
        public override void Add(T entity)
        {
            this.File.Add(entity);
        }

        public override void Attach(T entity)
        {
            // find match for entity & remove it
            T existing = One(p => GetKey.Invoke(entity) == GetKey.Invoke(p));
            this.File.Remove(existing);
            this.File.Add(entity);
        }
    }
}
