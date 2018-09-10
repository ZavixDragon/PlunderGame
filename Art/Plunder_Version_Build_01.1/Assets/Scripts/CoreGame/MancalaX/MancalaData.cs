using System;
using System.Collections.Generic;
using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.CoreGame.MancalaX;
using Assets.Scripts.Temp.EnigmaDragons;

namespace Assets.Scripts.Temp
{
    public class MancalaData : Data
    {
        public PreviousMancalaState PreviousManclaState { get; set; }
        public Player PlayerToAct { get; set; }
        public List<string> BowlIDs { get; }
        public string LastBowlID { get; set; } = "";
        public Position LastPick { get; set; }
        public bool IsGameOver { get; set; }

        public bool IsPreviewing { get; set; } = false;
        public string PreviewPickBowlID { get; set; } = "";
        public string PreviewLastBowlID { get; set; } = "";
        public bool IsPreviewCapture { get; set; } = false;

        //Serialization
        public MancalaData() {}

        public MancalaData(string id, Player playerToAct, List<string> bowlIDs) : base(id)
        {
            PlayerToAct = playerToAct;
            BowlIDs = bowlIDs;
        }

        public void PrepForStateChange()
        {
            PreviousManclaState = new PreviousMancalaState(PlayerToAct, LastBowlID, LastPick, PreviousManclaState);
        }

        public void Undo()
        {
            if (PreviousManclaState == null)
                throw new InvalidOperationException("No actions to undo");
            PlayerToAct = PreviousManclaState.PlayerToAct;
            LastBowlID = PreviousManclaState.LastBowlID;
            IsPreviewing = false;
            LastPick = PreviousManclaState.LastPick;
            PreviousManclaState = PreviousManclaState.PreviousState;
            IsGameOver = false;
        }
    }
}
