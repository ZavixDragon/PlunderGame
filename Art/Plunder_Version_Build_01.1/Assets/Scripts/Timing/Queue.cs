using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.BowlX;
using Assets.Scripts.Code.Common;
using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.UI;
using Assets.Scripts.PlunderX;
using Assets.Scripts.Sound;
using Assets.Scripts.Temp;
using Assets.Scripts.Temp.StoneX;
using UnityEngine;

namespace Assets.Scripts.Timing
{
    public class Queue
    {
        private const float _spawnDelay = 0.3f;
        private const float _moveStoneTime = 0.5f;
        private const float _notifyPlayerTime = 0.9f;
        private const float _flipBoardTime = 3;
        private const float _returnToBowlTime = 0.3f;

        private readonly Dictionary<Player, DictionaryWithDefault<Position, DateTimeOffset>> _spawningTimes =
            new Dictionary<Player, DictionaryWithDefault<Position, DateTimeOffset>>
            {
                { Player.One, new DictionaryWithDefault<Position, DateTimeOffset>(DateTimeOffset.MinValue) },
                { Player.Two, new DictionaryWithDefault<Position, DateTimeOffset>(DateTimeOffset.MinValue) }
            };

        private List<MoveStone> _moveStones = new List<MoveStone>();
        private List<SpawnStone> _spawnStones = new List<SpawnStone>();
        private List<NotifyPlayer> _playerNotifies = new List<NotifyPlayer>();
        private List<FlipBoard> _boardFlips = new List<FlipBoard>();
        private List<CoverBoard> _coverBoards = new List<CoverBoard>();
        private List<AIMove> _aiMoves = new List<AIMove>();
        private List<SwapPortrait> _swapPortraits = new List<SwapPortrait>(); 
        private List<ReturnToBowl> _returnToBowls = new List<ReturnToBowl>();

        public DateTimeOffset NextAvailableTime { get; private set; } = DateTimeOffset.MinValue;

        public Queue()
        {
            GameResources.Queue = this;
        }

        public void Update(float time)
        {
            Update(time, _moveStones);
            Update(time, _spawnStones);
            Update(time, _playerNotifies);
            Update(time, _boardFlips);
            Update(time, _coverBoards);
            Update(time, _aiMoves);
            Update(time, _swapPortraits);
            Update(time, _returnToBowls);
        }

        private void Update<T>(float time, List<T> commands) where T : QueueCommand
        {
            commands.Where(x => x.HasHadFirstUpdate).ForEach(x => x.SecondsRemaining -= time);
            commands.Where(x => !x.HasHadFirstUpdate).ForEach(x => x.HasHadFirstUpdate = true);
            commands.ToList().Where(x => x.SecondsRemaining <= 0).ForEach(x =>
            {
                x.Execute();
                commands.Remove(x);
            });
        }

        public QueueSave Save()
        {
            return new QueueSave
            {
                BoardFlips = _boardFlips,
                MoveStones = _moveStones,
                PlayerNotifies = _playerNotifies,
                SpawnStones = _spawnStones,
                CoverBoards = _coverBoards,
                AIMoves = _aiMoves,
                SwapPortraits = _swapPortraits,
                ReturnToBowls = _returnToBowls
            };
        }

        public void Load(QueueSave save)
        {
            _boardFlips = save.BoardFlips ?? new List<FlipBoard>();
            _moveStones = save.MoveStones ?? new List<MoveStone>();
            _playerNotifies = save.PlayerNotifies ?? new List<NotifyPlayer>();
            _spawnStones = save.SpawnStones ?? new List<SpawnStone>();
            _coverBoards = save.CoverBoards ?? new List<CoverBoard>();
            _aiMoves = save.AIMoves ?? new List<AIMove>();
            _swapPortraits = save.SwapPortraits ?? new List<SwapPortrait>();
            _returnToBowls = save.ReturnToBowls ?? new List<ReturnToBowl>();
        }

        public void MoveStone(string stoneID, string bowlID)
        {
            var time = new DateTimeOffset(new DateTime(Math.Max(NextAvailableTime.Ticks, DateTimeOffset.Now.Ticks)));
            NextAvailableTime = time.AddSeconds(_moveStoneTime);
            _moveStones.Add(new MoveStone
            {
                SecondsRemaining = GetTimeRemaining(time),
                StoneID = stoneID,
                BowlID = bowlID
            });
        }

        public void SpawnStone(string stoneID, string bowlID, Player owner, Position position)
        {
            var time = new DateTimeOffset(new DateTime(Math.Max(_spawningTimes[owner][position].AddSeconds(_spawnDelay).Ticks, DateTimeOffset.Now.Ticks)));
            _spawningTimes[owner][position] = time;
            NextAvailableTime = time > NextAvailableTime ? time : NextAvailableTime;
            _spawnStones.Add(new SpawnStone
            {
                SecondsRemaining = GetTimeRemaining(time),
                StoneID = stoneID,
                BowlID = bowlID
            });
        }

        public void NotifyPlayer(string message, SoundEffectControl soundEffect)
        {
            var time = new DateTimeOffset(new DateTime(Math.Max(NextAvailableTime.Ticks, DateTimeOffset.Now.Ticks)));
            NextAvailableTime = time.AddSeconds(_notifyPlayerTime);
            _playerNotifies.Add(new NotifyPlayer
            {
                SecondsRemaining = GetTimeRemaining(time),
                Message = message,
                SoundEffect = soundEffect
            });
        }

        public void FlipBoard(string plunderID)
        {
            var time = new DateTimeOffset(new DateTime(Math.Max(NextAvailableTime.Ticks, DateTimeOffset.Now.Ticks)));
            NextAvailableTime = time.AddSeconds(_flipBoardTime);
            _boardFlips.Add(new FlipBoard
            {
                SecondsRemaining = GetTimeRemaining(time),
                PlunderID = plunderID
            });
        }

        public void CoverBoard(string plunderID, Player playersTurn)
        {
            var time = new DateTimeOffset(new DateTime(Math.Max(NextAvailableTime.Ticks, DateTimeOffset.Now.Ticks)));
            _coverBoards.Add(new CoverBoard
            {
                SecondsRemaining = GetTimeRemaining(time.AddSeconds(-_flipBoardTime - 0.5f)),
                PlunderID = plunderID,
                PlayersTurn = playersTurn
            });
        }

        public void MakeAIMove()
        {
            var time = new DateTimeOffset(new DateTime(Math.Max(NextAvailableTime.Ticks, DateTimeOffset.Now.Ticks)));
            _aiMoves.Add(new AIMove
            {
                SecondsRemaining = GetTimeRemaining(time)
            });
        }

        public void SwapPortrait(Player target)
        {
            var time = new DateTimeOffset(new DateTime(Math.Max(NextAvailableTime.Ticks, DateTimeOffset.Now.Ticks)));
            _swapPortraits.Add(new SwapPortrait
            {
                SecondsRemaining = GetTimeRemaining(time),
                TargetSwap = target
            });
        }

        public void ReturnToBowl(string stoneID)
        {
            var time = new DateTimeOffset(new DateTime(Math.Max(NextAvailableTime.Ticks, DateTimeOffset.Now.Ticks)));
            NextAvailableTime = time.AddSeconds(_returnToBowlTime);
            _returnToBowls.Add(new ReturnToBowl
            {
                SecondsRemaining = GetTimeRemaining(time),
                ID = stoneID
            });
        }

        private float GetTimeRemaining(DateTimeOffset goTime)
        {
            return (float)(goTime.Ticks - DateTimeOffset.Now.Ticks) / 10000000;
        }
    }

    public sealed class MoveStone : QueueCommand
    {
        public string StoneID { get; set; }
        public string BowlID { get; set; }

        public override void Execute() => Access.Script<StoneScript>(StoneID).MoveTo(new Bowl(BowlID));
    }

    public sealed class SpawnStone : QueueCommand
    {
        public string StoneID { get; set; }
        public string BowlID { get; set; }

        public override void Execute() => GameResources
            .Stones[Mathf.Abs(StoneID.GetHashCode() % GameResources.Stones.Count)]
            .Spawn(new StoneBody(StoneID), Access.Script<BowlScript>(BowlID).transform);
    }

    public sealed class FlipBoard : QueueCommand
    {
        public string PlunderID { get; set; }

        public override void Execute() => new PlunderBody(PlunderID).Data.RotateAmount += 180;
    }

    public sealed class NotifyPlayer : QueueCommand
    {
        public string Message { get; set; }
        public SoundEffectControl SoundEffect { get; set; }

        public override void Execute() => GameResources.Notifications.ShowMessage(Message, SoundEffect);
    }

    public sealed class CoverBoard : QueueCommand
    {
        public string PlunderID { get; set; }
        public Player PlayersTurn { get; set; }

        public override void Execute() => Access.Script<PlunderScript>(PlunderID).LightingScript.LargeClouds.Block(PlayersTurn);
    }

    public sealed class AIMove : QueueCommand
    {
        public override void Execute() => GameResources.AI.MakeAMove(GameResources.Game);
    }

    public sealed class SwapPortrait : QueueCommand
    {
        public Player TargetSwap;

        public override void Execute() => (TargetSwap == Player.One ? GameResources.SwapOne : GameResources.SwapTwo).SwapPlaces();
    }

    public sealed class ReturnToBowl : QueueCommand
    {
        public string ID;

        public override void Execute() => Access.Script<StoneScript>(ID).StopPreviewing();
    }

    public abstract class QueueCommand
    {
        public float SecondsRemaining;
        public bool HasHadFirstUpdate = false;

        public abstract void Execute();
    }

    public class QueueSave
    {
        public List<MoveStone> MoveStones { get; set; }
        public List<SpawnStone> SpawnStones { get; set; }
        public List<NotifyPlayer> PlayerNotifies { get; set; }
        public List<FlipBoard> BoardFlips { get; set; }
        public List<CoverBoard> CoverBoards { get; set; }
        public List<AIMove> AIMoves { get; set; }
        public List<SwapPortrait> SwapPortraits { get; set; }
        public List<ReturnToBowl> ReturnToBowls { get; set; }
    }
}