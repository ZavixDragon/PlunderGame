using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.Message;
using Assets.Scripts.Temp;
using Assets.Scripts.Temp.BowlX;
using Assets.Scripts.Temp.EnigmaDragons;
using Assets.Scripts.Temp.StoneX;

namespace Assets.Scripts.BowlX
{
    public class Bowl : Obj
    {
        public List<string> StoneIDs => Access.Data<BowlData>(ID).StoneIDs;
        public List<string> PreviewStoneIDs => Access.Data<BowlData>(ID).PreviewStoneIDs;
        public Position Position => Access.Data<BowlData>(ID).Position;
        public Player Owner => Access.Data<BowlData>(ID).Owner;

        public Bowl(string id) : base(id) {}

        public void AddStones(List<Stone> stones) => AddStones(stones.Select(x => x.ID).ToList());
        public void AddStones(List<string> stoneIDs)
        {
            stoneIDs.ToList().ForEach(AddStone);
        }

        public Bowl EmptyTo(List<Bowl> bowls)
        {
            EnsureCanEmptyTo(bowls);
            var lastBowl = PerformEmptying(bowls);
            return lastBowl;
        }

        public void AddStone(Stone stone) => AddStone(stone.ID);
        public void AddStone(string stoneID)
        {
            StoneIDs.Add(stoneID);
            new Stone(stoneID).StoneData.BowlID.Set(this, ID);
        }

        public void Capture(Bowl scoreBowl)
        {
            var count = StoneIDs.Count;
            scoreBowl.Capture(StoneIDs);
            StoneIDs.Clear();
            Messages.Send(new StonesCaptured(Owner, count));
        }

        public void Capture(List<string> stones)
        {
            AddStones(stones);
        }

        public void Score(Bowl scoreBowl)
        {
            scoreBowl.AddStones(StoneIDs);
            StoneIDs.Clear();
        }

        public Bowl PreviewEmptyTo(List<Bowl> bowls)
        {
            EnsureCanEmptyTo(bowls);
            var index = bowls.FindIndex(x => x.ID == ID);
            var stoneIds = StoneIDs.ToList();
            foreach (var stone in stoneIds)
            {
                index++;
                if (index == bowls.Count)
                    index = 0;
                bowls[index].PreviewAddStone(stone);
            }
            for (var i = 0; i < stoneIds.Count; i++)
            {
                var stoneBodyData = new StoneBody(stoneIds[i]).StoneData;
                stoneBodyData.SortingNumber = i;
                stoneBodyData.IsPreviewing.Set(this, true);
            }
            return bowls[index];
        }

        public void PreviewScore(Bowl scoreBowl)
        {
            scoreBowl.AddStones(StoneIDs);
            PreviewStoneIDs.Clear();
        }

        public void PreviewCapture(Bowl scoreBowl)
        {
            scoreBowl.PreviewCapture(PreviewStoneIDs);
            PreviewStoneIDs.Clear();
        }

        public void PreviewCapture(List<string> stones)
        {
            PreviewAddStones(stones);
        }

        public void PreviewAddStones(List<string> stoneIDs)
        {
            PreviewStoneIDs.ToList().ForEach(PreviewAddStone);
        }

        public void PreviewAddStone(string stoneID)
        {
            PreviewStoneIDs.Add(stoneID);
            new Stone(stoneID).StoneData.PreviewBowlID.Set(this, ID);
        }

        public void StopPreviewing()
        {
            PreviewStoneIDs.ForEach(x => new Stone(x).StoneData.PreviewBowlID.Set(this, new Stone(x).StoneData.BowlID.Value));
            PreviewStoneIDs.ForEach(x => new StoneBody(x).StoneData.IsPreviewing.Set(this, false));
            PreviewStoneIDs.Clear();
            PreviewStoneIDs.AddRange(StoneIDs);
        }

        private void EnsureCanEmptyTo(List<Bowl> bowls)
        {
            if (bowls.All(x => x.ID != ID))
                throw new ArgumentException("This bowl must be in the list of bowls to pass to");
            if (!StoneIDs.Any())
                throw new InvalidOperationException("This bowl is empty!");
        }

        private Bowl PerformEmptying(List<Bowl> bowls)
        {
            var index = bowls.FindIndex(x => x.ID == ID);
            var stoneIds = StoneIDs.ToList();
            StoneIDs.Clear();
            foreach (var stone in stoneIds)
            {
                index++;
                if (index == bowls.Count)
                    index = 0;
                bowls[index].AddStone(stone);
            }
            return bowls[index];
        }
    }
}
