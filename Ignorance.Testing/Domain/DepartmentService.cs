using System;
using System.Linq;
using Ignorance.Domain;
using Ignorance.Testing.Data;

namespace Ignorance.Testing.Domain
{
    public class DepartmentService : Service<Department>
    {
        public DepartmentService(IWork work = null) : base(work) { }

        protected override void OnDeleting(Department entity)
        {
            if (entity.Name.Contains("DO NOT DELETE"))
                throw new ApplicationException("Um.");
        }

        protected override void OnSaving(Department entity)
        {
            if (entity.Name.Contains("Torture"))
                throw new ApplicationException("That's not cool.");
        }

        protected override void OnCreated(Department entity)
        {
            entity.Name = "New Department";
            entity.ModifiedDate = DateTime.Today;
        }

        public Department GetByID(short id)
        {
            return GetStore().One(p => p.DepartmentID == id);
        }

        public Department GetFirst()
        {
            return GetStore().All().First();
        }
    }
}
