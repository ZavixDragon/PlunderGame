using System;
using Assets.Scripts.Code.UI;
using UnityEngine;

namespace Assets.Scripts.InGame
{
    public class MusicControl : MonoBehaviour
    {
        public AudioSource Audio;

        public void Update()
        {
            if (Math.Abs(Audio.volume - GameResources.AppSettings.MusicVolume) > 0.01f)
                Audio.volume = GameResources.AppSettings.MusicVolume;
            if (Audio.mute != !GameResources.AppSettings.IsMusicOn)
                Audio.mute = !GameResources.AppSettings.IsMusicOn;
        }
    }
}
