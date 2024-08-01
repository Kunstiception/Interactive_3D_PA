using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Türklinke : InteractableObject
{
    // Reference to the animation clip
    public AnimationClip animationClip;

    // Animator of the object
    private Animator _animator;

    // Reference to the subtitles manager script
    private SubtitlesManager _subtitlesManager;

    // Reference to the progression script
    private Progression _progression;

    // Reference to the animator of the blackscreen
    private Animator _blackscreenAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();
        _progression = GameObject.Find("GameManager").GetComponent<Progression>();
        _blackscreenAnimator = GameObject.Find("Blackscreen").GetComponent<Animator>();
    }

    // Trigger the final blackscreen if all requirements are met
    private IEnumerator Ending()
    {
        _animator.SetTrigger("hasPushed");
        yield return new WaitForSeconds(animationClip.length);
        _blackscreenAnimator.SetBool("hasTaken", true);
        yield return StartCoroutine(_subtitlesManager.WriteSubtitles(11, 3f));
    }


    public override void TriggerInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // First check if all photos were taken, if not set the corresponding subtitle
            if (_progression.photosTaken < 2)
            {
                StartCoroutine(_subtitlesManager.WriteSubtitles(9, 3f));

            }

            else if (_progression.photosTaken >= 2)
            {
                // First check if all lights are turned off, if not set the corresponding subtitle, if they are start the final coroutine
                if (_progression.enabledLights == 0)
                {
                    StartCoroutine(Ending());
                }
                else
                {
                    StartCoroutine(_subtitlesManager.WriteSubtitles(10, 3f));
                }
            }

        }
    }
}
