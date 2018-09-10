using System;

namespace Assets.Scripts.Temp.EnigmaDragons
{
    public abstract class BodyData : IDisposable
    {
        public string ID { get; set; }

        protected BodyData() {}

        protected BodyData(string id)
        {
            ID = id;
            AddToAccess();
        }

        public void AddToAccess()
        {
            Access.Add(this);
        }

        public void Dispose()
        {
            Access.Remove(this);
        }
    }
}
