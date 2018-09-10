using System;
using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.Message;
using Assets.Scripts.Code.UI;
using Assets.Scripts.InGame;
using Assets.Scripts.PlunderX;
using Assets.Scripts.Sound;
using UnityEngine;
using UnityEngine.UI;

public class NotificationBehaviour : MonoBehaviour, IPlayerNotifications
{
    private float _timeRemaining;
    [SerializeField]
    private bool _isGameOver = false;
    public Text Text;
    public GameResult GameResult;
    public SoundEffectControl ExtraTurn;
    public SoundEffectControl Capture;
    public SoundEffectControl Victory;
    public SoundEffectControl Defeat;
    public SoundEffectControl TurnChange;

    public void Start()
    {
        Messages.ListenFor<StonesCaptured>(x => GameResources.Queue.NotifyPlayer("Captured!", Capture), this);
        Messages.ListenFor<ExtraTurnGained>(x => GameResources.Queue.NotifyPlayer("Extra Turn!", ExtraTurn), this);
        Messages.ListenFor<TurnChanged>(x => GameResources.Queue.NotifyPlayer(
            GameResources.Plunder.GameType == GameType.SinglePlayer 
                ? x.Player == Player.One 
                    ? "Your Turn" 
                    : "Enemy's Turn" 
                : $"Player {(int)x.Player} Turn", TurnChange), this);
        Messages.ListenFor<GameFinished>(x => GameResources.Queue.NotifyPlayer(x.Winner.ToString(),
            x.Winner == Player.None || (GameResources.Plunder.GameType == GameType.SinglePlayer && x.Winner == Player.Two) ? Defeat : Victory), this);
        GameResources.Notifications = this;
    }

    public void Update()
    {
        _timeRemaining -= Time.deltaTime;
        if (_timeRemaining <= 0 && !_isGameOver)
            Text.text = "";
    }

    public void OnDestroy()
    {
        Messages.StopListening<StonesCaptured>(this);
        Messages.StopListening<ExtraTurnGained>(this);
        Messages.StopListening<TurnChanged>(this);
        Messages.StopListening<GameFinished>(this);
    }

    public void ShowMessage(string message, SoundEffectControl SoundEffect)
    {
        if (_isGameOver)
            return;
        SoundEffect.Play();
        Player winner;
        if (Enum.TryParse(message, out winner))
        {
            _isGameOver = true;
            GameResult.Resolve(winner);
        }
        else
        {
            Text.text = message;
            _timeRemaining = 0.5f;
        }
    }
}
