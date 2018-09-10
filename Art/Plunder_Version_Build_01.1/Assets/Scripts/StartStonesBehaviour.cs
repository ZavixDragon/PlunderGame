using Assets.Scripts.Code.UI;
using UnityEngine;
using UnityEngine.UI;

public class StartStonesBehaviour : MonoBehaviour
{
    public Text Number;
    public Button IncrementButton;
    public Button DecrementButton;

    public void Start()
    {
        UpdateState();
    }

    public void Increment()
    {
        GameResources.Settings.StartingStones++;
        UpdateState();
    }

    public void Decrement()
    {
        GameResources.Settings.StartingStones--;
        UpdateState();
    }

    private void UpdateState()
    {
        Number.text = $"Stones: {GameResources.Settings.StartingStones}";
        IncrementButton.gameObject.SetActive(GameResources.Settings.StartingStones != 9);
        DecrementButton.gameObject.SetActive(GameResources.Settings.StartingStones != 1);
    }
}
