using Assets.Scripts.Temp;
using Assets.Scripts.Temp.EnigmaDragons;

namespace Assets.Scripts.PlunderX
{
    public class Plunder : Obj
    {
        public bool IsPlunder
        {
            get { return Access.Data<PlunderData>(ID).IsPlunder; }
            set { Access.Data<PlunderData>(ID).IsPlunder = value; }
        }

        public GameType GameType
        {
            get { return Access.Data<PlunderData>(ID).GameType; }
            set { Access.Data<PlunderData>(ID).GameType = value; }
        }

        public Plunder(string id) : base(id) {}
    }
}
