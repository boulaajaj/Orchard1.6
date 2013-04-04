using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotTest
{
    public class Persist<T> : IPersist<T>
    {
        private List<T> _store; 
        public Persist()
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
    }
}