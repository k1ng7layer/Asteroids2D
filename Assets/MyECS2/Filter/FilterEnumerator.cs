using System.Collections;
using System.Collections.Generic;

namespace MyECS2
{
    internal class FilterEnumerator : IEnumerator<int>
    {
        public FilterEnumerator(IFilter filter)
        {
            _filter = filter;
            _entityCount = _filter.RegisteredEntitiesCount;
            _filter.Lock();
        }

        public int Current
        {
            get
            {
                return _position;
            }
        }

        object IEnumerator.Current => Current;

        private int _entityCount;
        private int _position = -1;
        private IFilter _filter;
        public void Dispose()
        {
            _filter.Unlock();
        }

        public bool MoveNext()
        {
            if (++_position < _entityCount)
            {               
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}
