using System.Linq;
using Ignorance.Testing.Data.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ignorance.Testing
{
    /// <summary>
    /// Summary description for ServiceAdd
    /// </summary>
    [TestClass]
    public class When_an_entity_is_added_and_saved : ServiceBehaviorTest
    {
        [TestMethod]
        public void it_should_persist_to_storage()
        {
            using (var db = new AdventureWorksEntities())
            {
                var d = db.Departments.FirstOrDefault(p => p.Name == this.Dept.Name);
                Assert.IsNotNull(d, "Entity was not created and saved.");
            }
        }

        [TestMethod]
        public void it_should_be_assigned_a_key_value()
        {
            Assert.AreNotEqual(0, this.Dept.DepartmentID, "Entity was not assigned an ID.");
        }
    }
}
