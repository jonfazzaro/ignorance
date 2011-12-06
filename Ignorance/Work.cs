using System.Collections;

namespace Ignorance
{
    /// <summary>
    /// A partial implementation of a Unit of Work. A class implementing this abstract 
    /// is responsible for maintaining a data context and executing operations on that context.
    /// </summary>
    public abstract class Work : IWork
    {
        /// <summary>
        /// Commits changes to data storage.
        /// </summary>
        protected abstract void Commit();

        /// <summary>
        /// Executes validation tasks and
        /// commits the changes to storage.
        /// </summary>
        public virtual void Save()
        {
            OnCommitting();
            Commit();
        }
        
        public virtual void OnCommitting()
        {
            foreach (var entity in Added)
                OnAdding(new EntityEventArgs(entity));

            foreach (var entity in Updated)
                OnUpdating(new EntityEventArgs(entity));

            foreach (var entity in Deleted)
                OnDeleting(new EntityEventArgs(entity));
        }
        
        public abstract void Dispose();

        public abstract ICollection Added { get; }

        public abstract ICollection Updated { get; }

        public abstract ICollection Deleted { get; }

        /// <summary>
        /// Raised when Adding an entity.
        /// </summary>
        public event EntityEventHandler Adding;
        protected void OnAdding(EntityEventArgs e)
        {
            if (Adding != null)
                Adding(this, e);
        }

        /// <summary>
        /// Raised when Updating an entity.
        /// </summary>
        public event EntityEventHandler Updating;
        protected void OnUpdating(EntityEventArgs e)
        {
            if (Updating != null)
                Updating(this, e);
        }

        /// <summary>
        /// Raised when Deleting an entity.
        /// </summary>
        public event EntityEventHandler Deleting;
        protected void OnDeleting(EntityEventArgs e)
        {
            if (Deleting != null)
                Deleting(this, e);
        }
    }
}
