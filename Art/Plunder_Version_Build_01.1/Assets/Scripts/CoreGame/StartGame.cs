using System;
using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.UI;
using Assets.Scripts.CoreGame.MancalaX;
using Assets.Scripts.PlunderX;
using UnityEngine;
using Utf8Json;

namespace Assets.Scripts.CoreGame.StoneX
{
    public class StartGame : MonoBehaviour
    {
        public MancalaScript MancalaScript;
        public PlunderScript PlunderScript;

        public void Start()
        {
            if (GameResources.IsContinue)
                Continue();
            else 
                New();
        }

        private void New()
        {
            var mancala = Mancala.NewGame(GameResources.Settings.StartingStones);
            GameResources.Game = mancala;
            var mancalaScript = MancalaScript.Spawn(mancala);
            mancalaScript.transform.SetParent(transform);
            var plunder = new PlunderData(Guid.NewGuid().ToString(), GameResources.Settings.IsPlunder, GameResources.Settings.GameType);
            GameResources.Plunder = new Plunder(plunder.ID);
            new PlunderBodyData(plunder.ID, PlunderScript.WorldScript.transform.position, PlunderScript.WorldScript.transform.rotation);
            var plunderScript = PlunderScript.Spawn(new PlunderBody(plunder.ID));
            plunderScript.transform.SetParent(transform);
        }

        public void Continue()
        {
            var save = JsonSerializer.Deserialize<Save>(PlayerPrefs.GetString(GameResources.Settings.GameType.ToString()));
            save.AddToAccess();
            var mancala = new Mancala(save.MancalaData.ID);
            GameResources.Game = mancala;
            var mancalaScript = MancalaScript.Spawn(mancala);
            mancalaScript.transform.SetParent(transform);
            GameResources.Plunder = new Plunder(save.PlunderData.ID);
            var plunderScript = PlunderScript.Spawn(new PlunderBody(save.PlunderData.ID));
            plunderScript.transform.SetParent(transform);
            GameResources.Queue.Load(save.Queue);
        }
    }
}
