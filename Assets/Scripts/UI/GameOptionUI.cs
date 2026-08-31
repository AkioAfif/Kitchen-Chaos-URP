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
  
    [SerializeField] private TextMeshProUGUI moveUptext;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pausetext;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private Transform presstoRebindKeyTransform;


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

        moveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        moveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        moveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Right);
        });
        interactButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        });
        interactAlternateButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact_Alternate);
        });
        pauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        });
    }

    public void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        updateVisual();
        HidePressTorebindKey();
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
        moveUptext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        pausetext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressTorebindKey()
    {
        presstoRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressTorebindKey()
    {
        presstoRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressTorebindKey();
        GameInput.Instance.Rebinding(binding, () => 
        {
            HidePressTorebindKey();
            updateVisual();
        });
    }
}



