using System.Runtime.CompilerServices;
using UnityEngine;

public class SelectedCounterVisual: MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedGameObjectArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounter += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
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
        foreach (GameObject selectedGameObject in selectedGameObjectArray)
        { 
            selectedGameObject.SetActive(true); 
        }
    }

    private void Hide()
    {
        foreach (GameObject selectedGameObject in selectedGameObjectArray)
        {
            selectedGameObject.SetActive(false);
        }
    }
}

