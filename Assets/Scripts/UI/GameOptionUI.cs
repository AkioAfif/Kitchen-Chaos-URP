using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionUI : MonoBehaviour
{
    public static GameOptionUI Instance {  get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;


    private void Awake()
    {
        Instance = this;
        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.VolumeChange();
            updateVisual();
            
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.VolumeChange();
            updateVisual();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    public void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        updateVisual();
        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void updateVisual()
    {
        soundEffectText.text = "Sound Effect: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


}



