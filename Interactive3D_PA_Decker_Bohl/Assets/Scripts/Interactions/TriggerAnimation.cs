using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : InteractableObject
{
    // The corresponding lightsource
    public Light lightSource;
    
    // Animator of the object
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<Animator>() == null)
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
}
