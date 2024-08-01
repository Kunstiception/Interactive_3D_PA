using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : InteractableObject
{
    // Reference to the audio clip
    public AudioClip click;

    // Reference to the animation clip fadetoblack
    public AnimationClip fadeToBlack;

    // Reference to the animation clip reversedfadetoblack
    public AnimationClip reversedFadeToBlack;

    // Reference to the photo spot collider
    public Collider photoSpot;

    // Reference to the animator of the blackscreen
    private Animator _blackscreenAnimator;

    // Reference to the audio source
    private AudioSource _audioSource;

    // Reference to the player movement script
    private PlayerMovement _playerMovement;

    // Reference to the subtitles manager script
    private SubtitlesManager _subtitlesManager;

    // Reference to the progression script
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

    // Trigger the coroutine
    public override void TriggerInteraction()
    {
        StartCoroutine(TakingPhoto());

    }

    // Coroutine to imitate the action of taking a photo, it triggers a short fadetoblack and audio clip and increments the photsTaken variable
    // The player movement is locked during this coroutine to ensure they are looking in the correct direction
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
