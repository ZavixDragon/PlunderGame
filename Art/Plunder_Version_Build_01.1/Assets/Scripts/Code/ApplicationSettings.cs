using UnityEngine;
using Utf8Json;

namespace Assets.Scripts.Code
{
    public class ApplicationSettings
    {
        private bool _isMusicOn;
        private float _musicVolume;
        private bool _isAmbienceOn;
        private float _ambienceVolume;
        private bool _isSoundEffectsOn;
        private float _soundEffectsVolume;

        public bool IsMusicOn
        {
            get { return _isMusicOn; }
            set
            {
                _isMusicOn = value;
                Save();
            }
        }

        public float MusicVolume
        {
            get { return _musicVolume; }
            set
            {
                _musicVolume = value;
                Save();
            }
        }

        public bool IsAmbienceOn
        {
            get { return _isAmbienceOn; }
            set
            {
                _isAmbienceOn = value;
                Save();
            }
        }

        public float AmbienceVolume
        {
            get { return _ambienceVolume; }
            set
            {
                _ambienceVolume = value;
                Save();
            }
        }

        public bool IsSoundEffectsOn
        {
            get { return _isSoundEffectsOn; }
            set
            {
                _isSoundEffectsOn = value;
                Save();
            }
        }

        public float SoundEffectsVolume
        {
            get { return _soundEffectsVolume; }
            set
            {
                _soundEffectsVolume = value;
                Save();
            }
        }

        public void Save()
        {
            PlayerPrefs.SetString("appSettings", JsonSerializer.ToJsonString(this));
        }

        public static ApplicationSettings Load()
        {
            return PlayerPrefs.HasKey("appSettings")
                ? JsonSerializer.Deserialize<ApplicationSettings>(PlayerPrefs.GetString("appSettings"))
                : new ApplicationSettings { _isMusicOn = true, _musicVolume = 1, _isAmbienceOn = true, _ambienceVolume = 1, _isSoundEffectsOn = true, _soundEffectsVolume = 1 };
        }
    }
}
