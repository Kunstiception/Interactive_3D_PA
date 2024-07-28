using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayAudio : MonoBehaviour
{
    // Array of the used audio clips
    public AudioClip[] audioFiles;

    // Array of the used UI labels
    public string[] labelsText;

    private AudioSource _audioSource;

    // Boolean describing if audio is currently playing
    private bool _isPlaying;

    //Reference to the animator
    private Animator _animator;

    // Boolean to check if the player is close
    private bool _inRange;

    private TMP_Text _text;

    private TriggerAnimation _triggerAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponentInChildren<Animator>();
        _text = GameObject.Find("AudioLabel").GetComponent<TMP_Text>();
        _text.gameObject.SetActive(false);
        _triggerAnimation = GetComponent<TriggerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        // Show the labels for stopping and rewinding if audio is playing and player is close enough
        if (_animator.GetBool("hasInteracted") )
        {
            _triggerAnimation.commandoText = labelsText[0];
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && _animator.GetBool("hasInteracted"))
        {
            _inRange = true;
            _text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _inRange = false;
            _text.gameObject.SetActive(false);
        } 
    }
    
    private IEnumerator PlaySpeeches()
    {
        _audioSource.PlayOneShot(audioFiles[0], 1); 
        yield return new WaitForSeconds(audioFiles[0].length);
        _audioSource.PlayOneShot(audioFiles[1], 1);
        yield return new WaitForSeconds(audioFiles[1].length);
        _audioSource.PlayOneShot(audioFiles[2], 1);
        yield return new WaitForSeconds(audioFiles[2].length);
    }
}
