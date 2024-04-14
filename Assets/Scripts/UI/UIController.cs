using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject NoteObject;
    [SerializeField] private TMP_Text NoteText;
    [SerializeField] private GameObject NewspaperObject;
    [SerializeField] private TMP_Text NewspaperText;

    private PlayerInput playerInput;
    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    private void Update()
    {
        if(playerInput.EscButton)
        {
            Menu.SetActive(!Menu.activeSelf);
        }
    }

    public void VolumeSlider()
    {
        AudioListener.volume = slider.value;
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

    public void OpenNewspaper(string text)
    {
        NewspaperObject.SetActive(true);
        NewspaperText.text = text;
    }

    public void CloseNewspaper()
    {
        NewspaperObject.SetActive(false);
    }
}
