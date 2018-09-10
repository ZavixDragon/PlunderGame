using Assets.Scripts.BowlX;
using Assets.Scripts.Temp.EnigmaDragons;

namespace Assets.Scripts.Temp.StoneX
{
    public sealed class Stone : Obj
    {
        public StoneData StoneData => Access.Data<StoneData>(ID);
        public Bowl Bowl => new Bowl(StoneData.BowlID.Value);
        public Bowl PreviewBowl => new Bowl(StoneData.PreviewBowlID.Value);

        public Stone(string id) : base(id) {}
    }
}
