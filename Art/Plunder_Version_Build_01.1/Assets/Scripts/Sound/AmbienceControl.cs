using System;
using Assets.Scripts.Code.UI;
using UnityEngine;

namespace Assets.Scripts.InGame
{
    public class AmbienceControl : MonoBehaviour
    {
        public AudioSource Audio;

        public void Update()
        {
            if (Math.Abs(Audio.volume - GameResources.AppSettings.AmbienceVolume) > 0.01f)
                Audio.volume = GameResources.AppSettings.AmbienceVolume;
            if (Audio.mute != !GameResources.AppSettings.IsAmbienceOn)
                Audio.mute = !GameResources.AppSettings.IsAmbienceOn;
        }
    }
}
