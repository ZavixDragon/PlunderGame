using System;
using Assets.Scripts.Code.UI;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    public class SoundEffectControl : MonoBehaviour
    {
        public AudioSource Audio;

        public void Update()
        {
            if (Math.Abs(Audio.volume - GameResources.AppSettings.SoundEffectsVolume) > 0.01f)
                Audio.volume = GameResources.AppSettings.SoundEffectsVolume;
            if (Audio.mute != !GameResources.AppSettings.IsSoundEffectsOn)
                Audio.mute = !GameResources.AppSettings.IsSoundEffectsOn;
        }

        public void Play()
        {
            Audio.Play();
        }
    }
}
