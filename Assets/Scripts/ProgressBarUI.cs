using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image progressBar;

    private void Start()
    {
        cuttingCounter.OnProgresChanged += CuttingCounter_OnProgresChanged;
        progressBar.fillAmount = 0f;

        Hide();
    }

    private void CuttingCounter_OnProgresChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
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
