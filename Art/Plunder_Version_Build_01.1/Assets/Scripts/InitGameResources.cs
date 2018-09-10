using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.BowlX;
using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.UI;
using Assets.Scripts.PlunderX;
using Assets.Scripts.Sound;
using Assets.Scripts.Temp.StoneX;
using UnityEngine;

public class InitGameResources : MonoBehaviour
{
    public List<StoneScript> Stones;
    public List<WorldScript> Worlds;
    public List<SoundEffectControl> StoneDropSounds;

    public Material BowlIdleMaterial;
    public Material BowlHoverMaterial;
    public Material BowlAddedMaterial;
    public Material BowlCapturedMaterial;
    public Material BowlExtraTurnMaterial;
    public Material LastBowlMaterial;

    public List<Sprite> Busts;

    public BowlScript PlayerOneFirstBowl;
    public BowlScript PlayerOneSecondBowl;
    public BowlScript PlayerOneThirdBowl;
    public BowlScript PlayerOneFourthBowl;
    public BowlScript PlayerOneFifthBowl;
    public BowlScript PlayerOneSixthBowl;
    public BowlScript PlayerOneScoreBowl;
    public BowlScript PlayerTwoFirstBowl;
    public BowlScript PlayerTwoSecondBowl;
    public BowlScript PlayerTwoThirdBowl;
    public BowlScript PlayerTwoFourthBowl;
    public BowlScript PlayerTwoFifthBowl;
    public BowlScript PlayerTwoSixthBowl;
    public BowlScript PlayerTwoScoreBowl;

    public void Awake()
    {
        GameResources.Stones = Stones;
        GameResources.Worlds = Worlds;
        GameResources.StoneDropSounds = StoneDropSounds;
        GameResources.BowlIdleMaterial = BowlIdleMaterial;
        GameResources.BowlHoverMaterial = BowlHoverMaterial;
        GameResources.BowlAddedMaterial = BowlAddedMaterial;
        GameResources.BowlCapturedMaterial = BowlCapturedMaterial;
        GameResources.BowlExtraTurnMaterial = BowlExtraTurnMaterial;
        GameResources.LastBowlMaterial = LastBowlMaterial;
        GameResources.Busts = Busts;
        GameResources.Bowls = new Dictionary<Player, Dictionary<Position, BowlScript>>
        {
            {
                Player.One,
                new Dictionary<Position, BowlScript>
                {
                    { Position.First, PlayerOneFirstBowl },
                    { Position.Second, PlayerOneSecondBowl },
                    { Position.Third, PlayerOneThirdBowl },
                    { Position.Fourth, PlayerOneFourthBowl },
                    { Position.Fifth, PlayerOneFifthBowl },
                    { Position.Sixth, PlayerOneSixthBowl },
                    { Position.Score, PlayerOneScoreBowl },
                }
            },
            {
                Player.Two,
                new Dictionary<Position, BowlScript>
                {
                    { Position.First, PlayerTwoFirstBowl },
                    { Position.Second, PlayerTwoSecondBowl },
                    { Position.Third, PlayerTwoThirdBowl },
                    { Position.Fourth, PlayerTwoFourthBowl },
                    { Position.Fifth, PlayerTwoFifthBowl },
                    { Position.Sixth, PlayerTwoSixthBowl },
                    { Position.Score, PlayerTwoScoreBowl },
                }
            }
        };

        GameResources.SelectedBust = PlayerPrefs.HasKey("bust") 
            ? GameResources.Busts.First(x => x.name == PlayerPrefs.GetString("bust")) 
            : GameResources.Busts.First();
    }
}
