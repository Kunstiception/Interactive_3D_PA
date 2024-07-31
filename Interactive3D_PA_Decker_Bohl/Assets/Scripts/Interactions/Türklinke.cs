using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Türklinke : InteractableObject
{
    public AnimationClip animationClip;

    // Animator of the object
    private Animator _animator;

    private SubtitlesManager _subtitlesManager;

    private Progression _progression;

    private Animator _blackscreenAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();
        _progression = GameObject.Find("GameManager").GetComponent<Progression>();
        _blackscreenAnimator = GameObject.Find("Blackscreen").GetComponent<Animator>();
    }


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
            if (_progression.photosTaken < 2)
            {
                StartCoroutine(_subtitlesManager.WriteSubtitles(9, 3f));

            }

            else if (_progression.photosTaken >= 2)
            {
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
