﻿using System;
using System.Linq;
using Ignorance.Testing.Data;
using Ignorance.Testing.Data.EntityFramework;
using Ignorance.Testing.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ignorance.Testing
{
    [TestClass]
    public class IncompleteWork
    {
        public short TestDepartmentID { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            // create a record using straight EF
            using (var db = new AdventureWorksEntities())
            {
                var d = new Department() { 
                     Name = "Ignorance",
                     GroupName = "Information Technology",
                     ModifiedDate = DateTime.Today
                };
                db.Departments.Add(d);
                db.SaveChanges();

                this.TestDepartmentID = d.DepartmentID;
            }
        }


        [TestMethod]
        public void IncompleteAddTest()
        {
            var guidName = Guid.NewGuid().ToString();
            // using Ignorance/Service API, call Update on the record
            using (var work = Ignorance.Create.Work())
            {
                var s = new DepartmentService(work);
                var d = s.Create();
                d.Name = guidName;
                s.Add(d);

                // but do NOT call Save on Work.
            }

            // test (using straight EF) that the record still exists.
            using (var db = new AdventureWorksEntities())
            {
                var d = db.Departments.FirstOrDefault(p => p.Name == guidName);
                Assert.IsNull(d, "Add was persisted without Work.Save().");
            }
        }

        [TestMethod]
        public void IncompleteUpdateTest()
        {
            // using Ignorance/Service API, call Update on the record
            using (var work = Ignorance.Create.Work())
            {
                var s = new DepartmentService(work);
                var d = s.GetByID(this.TestDepartmentID);
                d.Name = "Updated";
                s.Update(d);

                // but do NOT call Save on Work.
            }

            // test (using straight EF) that the record still exists.
            using (var db = new AdventureWorksEntities())
            {
                var d = db.Departments.Find(this.TestDepartmentID);
                Assert.AreEqual("Ignorance", d.Name, "Update was persisted without Work.Save().");
            }
        }

        [TestMethod]
        public void IncompleteDeleteTest()
        {
            // using Ignorance/Service API, call Delete on the record
            using (var work = Ignorance.Create.Work())
            {
                var s = new DepartmentService(work);
                var d = s.GetByID(this.TestDepartmentID);
                s.Delete(d);

                // but do NOT call Save on Work.
            }

            // test (using straight EF) that the record still exists.
            using (var db = new AdventureWorksEntities())
            {
                var d = db.Departments.Find(this.TestDepartmentID);
                Assert.IsNotNull(d, "Changes were persisted without Work.Save().");
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            // delete the test record using straight EF
            using (var db = new AdventureWorksEntities())
            {
                var d = db.Departments.Find(this.TestDepartmentID);
                db.Departments.Remove(d);
                db.SaveChanges();
            }
        }
    }
}
