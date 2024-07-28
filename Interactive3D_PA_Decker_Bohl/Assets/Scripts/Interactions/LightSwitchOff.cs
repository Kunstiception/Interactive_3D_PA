using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchOff : InteractableObject
{
    // The corresponding lightsource
    private Light _lightSource;

    // Start is called before the first frame update
    void Start()
    {
        _lightSource = GetComponentInChildren<Light>();
    }

    public override void TriggerInteraction()
    {
        if(_lightSource.gameObject.GetComponent<Light>().isActiveAndEnabled)
        {
               _lightSource.enabled = false;
        }
        else if (!_lightSource.gameObject.GetComponent<Light>().isActiveAndEnabled)
        {
            _lightSource.enabled = true;
        }
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
