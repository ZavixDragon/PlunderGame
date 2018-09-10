using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.BowlX;
using Assets.Scripts.Code.Common;
using Assets.Scripts.Code.Message;
using Assets.Scripts.Temp;
using Assets.Scripts.Temp.BowlX;
using Assets.Scripts.Temp.EnigmaDragons;
using Assets.Scripts.Temp.StoneX;

namespace Assets.Scripts.Code.CoreGame
{
    public class Mancala : Obj
    {
        public Player OpposingPlayer => PlayerToAct == Player.One ? Player.Two : Player.One;
        public List<Bowl> UnsortedBowls => Access.Data<MancalaData>(ID).BowlIDs.Select(x => new Bowl(x)).ToList();
        public GameBowls Bowls { get; }

        public Mancala(string id) : base(id)
        {
            Bowls = new GameBowls(new List<PlayerBowls>
            {
                new PlayerBowls(this, Player.One),
                new PlayerBowls(this, Player.Two)
            });
        }

        public Player PlayerToAct
        {
            get { return Access.Data<MancalaData>(ID).PlayerToAct; }
            set { Access.Data<MancalaData>(ID).PlayerToAct = value; }
        }

        public Bowl LastBowl
        {
            get { return new Bowl(Access.Data<MancalaData>(ID).LastBowlID); }
            set { Access.Data<MancalaData>(ID).LastBowlID = value.ID; }
        }

        public Position LastPick
        {
            get { return Access.Data<MancalaData>(ID).LastPick; }
            set { Access.Data<MancalaData>(ID).LastPick = value; }
        }

        public bool IsGameOver
        {
            get { return Access.Data<MancalaData>(ID).IsGameOver; }
            set { Access.Data<MancalaData>(ID).IsGameOver = value; }
        }

        public bool IsPreviewing
        {
            get { return Access.Data<MancalaData>(ID).IsPreviewing; }
            set { Access.Data<MancalaData>(ID).IsPreviewing = value; }
        }

        public string PreviewPickID
        {
            get { return Access.Data<MancalaData>(ID).PreviewPickBowlID; }
            set { Access.Data<MancalaData>(ID).PreviewPickBowlID = value; }
        }

        public Bowl PreviewLastBowl
        {
            get { return new Bowl(Access.Data<MancalaData>(ID).PreviewLastBowlID); }
            set { Access.Data<MancalaData>(ID).PreviewLastBowlID = value.ID; }
        }

        public bool IsPreviewCapturing
        {
            get { return Access.Data<MancalaData>(ID).IsPreviewCapture; }
            set { Access.Data<MancalaData>(ID).IsPreviewCapture = value; }
        }

        public void Pick(Position position)
        {
            EnsureValidMove(position);
            PrepForStateChange();
            LastPick = position;
            Messages.Send(new MoveChosen(position));
            var bowlsToPassTo = GetBowlsToPassTo(PlayerToAct);
            LastBowl = Bowls[PlayerToAct][position].EmptyTo(bowlsToPassTo);
            CaptureIfApplicable();
            EndGameIfApplicable();
            if (!IsGameOver)
                ChangeTurnIfNeeded();
            StopPreviewing();
        }

        public void Preview(Position position)
        {
            StopPreviewing();
            PreviewPickID = Bowls[PlayerToAct][position].ID;
            EnsureValidMove(position);
            var bowlsToPassTo = GetBowlsToPassTo(PlayerToAct);
            PreviewLastBowl =  Bowls[PlayerToAct][position].PreviewEmptyTo(bowlsToPassTo);
            PreviewCaptureIfApplicable();
            IsPreviewing = true;
        }

        public void StopPreviewing()
        {
            PreviewPickID = "";
            IsPreviewCapturing = false;
            IsPreviewing = false;
            UnsortedBowls.ForEach(x => x.StopPreviewing());
        }

        public void Surrender(Player player)
        {
            IsGameOver = true;
            Messages.Send(new GameFinished(player == Player.One ? Player.Two : Player.One));
        }

        public void Undo()
        {
            Access.Data<MancalaData>(ID).Undo();
            UnsortedBowls.SelectMany(x => x.StoneIDs).ForEach(x =>
            {
                Access.Data<StoneData>(x).Undo();
                Access.BodyData<StoneBodyData>(x).Undo();
            });
            UnsortedBowls.ForEach(x => Access.Data<BowlData>(x.ID).Undo());
        }

        private void EnsureValidMove(Position position)
        {
            if (position == Position.Score)
                throw new ArgumentException("You can't take stones out of the score bowl!");
            if (!Bowls[PlayerToAct][position].StoneIDs.Any())
                throw new InvalidOperationException("This bowl is empty!");
        }

        private void PrepForStateChange()
        {
            Access.Data<MancalaData>(ID).PrepForStateChange();
            UnsortedBowls.SelectMany(x => x.StoneIDs).ForEach(x =>
            {
                Access.Data<StoneData>(x).PrepForStateChange();
                Access.BodyData<StoneBodyData>(x).PrepForStateChange();
            });
            UnsortedBowls.ForEach(x => Access.Data<BowlData>(x.ID).PrepForStateChange());
        }

        private List<Bowl> GetBowlsToPassTo(Player player)
        {
            var bowlsToPassTo = Bowls[player].Bowls;
            bowlsToPassTo.Add(Bowls[player][Position.Score]);
            bowlsToPassTo.AddRange(Bowls[player == Player.One ? Player.Two : Player.One].Bowls);
            return bowlsToPassTo;
        }

        private void CaptureIfApplicable()
        {
            if (LastBowl.Owner == PlayerToAct 
                && LastBowl.StoneIDs.Count == 1 
                && LastBowl.Position != Position.Score 
                && Bowls[OpposingPlayer][5 - (int)LastBowl.Position].StoneIDs.Count > 0)
            {
                LastBowl.Score(Bowls[PlayerToAct][Position.Score]);
                Bowls[OpposingPlayer][5 - (int)LastBowl.Position].Capture(Bowls[PlayerToAct][Position.Score]);
            }
        }

        private void PreviewCaptureIfApplicable()
        {
            if (PreviewLastBowl.Owner == PlayerToAct
                && PreviewLastBowl.PreviewStoneIDs.Count == 1
                && PreviewLastBowl.Position != Position.Score
                && Bowls[OpposingPlayer][5 - (int)PreviewLastBowl.Position].PreviewStoneIDs.Count > 0)
            {
                PreviewLastBowl.PreviewScore(Bowls[PlayerToAct][Position.Score]);
                Bowls[OpposingPlayer][5 - (int)PreviewLastBowl.Position].PreviewCapture(Bowls[PlayerToAct][Position.Score]);
                IsPreviewCapturing = true;
            }
        }

        private void ChangeTurnIfNeeded()
        {
            if (LastBowl.Position != Position.Score)
            {
                PlayerToAct = OpposingPlayer;
                Messages.Send(new TurnChanged(PlayerToAct));
            }
            else
                Messages.Send(new ExtraTurnGained());
        }

        private void EndGameIfApplicable()
        {
            var playerWillAct = LastBowl.Position != Position.Score ? OpposingPlayer : PlayerToAct;
            if (!Bowls[playerWillAct].HasAvailableMove)
            {
                Bowls[playerWillAct == Player.One ? Player.Two : Player.One].ScoreAll();
                if (Bowls[Player.One][Position.Score].StoneIDs.Count > Bowls[Player.Two][Position.Score].StoneIDs.Count)
                    Messages.Send(new GameFinished(Player.One));
                else if (Bowls[Player.One][Position.Score].StoneIDs.Count < Bowls[Player.Two][Position.Score].StoneIDs.Count)
                    Messages.Send(new GameFinished(Player.Two));
                else
                    Messages.Send(new GameFinished(Player.None));
                IsGameOver = true;
            }
        }

        public static Mancala NewGame(int startingStones)
        {
            var playerOneBowls = Enum.GetValues(typeof(Position)).Cast<Position>().Select(x => new BowlData(Guid.NewGuid().ToString(), Player.One, x, new List<string>()));
            var playerTwoBowls = Enum.GetValues(typeof(Position)).Cast<Position>().Select(x => new BowlData(Guid.NewGuid().ToString(), Player.Two, x, new List<string>()));
            var bowls = playerOneBowls.Concat(playerTwoBowls).ToList();
            bowls.Where(x => x.Position != Position.Score)
                .SelectMany(bowl => Enumerable.Range(0, startingStones)
                .Select(_ =>
                {
                    var id = Guid.NewGuid().ToString();
                    bowl.StoneIDs.Add(id);
                    return new StoneData(id, bowl.ID);
                }))
                .ToList();
            var mancala = new MancalaData(Guid.NewGuid().ToString(), Player.One, bowls.Select(x => x.ID).ToList());
            return new Mancala(mancala.ID);
        } 
    }
}
