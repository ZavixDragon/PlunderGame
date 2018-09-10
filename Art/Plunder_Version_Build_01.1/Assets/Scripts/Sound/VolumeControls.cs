using Assets.Scripts.Code.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.InGame
{
    public class VolumeControls : MonoBehaviour
    {
        public GameObject MusicOnButton;
        public GameObject MusicOffButton;
        public Slider MusicVolume;
        public GameObject AmbienceOnButton;
        public GameObject AmbienceOffButton;
        public Slider AmbienceVolume;
        public GameObject SoundEffectOnButton;
        public GameObject SoundEffectOffButton;
        public Slider SoundEffectVolume;
        public GameObject SoundOnButton;
        public GameObject SoundOffButton;

        public void Start()
        {
            MusicVolume.value = GameResources.AppSettings.MusicVolume;
            AmbienceVolume.value = GameResources.AppSettings.AmbienceVolume;
            SoundEffectVolume.value = GameResources.AppSettings.SoundEffectsVolume;
            UpdateState();
        }

        public void ToggleMusic()
        {
            GameResources.AppSettings.IsMusicOn = !GameResources.AppSettings.IsMusicOn;
            UpdateState();
        }

        public void ToggleAmbience()
        {
            GameResources.AppSettings.IsAmbienceOn = !GameResources.AppSettings.IsAmbienceOn;
            UpdateState();
        }

        public void ToggleSoundEffects()
        {
            GameResources.AppSettings.IsSoundEffectsOn = !GameResources.AppSettings.IsSoundEffectsOn;
            UpdateState();
        }

        public void MusicVolumeChange()
        {
            GameResources.AppSettings.MusicVolume = MusicVolume.value;
        }

        public void AmbienceVolumeChanged()
        {
            GameResources.AppSettings.AmbienceVolume = AmbienceVolume.value;
        }

        public void SoundEffectVolumeChange()
        {
            GameResources.AppSettings.SoundEffectsVolume = SoundEffectVolume.value;
        }

        public void ToggleSound()
        {
            var isTurningOn = !GameResources.AppSettings.IsMusicOn && !GameResources.AppSettings.IsAmbienceOn && !GameResources.AppSettings.IsSoundEffectsOn;
            GameResources.AppSettings.IsMusicOn = isTurningOn;
            GameResources.AppSettings.IsAmbienceOn = isTurningOn;
            GameResources.AppSettings.IsSoundEffectsOn = isTurningOn;
            UpdateState();
        }

        private void UpdateState()
        {
            MusicOnButton.SetActive(GameResources.AppSettings.IsMusicOn);
            MusicOffButton.SetActive(!GameResources.AppSettings.IsMusicOn);
            AmbienceOnButton.SetActive(GameResources.AppSettings.IsAmbienceOn);
            AmbienceOffButton.SetActive(!GameResources.AppSettings.IsAmbienceOn);
            SoundEffectOnButton.SetActive(GameResources.AppSettings.IsSoundEffectsOn);
            SoundEffectOffButton.SetActive(!GameResources.AppSettings.IsSoundEffectsOn);
            var isTurningOn = !GameResources.AppSettings.IsMusicOn && !GameResources.AppSettings.IsAmbienceOn && !GameResources.AppSettings.IsSoundEffectsOn;
            SoundOnButton.SetActive(!isTurningOn);
            SoundOffButton.SetActive(isTurningOn);
        }
    }
}
