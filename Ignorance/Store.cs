using System;
using System.Linq;

namespace Ignorance
{
    /// <summary>
    /// A partial implementation of a Repository. A class implementing this abstract 
    /// performs the mechanics of persisting an entity of type T to specific data storage.
    /// </summary>
    public abstract class Store<T> : IStore<T>
        where T : class
    {
        /// <summary>
        /// Creates a new repository using the given
        /// unit of work.
        /// </summary>
        /// <param name="work"></param>
        public Store(IWork work)
        {
            this.Work = work;
        }
        
        /// <summary>
        /// Gets or sets the unit of work 
        /// for the repository.
        /// </summary>
        protected IWork Work { get; private set; }

        /// <summary>
        /// Gets internal queryable access to the repository's data.
        /// </summary>
        protected abstract IQueryable<T> Data { get; }
                
        /// <summary>
        /// Gets queryable access to the entire 
        /// pile of entities in the repository. I'd say
        /// "the whole table of records", but remember, 
        /// I'm not sure whether you're using a relational database
        /// or a five-gallon bucket with a faint fishy aroma.
        /// </summary>
        public virtual IQueryable<T> All()
        {
            return this.Data;
        }

        /// <summary>
        /// Pares the whole set of tuples down 
        /// some, using the given expression.
        /// </summary>
        public virtual IQueryable<T> Some(System.Linq.Expressions.Expression<Func<T, bool>> where) 
        {
            return this.Data.Where(where);
        }

        /// <summary>
        /// Gets that one entity you really care 
        /// about, using the given expression.
        /// </summary>
        public virtual T One(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return this.Data.FirstOrDefault(where);
        }

        /// <summary>
        /// Makes really serious plans to persist 
        /// the given entity to storage, as soon as
        /// the Work is cool with that.
        /// </summary>
        public abstract void Attach(T entity);

        /// <summary>
        /// Removes the given entity 
        /// from the repository.
        /// </summary>
        public abstract void Remove(T entity);

        /// <summary>
        /// Adds the given entity to the repository.
        /// </summary>
        public abstract void Add(T entity);
    }
}
