using System.Collections.Generic;
using Assets.Scripts.BowlX;
using Assets.Scripts.Code.AILogic;
using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.InGame;
using Assets.Scripts.PlunderX;
using Assets.Scripts.Sound;
using Assets.Scripts.Temp.StoneX;
using Assets.Scripts.Timing;
using UnityEngine;

namespace Assets.Scripts.Code.UI
{
    public static class GameResources
    {
        public static List<StoneScript> Stones;
        public static List<WorldScript> Worlds;
        public static List<SoundEffectControl> StoneDropSounds;

        public static Dictionary<Player, Dictionary<Position, BowlScript>> Bowls;

        public static List<Sprite> Busts;

        public static Material BowlIdleMaterial;
        public static Material BowlHoverMaterial;
        public static Material BowlAddedMaterial;
        public static Material BowlCapturedMaterial;
        public static Material BowlExtraTurnMaterial;
        public static Material LastBowlMaterial;

        public static bool IsContinue;
        public static GameSettings Settings = GameSettings.Load();

        public static ApplicationSettings AppSettings = ApplicationSettings.Load();
        public static IPlayerNotifications Notifications;
        public static Queue Queue;
        public static Mancala Game;
        public static Plunder Plunder;
        public static Cameras Cameras = new Cameras();
        public static string ErrorMessage = "";
        public static AI AI = new ChaosAI();
        public static Sprite SelectedBust;
        public static Sprite EnemyBust;
        public static PlayerSwap SwapOne;
        public static PlayerSwap SwapTwo;
    }
}
