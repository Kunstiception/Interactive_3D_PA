using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayAudio : InteractableObject
{
    // Array of the used audio clips
    public AudioClip audioFile;

    // Array of the used UI labels
    public string[] labelsText;

    // Reference to the  animation clip
    public AnimationClip animationClip;

    // Reference to the audio source
    private AudioSource _audioSource;

    // Boolean describing if audio is currently playing
    private bool _isPlaying;

    //Reference to the animator
    private Animator _animator;

    // Boolean to check if the player is close
    private bool _inRange;

    // Reference to the subtitles object
    private TMP_Text _text;

    // Reference to the Trigger Animation script
    private TriggerAnimation _triggerAnimation;

    // Reference to the progression script
    private Progression _progression;

    // Reference to the subtitles manager script
    private SubtitlesManager _subtitlesManager;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponentInChildren<Animator>();
        _progression = GameObject.Find("GameManager").GetComponent<Progression>();
        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Show the labels for stopping if audio is playing and player is close enough, otherwise the command for playing
        if (_animator.GetBool("hasInteracted") )
        {
            commandoText = labelsText[0];
        }
        else
        {
            commandoText = labelsText[1];
        }
    }

    // Plays the animation and audio with the first interaction, plays the deactivation animation and stops audio with the second interaction
    // (if audio should still be running)
    public override void TriggerInteraction()
    {
        if (_progression.lightsOn)
        {
            if (!_animator.GetBool("hasInteracted"))
            {
                _animator.SetBool("hasInteracted", true);
                _audioSource.Play();
                StartCoroutine(WaitForAudio());
            }
            else if (_animator.GetBool("hasInteracted"))
            {
                _animator.SetBool("hasInteracted", false);
                _audioSource.Stop();
            }
        }

        else if (!_progression.lightsOn)
        {
            StartCoroutine(_subtitlesManager.WriteSubtitles(2, 3f));

        }


    }

    // A coroutine that triggers the deactivation animation after the audio has played
    private IEnumerator WaitForAudio()
    {
        yield return new WaitForSeconds(audioFile.length);
        _animator.SetBool("hasInteracted", false);
        
    }
}
