using System.Collections.Generic;

namespace RobotTest
{
    public interface IPersist<TKey, TVal>
    {
        Dictionary<TKey, TVal> Fetch();
        void Add(TKey key, TVal value);
    }
}