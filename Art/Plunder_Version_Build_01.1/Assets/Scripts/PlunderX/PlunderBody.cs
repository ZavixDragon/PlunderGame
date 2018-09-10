using Assets.Scripts.Temp;
using Assets.Scripts.Temp.EnigmaDragons;

namespace Assets.Scripts.PlunderX
{
    public class PlunderBody : Body
    {
        public Plunder Plunder { get; }
        public PlunderBodyData Data => Access.BodyData<PlunderBodyData>(ID);

        public PlunderBody(string id) : base(id)
        {
            Plunder = new Plunder(id);
        }
    }
}
