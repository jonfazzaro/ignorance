namespace Ignorance
{
    public delegate void EntityEventHandler(object sender, EntityEventArgs e);
    public class EntityEventArgs : System.EventArgs
    {
        public object Entity { get; private set; }

        /// <summary>
        /// Initializes the EntityEventArgs.
        /// </summary>
        public EntityEventArgs(object entity)
        {
            this.Entity = entity;
        }
    }
}
