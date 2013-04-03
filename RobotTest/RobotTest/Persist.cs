using System;
using System.Collections.Generic;

namespace RobotTest
{
    public class Persist : IPersist<Location, Command>
    {
        private Dictionary<Location, Command> _store;
        public Persist()
        {
            //get from DB here
            _store= new Dictionary<Location, Command>();
        }
        public Dictionary<Location, Command> Fetch()
        {
            //fetch from DB here
            Console.WriteLine("fetching from storage");
            return _store;
        }

        public void Add(Location key, Command value)
        {
            _store.Add(key,value);
            Console.WriteLine(string.Format("adding value {0} to storage with key {1}",value, key));
        }

     
    }
}