using System;
using System.Collections.Generic;
using System.Linq;
using Ignorance.Domain;
using Ignorance.Testing.Data;

namespace Ignorance.Testing.Domain
{
    public class ContactService : Service<Contact>
    {
        public ContactService(IWork work) : base(work) { }

        protected override void OnCreated(Contact entity)
        {
            entity.FirstName = "New";
            entity.LastName = "Guy";
            entity.Suffix = "Esq.";
            entity.NameStyle = true;
            entity.PasswordHash = "Hash!";
            entity.PasswordSalt = "Pepper.";
        }

        protected override void OnSaving(Contact entity)
        {
            if (!entity.EmailAddress.Contains("@"))
                throw new ApplicationException("Invalid email address.");
        }

        protected override void OnDeleting(Contact entity)
        {
            if (entity.LastName == "Fazzaro")
                throw new ApplicationException("Don't delete me or my relatives!");
        }

        public IEnumerable<Contact> GetByLastName(string lastName)
        {
            var repo = GetStore();
            return repo.Some(p => 
                p.LastName.Equals(lastName, 
                    StringComparison.InvariantCultureIgnoreCase))
                .AsEnumerable();
        }

        public IEnumerable<Contact> GetContactsWithSuffixesBecauseTheyAreAwesome()
        {
            var repo = GetStore();
            return repo.Some(p => p.Suffix != null);
        }

        public void JustGoAheadAndMakeSureEveryoneHasSuffixesNow()
        {
            var repo = GetStore();
            var unsuffixedPeeps = repo.Some(p => p.Suffix == null);
            
            foreach (var peep in unsuffixedPeeps)
            {
                peep.Suffix = "Esq.";
            }
        }

        public int GetContactCount()
        {
            var repo = GetStore();
            return repo.All().Count();
        }
    }
}
