using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotTest
{
    public class Persist : IPersist<Edge>
    {
        //private Dictionary<Location, Edge> _store;
        private List<Edge> _store; 
        public Persist()
        {
            //get from DB here
            _store = new List<Edge>();
        }
        //public Dictionary<Location, Edge> Fetch()
        //{
        //    //fetch from DB here
        //    Console.WriteLine("fetching from storage");
        //    return _store;
        //}

        public void Add(Edge value)
        {
            _store.Add(value);
            Console.WriteLine(string.Format("adding value {0} to storage",value));
        }

        public IQueryable<Edge> Query()
        {            
            return _store.AsQueryable();
        }
    }
}