using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Code.Common;

namespace Assets.Scripts.EnigmaDragons
{
    public class Notifying<T>
    {
        private readonly List<Reaction> _reactions = new List<Reaction>();
        private readonly Func<T> _getValue;
        private readonly Action<T> _setValue;

        public T Value => _getValue();

        public Notifying(Func<T> getValue, Action<T> setValue)
        {
            _getValue = getValue;
            _setValue = setValue;
        }

        public void Set(object setter, T value)
        {
            if (value.Equals(_getValue()))
                return;
            _setValue(value);
            _reactions.Where(x => x.Owner != setter).ForEach(x => x.OnChange(value));
        }

        public void OnChanged(Action<T> onChange, object owner)
        {
            _reactions.Add(new Reaction(onChange, owner));
        }

        public void RemoveChangeEvents(object owner)
        {
            _reactions.ToList().Where(x => x.Owner == owner).ForEach(x => _reactions.Remove(x));
        }

        private class Reaction
        {
            public Action<T> OnChange { get; }
            public object Owner { get; }

            public Reaction(Action<T> onChange, object owner)
            {
                OnChange = onChange;
                Owner = owner;
            }
        }
    }
}
