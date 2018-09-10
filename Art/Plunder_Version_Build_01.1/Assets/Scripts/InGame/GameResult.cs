using Assets.Scripts.Code.CoreGame;
using UnityEngine;

namespace Assets.Scripts.InGame
{
    public class GameResult : MonoBehaviour
    {
        public GameObject Victory;
        public GameObject Defeat;

        public void Resolve(Player winner)
        {
            if (winner == Player.One)
                Victory.SetActive(true);
            else
                Defeat.SetActive(true);
        }
    }
}
