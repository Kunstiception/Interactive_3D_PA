using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection.Emit;
using System.Net.NetworkInformation;

public class PlayAudio : MonoBehaviour
{
    // Array of the used audio clips
    public AudioClip[] audioFiles;

    // Array of the used UI labels
    public TextMeshProUGUI[] labelsUI;

    private AudioSource _audioSource;

    // Boolean describing if audio is currently playing
    private bool _isPlaying;

    // Boolean describing if the player is close enought to play the audio files
    private bool _isClose;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        HideAllLabels();
    }

    // Update is called once per frame
    void Update()
    {
        // Show the labels for stopping and rewinding if audio is playing and player is close enough
        if (_isPlaying && _isClose)
        {
            ShowLabel(1);
            ShowLabel(2);
        }

        // If the player is close enough
        if (_isClose)
        {
            // If E key is pressed, select the current audio clip, play it and set isPlaying true
            if (Input.GetKeyDown(KeyCode.E))
            {
                SelectAudioClip(0);
                _audioSource.Play();
                _isPlaying = true;
            }
        }
        
        // If the audio is playing
        if(_isPlaying)
        {
            // If spacebar is pressed, stop audio and set isPlaying to false
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _audioSource.Stop();
                _isPlaying = false;
            }
        }
        
        // If the R key is pressed, stop and play the audio clip, so it starts from the beginning
        if (Input.GetKeyDown(KeyCode.R))
        {
            _audioSource.Stop();
            _audioSource.Play();
        }
    }

    // As long as the player is in the bounds of the trigger, show label with the index 0 ("Press E") and set isClose and isPlaying true
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowLabel(0);
            _isPlaying = true;
            _isClose = true;
        }
    }

    // If the player leaves the bounds of the trigger, hide all labels and set isClose to false
    private void OnTriggerExit(Collider other)
    {
        HideAllLabels();
        _isClose = false;
    }

    // Hides all labels in the labelsUI array
    public void HideAllLabels()
    {
        foreach (TextMeshProUGUI label in labelsUI)
        {
            label.gameObject.SetActive(false);
        }
    }

    // Shows the label with the given index
    public void ShowLabel(int index)
    {
        if (index >= 0 && index <= labelsUI.Length)
        {
            labelsUI[index].gameObject.SetActive(true);
        }
        
    }

    // Select the right audio clip with the given index 
    public void SelectAudioClip(int index)
    {
        if (index >= 0 && index <= audioFiles.Length)
        {
            // https://docs.unity3d.com/ScriptReference/AudioSource-clip.html
            _audioSource.clip = audioFiles[index];
            print(_audioSource.clip);
        }
    }
}
