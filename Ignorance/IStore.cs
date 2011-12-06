using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ignorance
{
    /// <summary>
    /// An interface describing a Repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStore<T> where T : class
    {
        /// <summary>
        /// Gets queryable access to the entire 
        /// pile of entities in the repository. I'd say
        /// "the whole table of records", but remember, 
        /// I'm not sure whether you're using a relational database
        /// or a five-gallon bucket with a faint fishy aroma.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> All();

        /// <summary>
        /// Pares the whole set of tuples down 
        /// some, using the given expression.
        /// </summary>
        IQueryable<T> Some(Expression<Func<T, bool>> where);

        /// <summary>
        /// Using the given expression, finds that one 
        /// entity you really care about.
        /// </summary>
        T One(Expression<Func<T, bool>> where);

        /// <summary>
        /// Removes the given entity from the repository,
        /// and marks it for deletion from storage.
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);
        
        /// <summary>
        /// Adds the given entity to the
        /// repository, to be persisted at the thrilling 
        /// conclusion of the current unit of work.
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Attaches the given entity and its changes to the
        /// repository, to be persisted at the thrilling 
        /// conclusion of the current unit of work.
        /// </summary>
        /// <param name="entity"></param>
        void Attach(T entity);
    }
}
