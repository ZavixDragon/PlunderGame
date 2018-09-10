using System.Collections.Generic;

namespace Assets.Scripts.Code.Common
{
    public sealed class DictionaryWithDefault<Key, Value> : Dictionary<Key, Value>
    {
        private readonly Value _value;

        public DictionaryWithDefault(Value value)
        {
            _value = value;
        }

        public new Value this[Key key]
        {
            get { return ContainsKey(key) ? base[key] : _value; }
            set { base[key] = value; }
        }

        public new bool TryGetValue(Key key, out Value value)
        {
            var succeeded = base.TryGetValue(key, out value);
            if (!succeeded)
                value = _value;
            return succeeded;
        }
    }
}
