using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : InteractableObject
{
    // The corresponding lightsource
    public Light lightSource;
    
    // Animator of the object
    private Animator _animator;

    // Reference to the progression script
    private Progression _progression;

    // Reference to the subtitles manager script
    private SubtitlesManager _subtitlesManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _progression = GameObject.Find("GameManager").GetComponent<Progression>();

        _subtitlesManager = GameObject.Find("GameManager").GetComponent<SubtitlesManager>();

        // Check if the animator might be in a child game object
        if (gameObject.GetComponent<Animator>() == null)
        {
            _animator = gameObject.GetComponentInChildren<Animator>();
        }
        else
        {
            _animator = gameObject.GetComponent<Animator>();
        }
        
    }

    // Trigger the interaction only if all lights are turned on, if the object is a light, then turn it on or off
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
                    _progression.enabledLights--;
                }
            }
        }
        else if (!_progression.lightsOn)
        {
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
