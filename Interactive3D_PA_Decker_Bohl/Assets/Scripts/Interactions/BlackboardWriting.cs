using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class BlackboardWriting : InteractableObject
{
    // Array of the decal projectors
    public DecalProjector[] decals;

    // Reference to the first animation clip
    public AnimationClip clip1;

    // Reference to the second animation clip
    public AnimationClip clip2;

    // Reference to the audio clip
    public AudioClip audioClip;

    // Reference to the subtitles manager script
    private SubtitlesManager _subtitlesManager;

    // Reference to the animator of the blackscreen
    private Animator _blackscreenAnimator;

    // Reference to the progression script
    private Progression _progression;

    // Integer to describe how many times was written on the blackboard
    private int timesWritten = 0;

    // Boolean to describe if the coroutine is running
    private bool isRunning;

    // Reference to the audio source
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();
        _blackscreenAnimator = GameObject.Find("Blackscreen").GetComponent<Animator>();
        _progression = GameObject.Find("GameManager").GetComponent<Progression>();
        _audioSource = Camera.main.GetComponent<AudioSource>();

        // Disable both decals on start
        foreach(DecalProjector decal in decals)
        {
            decal.enabled = false;
        }
    }


    // Disbale and enable the correct decals to be displayed on the blackboard depending on how many times the interaction has been triggered
    public override void TriggerInteraction()
    {
        if (_progression.lightsOn && timesWritten < 2)
        {
            if (!decals[0].isActiveAndEnabled && !decals[1].isActiveAndEnabled)
            {
                StartCoroutine(WriteOnBlackborad(0, 1, 5));
            }
            else if (decals[0].isActiveAndEnabled && !decals[1].isActiveAndEnabled)
            {
                StartCoroutine(WriteOnBlackborad(1, 0, 6));
            }
            
        } 
        
        else if(timesWritten >= 2 && !isRunning)
        {
            StartCoroutine(_subtitlesManager.WriteSubtitles(7, 4f));
        }

    }

    // Coroutine to disable and enable decals, control the animator of the blackscreen and play an audio clip
    private IEnumerator WriteOnBlackborad(int enable, int disable, int subtitle)
    {
        isRunning = true;
        timesWritten++;
        _blackscreenAnimator.SetBool("hasTaken", true);
        _audioSource.PlayOneShot(audioClip, 2.5f);
        yield return new WaitForSeconds(clip1.length + 1);
        decals[enable].enabled = true;
        decals[disable].enabled = false;
        _blackscreenAnimator.SetBool("hasTaken", false);
        yield return new WaitForSeconds(clip2.length + 1);
        yield return StartCoroutine(_subtitlesManager.WriteSubtitles(subtitle, 4f));
        isRunning = false;
    }
}
