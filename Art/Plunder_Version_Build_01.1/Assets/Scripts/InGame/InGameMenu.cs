using Assets.Scripts.Code.UI;
using Assets.Scripts.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.InGame
{
    public class InGameMenu : MonoBehaviour
    {
        public BurnScreen Menu;
        public SoundEffectControl ButtonSound;

        public void ShowMenu()
        {
            ButtonSound.Play();
            gameObject.SetActive(true);
        }

        public void MainMenu()
        {
            ButtonSound.Play();
            SceneManager.LoadScene(0);
        }

        public void Surrender()
        {
            ButtonSound.Play();
            Menu.Transition();
            GameResources.Game.Surrender(GameResources.Game.PlayerToAct);
        }

        public void Return()
        {
            ButtonSound.Play();
            Menu.Transition();
        }
    }
}
