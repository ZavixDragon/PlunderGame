using Assets.Scripts.Temp.EnigmaDragons;

namespace Assets.Scripts.PlunderX
{
    public class PlunderData : Data
    {
        public bool IsPlunder { get; set; }
        public GameType GameType { get; set; }

        //Serialization
        public PlunderData() {}

        public PlunderData(string id, bool isPlunder, GameType gameType) : base(id)
        {
            IsPlunder = isPlunder;
            GameType = gameType;
        }
    }
}
