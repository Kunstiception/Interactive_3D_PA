using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photograph : InteractableObject
{
    public AnimationClip drawerPull;
    
    private SpriteRenderer _renderer;

    private Animator _animatorDrawer;

    private Animator _animator;

    private SubtitlesManager _subtitlesManager;

    private bool hasDrawn = false;

    private Collider _collider;



    // Start is called before the first frame update
    void Start()
    {
        _animatorDrawer = GameObject.Find("Schublade").GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GameObject.Find("Bohr_Einstein").GetComponent<Animator>();
        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();
        //_renderer.enabled = false;
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_animatorDrawer.GetCurrentAnimatorStateInfo(0).IsName("Idle_Opened"))
        {
            _collider.enabled = true;
        }
        else if (_animatorDrawer.GetCurrentAnimatorStateInfo(0).IsName("Idle_Closed"))
        {
            _collider.enabled = false;
        }
    } 
    
    public override void TriggerInteraction()
    {
        StartCoroutine(ShowPicture());

    }

    private IEnumerator ShowPicture()
    {
        _animator.SetBool("show", true);
        yield return StartCoroutine(_subtitlesManager.WriteSubtitles(3, 6f));
        _animator.SetBool("show", false);
        
    }
}
