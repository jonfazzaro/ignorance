using System.Configuration;
using System.Data.Linq.Mapping;
using Ignorance.Testing.Data;
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
                .RegisterType(typeof(IStore<>), typeof(Ignorance.EntityFramework.Store<>))
                .RegisterType<IWork, Ignorance.EntityFramework.Work>(
                    new InjectionConstructor(typeof(AdventureWorksEntities)));

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

            // get the map
            var map = XmlMappingSource.FromXml(Properties.Resources.AdventureWorks);

            var container = new UnityContainer()
                .RegisterType<AdventureWorks>(
                    new InjectionConstructor(l2sConnectionString, map))
                .RegisterType<IWork, Ignorance.LinqToSql.Work>(
                    new InjectionConstructor(typeof(AdventureWorks)))
                .RegisterType(typeof(IStore<Department>),
                    typeof(Ignorance.Testing.Data.LinqToSql.DepartmentStore))
                .RegisterType(typeof(IStore<>), typeof(Ignorance.LinqToSql.Store<>));

            Create.Container = (UnityContainer)container;

            Assert.IsTrue(true, "Failed to load the LINQ to SQL IoC Container.");
        }
    }
}
