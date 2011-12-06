using System.Data.Linq;
using System.Collections;

namespace Ignorance.LinqToSql
{
    public class Work : Ignorance.Work
    {
        public DataContext DataContext { get; set; }

        public Work(DataContext context)
        {
            this.DataContext = context;
        }

        protected override void Commit()
        {
            this.DataContext.SubmitChanges();
        }

        public override void Dispose()
        {
            this.DataContext.Dispose();
        }

        public override ICollection Added
        {
            get
            {
                return this.DataContext.GetChangeSet().Inserts as ICollection;
            }
        }

        public override ICollection Updated
        {
            get
            {
                return this.DataContext.GetChangeSet().Updates as ICollection;
            }
        }

        public override System.Collections.ICollection Deleted
        {
            get
            {
                return this.DataContext.GetChangeSet().Deletes as ICollection;
            }
        }
    }
}
