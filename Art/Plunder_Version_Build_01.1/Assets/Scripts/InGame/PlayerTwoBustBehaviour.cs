using System.Linq;
using Assets.Scripts.Code.Common;
using Assets.Scripts.Code.UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTwoBustBehaviour : MonoBehaviour
{
    public Image Image;

    public void Start()
    {
        GameResources.EnemyBust = GameResources.Busts.Where(x => x != GameResources.SelectedBust).Random();
        Image.sprite = GameResources.EnemyBust;
    }
}
