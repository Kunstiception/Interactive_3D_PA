using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection.Emit;
using System.Net.NetworkInformation;
using Unity.VisualScripting;

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
            HideLabel(0);
        }

        // If the player is close enough and label 3 is not showing (audio is not paused), show label 0
        if (_isClose)
        {
            if (_isPlaying == false && labelsUI[3].gameObject.activeSelf == false)
            {
                ShowLabel(0);
            }
            
            // If E key is pressed, select the current audio clip, play it and set isPlaying true, hide label 3 and show label 1
            if (Input.GetKeyDown(KeyCode.E))
            {
                SelectAudioClip(0);
                _audioSource.Play();
                _isPlaying = true;
                HideLabel(3);
                ShowLabel(1);
                
            }
        }
        
        // If the audio is playing
        if(_isPlaying)
        {
            // If spacebar is pressed, stop audio and set isPlaying to false, hide label 1 and show label 3
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _audioSource.Pause();
                _isPlaying = false;
                HideLabel(1);
                ShowLabel(3);
            }    
        }

        // If the R key is pressed, stop and play the audio clip, so it starts from the beginning
        if (Input.GetKeyDown(KeyCode.R))
        {
            _audioSource.Stop();
            _audioSource.Play();
        }
    }

    // As long as the player is in the bounds of the trigger, set isClose true
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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

    // Shows the label with the given index
    public void HideLabel(int index)
    {
        if (index >= 0 && index <= labelsUI.Length)
        {
            labelsUI[index].gameObject.SetActive(false);
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
