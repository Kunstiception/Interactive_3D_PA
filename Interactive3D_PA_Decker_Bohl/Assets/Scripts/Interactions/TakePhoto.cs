using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : InteractableObject
{
    public AudioClip click;

    public AnimationClip fadeToBlack;

    public AnimationClip reversedFadeToBlack;

    public Collider photoSpot;

    private Animator _blackscreenAnimator;

    private AudioSource _audioSource;

    private PlayerMovement _playerMovement;

    private SubtitlesManager _subtitlesManager;

    private Progression _progression;

    // Start is called before the first frame update
    void Start()
    {
        _blackscreenAnimator = GameObject.Find("Blackscreen").GetComponent<Animator>();
        _audioSource = Camera.main.GetComponent<AudioSource>();
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();
        _progression = GameObject.Find("GameManager").GetComponent<Progression>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void TriggerInteraction()
    {
        StartCoroutine(TakingPhoto());

    }

    private IEnumerator TakingPhoto()
    {
        _playerMovement.enabled = false;
        _blackscreenAnimator.SetBool("hasTaken", true);
        yield return new WaitForSeconds(fadeToBlack.length);
        _audioSource.PlayOneShot(click, 1f);
        yield return new WaitForSeconds(click.length);
        _blackscreenAnimator.SetBool("hasTaken", false);
        yield return new WaitForSeconds(reversedFadeToBlack.length);
        yield return StartCoroutine(_subtitlesManager.WriteSubtitles(4, 3f));
        gameObject.GetComponent<Collider>().enabled = false;
        photoSpot.enabled = false;
        _progression.photosTaken++;
        _playerMovement.enabled = true;
    }
}
