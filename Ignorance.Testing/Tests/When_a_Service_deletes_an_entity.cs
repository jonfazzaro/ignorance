using Ignorance.Testing.Data.EntityFramework;
using Ignorance.Testing.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ignorance.Testing
{
    [TestClass]
    public class When_a_Service_deletes_an_entity : ServiceBehaviorTest
    {
        [TestMethod]
        public void it_can_come_from_the_current_Work()
        {
            // delete the test entity
            using (var work = Ignorance.Create.Work())
            {
                var s = new DepartmentService(work);
                var d = s.GetByID(this.Dept.DepartmentID);
                s.Delete(d);

                work.Save();
            }

            // verify its deletion
            using (var db = new AdventureWorksEntities())
            {
                var d = db.Departments.Find(this.Dept.DepartmentID);
                Assert.IsNull(d, "Entity was not deleted.");
            }
        }

        [TestMethod]
        public void it_can_come_from_outside_the_current_Work()
        {
            // delete the test entity
            using (var work = Ignorance.Create.Work())
            {
                var s = new DepartmentService(work);
                s.Delete(this.Dept);

                work.Save();
            }

            // verify its deletion
            using (var db = new AdventureWorksEntities())
            {
                var d = db.Departments.Find(this.Dept.DepartmentID);
                Assert.IsNull(d, "Entity was not deleted.");
            }
        }
        
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "OnDeleting was not called when deleting the entity.")]
        public void it_should_call_OnDeleting_first()
        {
            using (var work = Create.Work())
            {
                var s = new DepartmentService(work);
                var d = s.GetByID(this.Dept.DepartmentID);

                try
                {
                    // update the entity to something that will trip the OnDeleting rules
                    d.Name = "Department of Important Things DO NOT DELETE";
                    work.Save();

                    // now try to delete it
                    s.Delete(d);
                    work.Save();
                }
                finally
                {
                    // change it back so that tear down works
                    d.Name = this.Dept.Name;
                    work.Save();
                }
            }
        }
    }
}
