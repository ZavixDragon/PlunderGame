using Assets.Scripts.Sound;

namespace Assets.Scripts.InGame
{
    public interface IPlayerNotifications
    {
        void ShowMessage(string text, SoundEffectControl soundEffect);
    }
}
