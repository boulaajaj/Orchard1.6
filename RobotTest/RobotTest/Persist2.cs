using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RobotTest
{
    public class Persist2<T> : IPersist<T>
    {
        private List<T> _store; 
        public Persist2()
        {
            //get from DB here
            _store = new List<T>();
        }
       

        public void Add(T value)
        {
            _store.Add(value);
            Console.WriteLine(string.Format("adding value {0} to storage",value));
        }

        public IQueryable<T> GetAll()
        {            
            return _store.AsQueryable();
        }

        public ObservableCollection<T> Updated()
        {
            var observableCollection = new ObservableCollection<T>();
            observableCollection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(observableCollection_CollectionChanged);
            throw new NotImplementedException();
        }

        void observableCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}