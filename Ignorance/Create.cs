using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Ignorance
{
    /// <summary>
    /// Me taking a swing at the factory pattern. I 
    /// thought the consolidation might make all this
    /// Inversion of Control go down a bit smoother.
    /// 
    /// Uses Unity to resolve types per the current 
    /// app's configuration.
    /// </summary>
    public static class Create
    {
        private static UnityContainer _container;
        /// <summary>
        /// Gets a shared instance of a 
        /// configured Unity Container.
        /// </summary>
        public static UnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new UnityContainer();

                    // apply configuration from app/web.config
                    UnityConfigurationSection section = 
                        (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                    section.Configure(_container);
                }
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        /// <summary>
        /// Creates an instance of IStore using 
        /// the given IWork.
        /// </summary>
        public static IStore<T> Store<T>(IWork work) where T : class
        {
            return Container.Resolve<IStore<T>>(
                new ParameterOverride("work", work));
        }

        /// <summary>
        /// Creates an instance of IWork.
        /// </summary>
        /// <returns></returns>
        public static IWork Work()
        {
            return Container.Resolve<IWork>();
        }
    }
}
