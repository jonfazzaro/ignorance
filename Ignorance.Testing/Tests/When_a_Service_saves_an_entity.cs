using System;
using Ignorance.Testing.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ignorance.Testing
{
    [TestClass]
    public class When_a_Service_saves_an_entity : ServiceBehaviorTest
    {
        const string bad_department_name = "Department of Torture";

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "OnSaving was not called when saving the entity.")]
        public void it_should_call_OnSaving_for_added_entities()
        {
            using (var work = Create.Work())
            {
                var s = new DepartmentService(work);
                var d = s.Create();
                d.Name = bad_department_name;
                s.Add(d);

                work.Save();
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "OnSaving was not called when saving the entity.")]
        public void it_should_call_OnSaving_for_updated_entities()
        {
            using (var work = Create.Work())
            {
                var s = new DepartmentService(work);
                var d = s.GetByID(this.Dept.DepartmentID);
                d.Name = bad_department_name;

                work.Save();
            }
        }
    }
}
