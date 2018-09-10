using Assets.Scripts.PlunderX;
using UnityEngine;
using Utf8Json;

namespace Assets.Scripts.Code
{
    public class GameSettings
    {
        private bool _isPlunder;
        private int _startingStones;
        private GameType _gameType;

        public bool IsPlunder
        {
            get { return _isPlunder; }
            set
            {
                _isPlunder = value;
                Save();
            }
        }

        public int StartingStones
        {
            get { return _startingStones; }
            set
            {
                _startingStones = value;
                Save();
            }
        }

        public GameType GameType
        {
            get { return _gameType; }
            set
            {
                _gameType = value;
                Save();
            }
        }

        public void Save()
        {
            PlayerPrefs.SetString("gameSettings", JsonSerializer.ToJsonString(this));
        }

        public static GameSettings Load()
        {
            return PlayerPrefs.HasKey("gameSettings") 
                ? JsonSerializer.Deserialize<GameSettings>(PlayerPrefs.GetString("gameSettings")) 
                : new GameSettings { _isPlunder = false, _startingStones = 4, _gameType = GameType.SinglePlayer };
        }
    }
}
