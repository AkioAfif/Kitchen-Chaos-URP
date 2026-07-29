using System.Runtime.CompilerServices;
using UnityEngine;

public class SelectedCounterVisual: MonoBehaviour
{

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selectedGameObject;
    private void Start()
    {
        Player.Instance.OnSelectedCounter += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            Show();

        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        selectedGameObject.SetActive(true);
    }

    private void Hide()
    {
        selectedGameObject.SetActive(false);
    }
}

