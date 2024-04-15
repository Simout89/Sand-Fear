using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ManipulatorRadio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private OneUseTrigger _oneUseTrigger;
    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _oneUseTrigger.OnTriggerEnterEvent += PlayWithStartCutScene;
    }

    private void PlayClip(int indexClip)
    {
        if(_audioClips[indexClip] != null)
        {
            _audioSource.PlayOneShot(_audioClips[indexClip]);
        }
    }

    public void PlayWithItemType(StashTrigger.TypeItem typeItem)
    {
        PlayClip((int)typeItem);
    }

    private void PlayWithStartCutScene()
    {
        PlayClip(2);
    }
}
