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

    public AnimationClip animationClip;

    private AudioSource _audioSource;

    // Boolean describing if audio is currently playing
    private bool _isPlaying;

    //Reference to the animator
    private Animator _animator;

    // Boolean to check if the player is close
    private bool _inRange;

    private TMP_Text _text;

    private TriggerAnimation _triggerAnimation;

    private Progression _progression;

    private SubtitlesManager _subtitlesManager;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponentInChildren<Animator>();
        _text = GameObject.Find("AudioLabel").GetComponent<TMP_Text>();
        _text.gameObject.SetActive(false);
        _progression = GameObject.Find("GameManager").GetComponent<Progression>();
        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Show the labels for stopping and rewinding if audio is playing and player is close enough
        if (_animator.GetBool("hasInteracted") )
        {
            commandoText = labelsText[0];
        }
        else
        {
            commandoText = labelsText[1];
        }
    }

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
            print("ahahaha");
            StartCoroutine(_subtitlesManager.WriteSubtitles(2, 3f));

        }


    }

    private IEnumerator WaitForAudio()
    {
        yield return new WaitForSeconds(audioFile.length);
        _animator.SetBool("hasInteracted", false);
        
    }
}
