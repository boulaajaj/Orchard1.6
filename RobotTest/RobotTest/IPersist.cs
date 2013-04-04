using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RobotTest
{
    public interface IPersist<T>
    {        
        void Add(T value);

        IQueryable<T> GetAll();
    }
}