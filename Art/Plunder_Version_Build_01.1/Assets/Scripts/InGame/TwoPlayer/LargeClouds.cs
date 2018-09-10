using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.Message;
using Assets.Scripts.Code.UI;
using Assets.Scripts.PlunderX;
using UnityEngine;

public class LargeClouds : MonoBehaviour
{
    public Vector3 Hiding;
    public Vector3 Blocking;
    public bool IsBlocking;
    public float Speed;
    public bool IsOriginalPosition;

    public void Start()
    {
        IsOriginalPosition = GameResources.Game.PlayerToAct == Player.One;
        if (GameResources.Plunder.GameType == GameType.HotSeat)
        {
            Messages.ListenFor<TurnChanged>(x => GameResources.Queue.CoverBoard(GameResources.Plunder.ID, x.Player), this);
            Messages.ListenFor<GameFinished>(x => gameObject.SetActive(false), this);
        }
    }

    public void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, IsBlocking ? Blocking : Hiding, Speed * Time.deltaTime);
    }

    public void OnDestroy()
    {
        Messages.StopListening<TurnChanged>(this);
        Messages.StopListening<GameFinished>(this);
    }

    public void Block(Player playersTurn)
    {
        if ((IsOriginalPosition && playersTurn == Player.Two) || (!IsOriginalPosition && playersTurn == Player.One))
        {
            IsBlocking = true;
            IsOriginalPosition = !IsOriginalPosition;
        }
    }
}
