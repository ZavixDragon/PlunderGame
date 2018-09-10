using System;
using System.Linq;
using System.Runtime.Serialization;
using Assets.Scripts.CoreGame.StoneX;
using Assets.Scripts.EnigmaDragons;
using Assets.Scripts.Temp.EnigmaDragons;

namespace Assets.Scripts.Temp.StoneX
{
    public sealed class StoneData : Data
    {
        public PreviousStoneState _previousStoneState { get; set; }
        public string _bowlID { get; set; }
        public string _previewBowlID { get; set; }
        [IgnoreDataMember]
        public Notifying<string> BowlID { get; }
        [IgnoreDataMember]
        public Notifying<string> PreviewBowlID { get; }

        //Serialization
        public StoneData()
        {
            BowlID = new Notifying<string>(() => _bowlID, x => _bowlID = x);
            PreviewBowlID = new Notifying<string>(() => _previewBowlID, x => _previewBowlID = x);
        }

        public StoneData(string id, string bowlID) : base(id)
        {
            _bowlID = bowlID;
            _previewBowlID = bowlID;
            BowlID = new Notifying<string>(() => _bowlID, x => _bowlID = x);
            PreviewBowlID = new Notifying<string>(() => _previewBowlID, x => _previewBowlID = x);
        }

        public void PrepForStateChange()
        {
            _previousStoneState = new PreviousStoneState(_bowlID, _previousStoneState);
        }

        public void Undo()
        {
            if (_previousStoneState == null)
                throw new InvalidOperationException("No actions to undo");
            _bowlID = _previousStoneState.BowlID;
            _previousStoneState = _previousStoneState.PreviousState;
            _previewBowlID = _bowlID;
        }
    }
}
