using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotTest
{
    public class Persist : IPersist<WorldEdge>
    {
        private List<WorldEdge> _store; 
        public Persist()
        {
            //get from DB here
            _store = new List<WorldEdge>();
        }
       

        public void Add(WorldEdge value)
        {
            _store.Add(value);
            Console.WriteLine(string.Format("adding value {0} to storage",value));
        }

        public IQueryable<WorldEdge> Query()
        {            
            return _store.AsQueryable();
        }
    }
}