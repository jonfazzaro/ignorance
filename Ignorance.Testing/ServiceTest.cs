using System;
using System.Linq;
using Ignorance.Testing.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ignorance.Testing
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void OnCreateTest()
        {
            using (var context = Ignorance.Create.Work())
            {
                var service = new ContactService(context);

                var c = service.Create();
                var fullName = string.Format("{0} {1}", c.FirstName, c.LastName);

                Assert.AreEqual("New Guy", fullName);
            }
        }
        
        [TestMethod]
        public void CreateAddTest()
        {
            using (var context = Ignorance.Create.Work())
            {
                var service = new ContactService(context);

                var c = service.Create();
                c.FirstName = "Merlin";
                c.LastName = "Mann";
                c.Title = "Head of Hi. Can I Axe You A Question?";
                c.EmailAddress = "merlin@mann.com";
                service.Add(c);

                context.Save();

                Assert.AreNotEqual(0, c.ContactID);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (var work = Ignorance.Create.Work())
            {
                var service = new ContactService(work);
                var firstSuffixed = service.GetContactsWithSuffixesBecauseTheyAreAwesome().First();

                firstSuffixed.Suffix = null;

                var updatedCount = work.Updated.Count;
                work.Save();

                Assert.AreEqual(1, updatedCount, "Contact was not updated!");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Invalid email address.")]
        public void OnSavingTest()
        {
            using (var work = Ignorance.Create.Work())
            {
                var service = new ContactService(work);

                var newGuy = service.GetByLastName("Achong").LastOrDefault();
                newGuy.EmailAddress = "so angry";

                work.Save();
            }
        }

        [TestMethod]
        public void DeletingTest()
        {
            using (var work = Ignorance.Create.Work())
            {
                var service = new ContactService(work);

                var achong = service.GetByLastName("Achong").First();
                service.Delete(achong);

                work.Save();
            }
        }
    }
}
