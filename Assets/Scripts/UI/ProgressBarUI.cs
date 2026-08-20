using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image progressBar;

    private IHasProgress hasProgress;
    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("GameObject " + hasProgressGameObject + " does not have a component that implements IHasProgress interface.");
        }
        hasProgress.OnProgresChanged += HasProgress_OnProgresChanged;
        progressBar.fillAmount = 0f;

        Hide();
    }

    private void HasProgress_OnProgresChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        progressBar.fillAmount = e.progressNormalized;
        if (progressBar.fillAmount == 0f || progressBar.fillAmount == 1f )
        {
            Hide();
        }
        else
        {
            Show();
        }
        
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
     
    }
}
