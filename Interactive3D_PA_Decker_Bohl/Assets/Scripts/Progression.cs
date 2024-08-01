using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression : MonoBehaviour
{
    // Array of all 3 lights
    public Light[] lights;

    // The number of currently enabled lights
    public int enabledLights;

    // Boolean to describe if all lights are turned on
    public bool lightsOn = false;

    // The number of photos taken
    public int photosTaken;

    // The flashlight thats being turned on when there are no active lights in the room
    private Light _flashlight;

    // Reference to the subtitles manager script
    private SubtitlesManager _subtitlesManager;

    // Start is called before the first frame update
    void Start()
    {
        _flashlight = GameObject.Find("Taschenlampe").GetComponent<Light>();
        
        // Turn of all lights on start
        foreach(Light light in lights)
        {
            light.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Disable the flashlight if at least one light is active
        // Switch lightsOn variable to true only if all lights are active
        // Turn the flashlight back on if there is no active light in the scene and switch lightsOn to false
        if(enabledLights > 0)
        {
            _flashlight.enabled = false;

            if(enabledLights >= 3)
            {
                lightsOn = true;
            }
        }
        else if (enabledLights <= 0)
        {
            _flashlight.enabled = true;
            lightsOn = false;
        }

    }
}
