using Assets.Scripts.Temp.EnigmaDragons;

namespace Assets.Scripts.Temp.StoneX
{
    public sealed class StoneBody : Body
    {
        public Stone Stone { get; }
        public StoneBodyData StoneData => Access.BodyData<StoneBodyData>(ID);

        public StoneBody(string id) : base(id)
        {
            Stone = new Stone(id);
        }
    }
}
