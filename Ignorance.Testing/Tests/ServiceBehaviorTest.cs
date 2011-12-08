using System;
using System.Linq;
using Ignorance.Testing.Data;
using Ignorance.Testing.Data.EntityFramework;
using Ignorance.Testing.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ignorance.Testing
{
    [TestClass]
    public class ServiceBehaviorTest
    {
        //public string GuidDeptName { get; set; }
        protected Department Dept { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            // create the test entity, using a guid string for its name
            using (var work = Ignorance.Create.Work())
            {
                var service = new DepartmentService(work);

                this.Dept = service.Create();
                this.Dept.Name = Guid.NewGuid().ToString();
                this.Dept.GroupName = Guid.NewGuid().ToString();
                service.Add(this.Dept);

                work.Save();
            }
        }
        
        [TestCleanup]
        public void TearDown()
        {
            // delete the test entity
            using (var db = new AdventureWorksEntities())
            {
                var d = db.Departments.FirstOrDefault(p => p.Name == this.Dept.Name);
                if (d != null)
                {
                    db.Departments.Remove(d);
                    db.SaveChanges();
                }
            }
        }
    }
}
