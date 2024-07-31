using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone : InteractableObject
{

    public AudioClip audioClip;

    private AudioSource _audioSource;

    private SubtitlesManager _subtitlesManager;



    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TriggerInteraction()
    {
        StartCoroutine(PickUpPhone());
    }

    private IEnumerator PickUpPhone()
    {
        _audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(audioClip.length);
        yield return StartCoroutine(_subtitlesManager.WriteSubtitles(8, 3f));
    }
}
