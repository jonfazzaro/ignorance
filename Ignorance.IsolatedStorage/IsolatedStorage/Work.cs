using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Json;

namespace Ignorance.IsolatedStorage
{
    public class Work : Ignorance.Work
    {
        public Dictionary<string, ICollection> Data { get; private set; }

        public void Load(string filename, Type type)
        {
            // load data from isolated storage
            var store = IsolatedStorageFile.GetUserStoreForApplication();

            // store it in Data[filename]
            var serializer = new DataContractJsonSerializer(type);
            using (var file = store.OpenFile(filename, FileMode.Create, FileAccess.Write))
            {
                var data = serializer.ReadObject(file);
                if (data is ICollection)
                    this.Data[filename] = data as ICollection;
            }
        }
        
        protected override void Commit()
        {
            var store = IsolatedStorageFile.GetUserStoreForApplication();

            foreach (var key in this.Data.Keys)
            {
                var data = this.Data[key];
                var serializer = new DataContractJsonSerializer(data.GetType());
                using (var file = store.OpenFile(key, FileMode.Create, FileAccess.Write))
                {
                    serializer.WriteObject(file, data);
                }
            }
        }

        public override void Dispose()
        {
            //throw new NotImplementedException();
        }

        public override ICollection Added
        {
            get 
            { throw new NotImplementedException(); }
        }

        public override ICollection Updated
        {
            get { throw new NotImplementedException(); }
        }

        public override ICollection Deleted
        {
            get { throw new NotImplementedException(); }
        }
    }
}
