using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class BlackboardWriting : InteractableObject
{
    public DecalProjector[] decals;

    public AnimationClip clip1;

    public AnimationClip clip2;

    private SubtitlesManager _subtitlesManager;

    private Animator _blackscreenAnimator;

    private Progression _progression;

    private int timesWritten = 0;

    private bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();
        _blackscreenAnimator = GameObject.Find("Blackscreen").GetComponent<Animator>();
        _progression = GameObject.Find("GameManager").GetComponent<Progression>();

        foreach(DecalProjector decal in decals)
        {
            decal.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
    
    public override void TriggerInteraction()
    {
        if (_progression.lightsOn && timesWritten < 2)
        {
            if (!decals[0].isActiveAndEnabled && !decals[1].isActiveAndEnabled)
            {
                //decal1.enabled = true;
                //StartCoroutine(_subtitlesManager.WriteSubtitles(5, 3f));
                StartCoroutine(WriteOnBlackborad(0, 1, 5));
            }
            else if (decals[0].isActiveAndEnabled && !decals[1].isActiveAndEnabled)
            {
                //decal1.enabled = false;
                //decal2.enabled = true;
                //StartCoroutine(_subtitlesManager.WriteSubtitles(6, 3f));
                StartCoroutine(WriteOnBlackborad(1, 0, 6));
            }
            
        } 
        
        else if(timesWritten >= 2 && !isRunning)
        {
            print("Jetzt");
            StartCoroutine(_subtitlesManager.WriteSubtitles(7, 4f));
        }

    }

    private IEnumerator WriteOnBlackborad(int enable, int disable, int subtitle)
    {
        isRunning = true;
        timesWritten++;
        _blackscreenAnimator.SetBool("hasTaken", true);
        yield return new WaitForSeconds(clip1.length + 1);
        decals[enable].enabled = true;
        decals[disable].enabled = false;
        _blackscreenAnimator.SetBool("hasTaken", false);
        yield return new WaitForSeconds(clip2.length + 1);
        yield return StartCoroutine(_subtitlesManager.WriteSubtitles(subtitle, 4f));
        isRunning = false;
    }
}
