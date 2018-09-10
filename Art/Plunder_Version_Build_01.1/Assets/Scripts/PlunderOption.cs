using Assets.Scripts.Code.UI;
using UnityEngine;

public class PlunderOption : MonoBehaviour
{
    public GameObject X;

    public void Start()
    {
        X.SetActive(GameResources.Settings.IsPlunder);
    }

    public void ClickPlunder()
    {
        GameResources.Settings.IsPlunder = !GameResources.Settings.IsPlunder;
        X.SetActive(GameResources.Settings.IsPlunder);
    }
}
