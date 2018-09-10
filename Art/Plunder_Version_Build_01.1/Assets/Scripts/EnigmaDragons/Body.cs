namespace Assets.Scripts.Temp.EnigmaDragons
{
    public abstract class Body
    {
        public string ID { get; }

        protected Body(string id)
        {
            ID = id;
        }
    }
}
