using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone : InteractableObject
{
    // Reference to the audio clip
    public AudioClip audioClip;

    // Reference to the audio source
    private AudioSource _audioSource;

    // Reference to the subtitles manager script
    private SubtitlesManager _subtitlesManager;



    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();
    }

    // Triggers the coroutine
    public override void TriggerInteraction()
    {
        StartCoroutine(PickUpPhone());
    }

    // Coroutine that triggers an audio clip and sets the correct subtitles
    private IEnumerator PickUpPhone()
    {
        _audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(audioClip.length);
        yield return StartCoroutine(_subtitlesManager.WriteSubtitles(8, 3f));
    }
}
