using Assets.Scripts.Code.UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOneBustBehaviour : MonoBehaviour
{
    public Image Image;

    public void Start()
    {
        Image.sprite = GameResources.SelectedBust;
    }
}
