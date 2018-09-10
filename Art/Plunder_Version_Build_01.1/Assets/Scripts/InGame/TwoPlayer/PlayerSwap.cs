using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.Message;
using Assets.Scripts.Code.UI;
using Assets.Scripts.PlunderX;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSwap : MonoBehaviour
{
    [SerializeField]
    private bool _isOriginal = true;
    public string Text;
    public Text TextElement;
    public Image ImageElement;
    public PlayerSwap Swap;
    public Player Player;
    public Sprite Sprite => Player == Player.One ? GameResources.SelectedBust : GameResources.EnemyBust;

    public void Start()
    {
        if (Player == Player.One)
            GameResources.SwapOne = this;
        else
            GameResources.SwapTwo = this;
        if (GameResources.Plunder.GameType == GameType.SinglePlayer)
            return;
        Messages.ListenFor<TurnChanged>(x =>
        {
            if ((x.Player == Player.One && !_isOriginal) || (x.Player == Player.Two && _isOriginal))
                GameResources.Queue.SwapPortrait(Player);
        }, this);
    }

    public void OnDestroy()
    {
        Messages.StopListening<TurnChanged>(this);
    }

    public void SwapPlaces()
    {
        if (_isOriginal)
        {
            TextElement.text = Swap.Text;
            ImageElement.sprite = Swap.Sprite;
            _isOriginal = false;
        }
        else
        {
            TextElement.text = Text;
            ImageElement.sprite = Sprite;
            _isOriginal = true;
        }
    }
}
