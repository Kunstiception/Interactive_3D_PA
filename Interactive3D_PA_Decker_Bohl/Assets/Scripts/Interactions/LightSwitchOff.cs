using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchOff : InteractableObject
{
    // The corresponding lightsource
    private Light _lightSource;

    // Reference to the progression script
    private Progression _progression;

    // Start is called before the first frame update
    void Start()
    {
        _lightSource = GetComponentInChildren<Light>();

        _progression = GameObject.Find("GameManager").GetComponent<Progression>();
    }

    // Switch the lightsource on and off and increment the enabledLights variable
    public override void TriggerInteraction()
    {
        if(_lightSource.gameObject.GetComponent<Light>().isActiveAndEnabled)
        {
            _lightSource.enabled = false;
            _progression.enabledLights--;
        }
        else if (!_lightSource.gameObject.GetComponent<Light>().isActiveAndEnabled)
        {
            _lightSource.enabled = true;
            _progression.enabledLights++;
        }
        
    }
}
