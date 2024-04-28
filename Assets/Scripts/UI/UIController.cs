using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class UIController : MonoBehaviour
{
    [SerializeField] private FullScreenPassRendererFeature _fullScreenPassRendererFeature;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject Menu;
    [SerializeField] public GameObject NoteObject;
    [SerializeField] private TMP_Text NoteText;
    [SerializeField] public GameObject NewspaperObject;
    [SerializeField] public GameObject Directory;
    [SerializeField] private TMP_Text NewspaperText;
    [SerializeField] private Animator BlackScreen;
    private PlayerInput playerInput;
    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }
    private void Awake()
    {
        slider.value = AudioListener.volume;
    }
    private void Update()
    {
        if(playerInput.EscButton && (Directory.activeSelf == false) && (NewspaperObject.activeSelf == false) && (NoteObject.activeSelf == false))
        {
            Menu.SetActive(!Menu.activeSelf);
        }
    }

    public void VolumeSlider()
    {
        AudioListener.volume = slider.value;
    }

    public void MainMenu()
    {
        _fullScreenPassRendererFeature.SetActive(false);
        SceneManager.LoadScene(0);
    }
    private void OnDisable()
    {
        _fullScreenPassRendererFeature.SetActive(false);
    }

    public void OpenNote(string text)
    {
        NoteObject.SetActive(true);
        NoteText.text = text;
    }

    public void CloseNote()
    {
        NoteObject.SetActive(false);
    }


    public void OpenDirectory()
    {
        Directory.SetActive(true);
    }

    public void CloseDirectory()
    {
        Directory.SetActive(false);
    }

    public void OpenNewspaper(string text)
    {
        NewspaperObject.SetActive(true);
        NewspaperText.text = text;
    }

    public void CloseNewspaper()
    {
        NewspaperObject.SetActive(false);
    }

    public void Blink()
    {
        BlackScreen.SetTrigger("Blink");
    }
}
