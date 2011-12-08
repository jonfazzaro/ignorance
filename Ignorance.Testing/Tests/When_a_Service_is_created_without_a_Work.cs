using Ignorance.Testing.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ignorance.Testing
{
    [TestClass]
    public class When_a_Service_is_created_without_a_Work
    {
        [TestMethod]
        public void it_should_still_be_able_to_query_data()
        {
            var s = new DepartmentService();
            Assert.IsNotNull(s.GetFirst(), "Service did not create its own work when initialized without one.");
        }
    }
}
