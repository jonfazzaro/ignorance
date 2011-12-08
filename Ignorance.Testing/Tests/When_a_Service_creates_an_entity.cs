using Ignorance.Testing.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ignorance.Testing
{
    [TestClass]
    public class When_a_service_creates_an_entity
    {
        [TestMethod]
        public void the_entity_should_not_be_null()
        {
            var s = new DepartmentService();
            var d = s.Create();
            Assert.IsNotNull(d, "The entity was not created.");
        }

        [TestMethod]
        public void the_entity_should_be_initialized_with_default_values()
        {
            var s = new DepartmentService();
            var d = s.Create();
            Assert.AreEqual("New Department", d.Name, "OnCreate was not called.");
        }

    }
}
