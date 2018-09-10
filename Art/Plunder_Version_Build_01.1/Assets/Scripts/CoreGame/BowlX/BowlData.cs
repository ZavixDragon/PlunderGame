using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.CoreGame.BowlX;
using Assets.Scripts.Temp.EnigmaDragons;

namespace Assets.Scripts.Temp.BowlX
{
    public class BowlData : Data
    {
        public PreviousBowlState _previousBowlState { get; set; }
        public Player Owner { get; set; }
        public Position Position { get; set; }
        public List<string> StoneIDs { get; set; }
        public List<string> PreviewStoneIDs { get; set; }

        //Serialization
        public BowlData() {}

        public BowlData(string id, Player owner, Position position, List<string> stoneIDs) : base(id)
        {
            Owner = owner;
            Position = position;
            StoneIDs = stoneIDs;
            PreviewStoneIDs = stoneIDs.ToList();
        }

        public void PrepForStateChange()
        {
            _previousBowlState = new PreviousBowlState(StoneIDs.ToList(), _previousBowlState);
        }

        public void Undo()
        {
            if (_previousBowlState == null)
                throw new InvalidOperationException("No actions to undo");
            StoneIDs = _previousBowlState.StoneIDs.ToList();
            _previousBowlState = _previousBowlState.PreviousState;
            PreviewStoneIDs = StoneIDs.ToList();
        }
    }
}
