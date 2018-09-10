using System;
using Assets.Scripts;
using Assets.Scripts.Code.UI;
using Assets.Scripts.PlunderX;
using Assets.Scripts.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public BurnScreen TitleScreen;
    public BurnScreen MainMenu;
    public BurnScreen DifficultySelection;
    public BurnScreen GameSetup;
    public BurnScreen Options;
    public BurnScreen Busts;
    public SoundEffectControl ButtonSound;

    private BurnScreen _currentBurnScreen;

    [SerializeField]
    private Action _navigateBack = () => { };

    public void Start()
    {
        TitleScreen.gameObject.SetActive(true);
        _currentBurnScreen = TitleScreen;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            _navigateBack();
    }

    public void NavigateBack()
    {
        _navigateBack();
    }

    public void NavigateToTitleScreen()
    {
        ButtonSound.Play();
        TransitionTo(TitleScreen);
        _navigateBack = () => {};
    }

    public void NavigateToMainMenu()
    {
        ButtonSound.Play();
        TransitionTo(MainMenu);
        _navigateBack = NavigateToTitleScreen;
    }

    public void NavigateToVsAI()
    {
        GameResources.Settings.GameType = GameType.SinglePlayer;
        NavigateToDifficultySelection();
    }

    public void NavigateToHotSeat()
    {
        GameResources.Settings.GameType = GameType.HotSeat;
        NavigateToGameSetup();
    }

    public void NavigateToFaceOff()
    {
        GameResources.Settings.GameType = GameType.FaceOff;
        NavigateToGameSetup();
    }

    public void NavigateToDifficultySelection()
    {
        ButtonSound.Play();
        if (PlayerPrefs.HasKey(GameResources.Settings.GameType.ToString()))
        {
            GameResources.IsContinue = true;
            SceneManager.LoadScene(1);
        }
        TransitionTo(DifficultySelection);
        _navigateBack = NavigateToMainMenu;
    }

    public void NavigateToGameSetup()
    {
        ButtonSound.Play();
        if (PlayerPrefs.HasKey(GameResources.Settings.GameType.ToString()))
        {
            GameResources.IsContinue = true;
            SceneManager.LoadScene(1);
        }
        TransitionTo(GameSetup);
        _navigateBack = NavigateToMainMenu;
    }

    public void NavigateToOptions()
    {
        ButtonSound.Play();
        TransitionTo(Options);
        _navigateBack = NavigateToMainMenu;
    }

    public void NavigateToBusts()
    {
        ButtonSound.Play();
        TransitionTo(Busts);
        _navigateBack = NavigateToOptions;
    }

    public void SelectBust(Sprite bust)
    {
        ButtonSound.Play();
        PlayerPrefs.SetString("bust", bust.name);
        PlayerPrefs.Save();
        GameResources.SelectedBust = bust;
    }

    public void StartGame()
    {
        ButtonSound.Play();
        GameResources.IsContinue = false;
        SceneManager.LoadScene(1);
    }

    private void TransitionTo(BurnScreen screen)
    {
        screen.gameObject.transform.SetAsFirstSibling();
        screen.gameObject.SetActive(false);
        _currentBurnScreen.Transition();
        screen.gameObject.SetActive(true);
        _currentBurnScreen = screen;
    }
}
