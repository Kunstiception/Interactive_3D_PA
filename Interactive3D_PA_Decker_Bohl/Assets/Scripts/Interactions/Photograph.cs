using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photograph : InteractableObject
{
    // Reference to the animation clip
    public AnimationClip drawerPull;
    
    // Reference to the renderer of the picture
    private SpriteRenderer _renderer;

    // Reference to the animator of the drawer
    private Animator _animatorDrawer;

    // Reference to the animator of the blackscreen
    private Animator _animator;

    // Reference to the subtitles manager script
    private SubtitlesManager _subtitlesManager;

    // Reference to the collider of the picture
    private Collider _collider;



    // Start is called before the first frame update
    void Start()
    {
        _animatorDrawer = GameObject.Find("Schublade").GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GameObject.Find("Bohr_Einstein").GetComponent<Animator>();
        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If the current animator state is open, enable the collider of the picture so it can be interacted with
        if(_animatorDrawer.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            _collider.enabled = true;
        }
        // If the current animator state is closed, disable the collider of the picture so it cannot be interacted with
        else if (_animatorDrawer.GetCurrentAnimatorStateInfo(0).IsName("Close"))
        {
            _collider.enabled = false;
        }
    } 
    
    // Trigger the coroutine
    public override void TriggerInteraction()
    {
        StartCoroutine(ShowPicture());

    }

    // Show the picture on the canvas and set the subtitles
    private IEnumerator ShowPicture()
    {
        _animator.SetBool("show", true);
        yield return StartCoroutine(_subtitlesManager.WriteSubtitles(3, 6f));
        _animator.SetBool("show", false);
        
    }
}
