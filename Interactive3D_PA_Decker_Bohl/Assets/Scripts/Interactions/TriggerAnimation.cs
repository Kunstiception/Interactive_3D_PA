using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : InteractableObject
{
    // The corresponding lightsource
    public Light lightSource;
    
    // Animator of the object
    private Animator _animator;

    private Progression _progression;

    private SubtitlesManager _subtitlesManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _progression = GameObject.Find("GameManager").GetComponent<Progression>();

        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();

        if (gameObject.GetComponent<Animator>() == null)
        {
            _animator = gameObject.GetComponentInChildren<Animator>();
        }
        else
        {
            _animator = gameObject.GetComponent<Animator>();
        }
        
    }

    public override void TriggerInteraction()
    {
        if(_progression.lightsOn)
        {
            if (!_animator.GetBool("hasInteracted"))
            {
                _animator.SetBool("hasInteracted", true);
            
                if(lightSource != null)
                {
                    lightSource.enabled = true;
                }
            }

            else if (_animator.GetBool("hasInteracted"))
            {
                _animator.SetBool("hasInteracted", false);

                if (lightSource != null)
                {
                    lightSource.enabled = false;
                }
            }
        }
        else if (!_progression.lightsOn)
        {
            print("ahahaha");
            if(lightSource == null)
            {
                StartCoroutine(_subtitlesManager.WriteSubtitles(2, 3f));
            }
            else
            {
                _animator.SetBool("hasInteracted", true);
                lightSource.enabled = true;
                _progression.enabledLights++;

            }
            
        }

        
    }
}
