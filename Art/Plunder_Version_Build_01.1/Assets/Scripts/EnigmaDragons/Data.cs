using System;

namespace Assets.Scripts.Temp.EnigmaDragons
{
    public abstract class Data : IDisposable
    {
        public string ID { get; set; }

        //Serialization
        protected Data() {}

        protected Data(string id)
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
