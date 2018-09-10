using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Code.UI;
using Assets.Scripts.PlunderX;
using Assets.Scripts.Temp;
using Assets.Scripts.Temp.BowlX;
using Assets.Scripts.Temp.StoneX;
using Assets.Scripts.Timing;
using UnityEngine;
using Utf8Json;

public class SaveGame : MonoBehaviour
{
    public void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            SaveCurrentGame();
    }

    public void OnApplicationQuit()
    {
        SaveCurrentGame();
    }

    public void OnDestroy()
    {
        SaveCurrentGame();
    }

    private void SaveCurrentGame()
    {
        if (GameResources.Game.IsGameOver)
        {
            PlayerPrefs.DeleteKey(GameResources.Plunder.GameType.ToString());
            return;
        }
        var save = new Save
        {
            StoneData = GameResources.Game.UnsortedBowls.SelectMany(x => x.StoneIDs).Select(Access.Data<StoneData>).ToList(),
            BowlData = GameResources.Game.UnsortedBowls.Select(x => Access.Data<BowlData>(x.ID)).ToList(),
            MancalaData = Access.Data<MancalaData>(GameResources.Game.ID),
            StoneBodyData = GameResources.Game.UnsortedBowls.SelectMany(x => x.StoneIDs).Select(Access.BodyData<StoneBodyData>).ToList(),
            PlunderData = Access.Data<PlunderData>(GameResources.Plunder.ID),
            PlunderBodyData = Access.BodyData<PlunderBodyData>(GameResources.Plunder.ID),
            Queue = GameResources.Queue.Save()
        };
        PlayerPrefs.SetString(GameResources.Plunder.GameType.ToString(), JsonSerializer.ToJsonString(save));
        PlayerPrefs.Save();
    }
}

public class Save
{
    public List<StoneData> StoneData { get; set; }
    public List<BowlData> BowlData { get; set; }
    public MancalaData MancalaData { get; set; }
    public List<StoneBodyData> StoneBodyData { get; set; }
    public PlunderData PlunderData { get; set; }
    public PlunderBodyData PlunderBodyData { get; set; }
    public QueueSave Queue { get; set; }

    public void AddToAccess()
    {
        MancalaData.AddToAccess();
        StoneData.ForEach(x => x.AddToAccess());
        BowlData.ForEach(x => x.AddToAccess());
        StoneBodyData.ForEach(x => x.AddToAccess());
        PlunderData.AddToAccess();
        PlunderBodyData.AddToAccess();
    }
}
