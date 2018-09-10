using Assets.Scripts.Code.UI;
using UnityEngine;
using UnityEngine.UI;

public class DisplayErrorBehaviour : MonoBehaviour
{
    private float _timeRemaining;
    public Text Text;

    public void Update()
    {
        _timeRemaining -= Time.deltaTime;
        if (_timeRemaining <= 0)
            Text.text = "";
        if (_timeRemaining <= 0 && GameResources.ErrorMessage != "")
        {
            Text.text = GameResources.ErrorMessage;
            _timeRemaining = 1;
            GameResources.ErrorMessage = "";
        } 
    }
}
