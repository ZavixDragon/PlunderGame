using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.Message;
using Assets.Scripts.Code.UI;
using Assets.Scripts.PlunderX;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    public void Start()
    {
        Messages.ListenFor<TurnChanged>(x =>
        {
            if (x.Player == Player.Two && GameResources.Game.Bowls[Player.Two].HasAvailableMove && GameResources.Plunder.GameType == GameType.SinglePlayer)
                GameResources.Queue.MakeAIMove();
        }, this);
        Messages.ListenFor<ExtraTurnGained>(x =>
        {
            if (GameResources.Game.PlayerToAct == Player.Two && GameResources.Game.Bowls[Player.Two].HasAvailableMove && GameResources.Plunder.GameType == GameType.SinglePlayer)
                GameResources.Queue.MakeAIMove();
        }, this);
    }

    public void OnDestroy()
    {
        Messages.StopListening<TurnChanged>(this);
        Messages.StopListening<ExtraTurnGained>(this);
    }
}
