using System;
using System.Collections;

namespace Ignorance
{
    /// <summary>
    /// An interface describing a Unit of Work.
    /// </summary>
    public interface IWork : IDisposable
    {
        ICollection Added { get; }
        ICollection Updated { get; }
        ICollection Deleted { get; }

        event EntityEventHandler Adding;
        event EntityEventHandler Updating;
        event EntityEventHandler Deleting;

        /// <summary>
        /// A collection of tasks accumulated during
        /// the unit of work that validate the changes 
        /// made to the data before they are committed.
        /// </summary>
        void OnCommitting();
        
        /// <summary>
        /// Executes validation tasks and
        /// commits the changes to storage.
        /// </summary>
        void Save();
    }
}
