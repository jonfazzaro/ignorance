using System.Configuration;
using System.Data.Entity;
using System.Data.Linq;
using Ignorance.Testing.Data.EntityFramework;
using Ignorance.Testing.Data.LinqToSql;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ignorance.Testing.Tests
{
    [TestClass]
    public class UnityConfiguration
    {
        [TestMethod]
        public void Load_EntityFramework_container()
        {
            var container = new UnityContainer()
                .RegisterType<IWork, Ignorance.EntityFramework.Work>()
                .RegisterType(typeof(IStore<>), typeof(Ignorance.EntityFramework.Store<>));

            Create.Container = (UnityContainer)container;

            Assert.IsTrue(true, "Failed to load the Entity Framework IoC Container.");
        }

        [TestMethod]
        public void Load_LinqToSql_container()
        {
            const string l2sConnectionStringName = 
                "Ignorance.Testing.Properties.Settings.AdventureWorksConnectionString";
            string l2sConnectionString = 
                ConfigurationManager.ConnectionStrings[l2sConnectionStringName].ConnectionString;

            // TODO: get the map filename

            var container = new UnityContainer()
                .RegisterType<IWork, Ignorance.LinqToSql.Work>(
                    new InjectionConstructor(new ParameterOverride("context", 
                        new AdventureWorksDataContext(l2sConnectionString))))
                .RegisterType(typeof(IStore<>), typeof(Ignorance.LinqToSql.Store<>));

            Create.Container = (UnityContainer)container;

            Assert.IsTrue(true, "Failed to load the LINQ to SQL IoC Container.");
        }
    }
}
