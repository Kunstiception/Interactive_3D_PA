using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression : MonoBehaviour
{
    public Light[] lights;

    public int enabledLights;

    public bool lightsOn = false;

    public int photosTaken;

    private Light _flashlight;

    private SubtitlesManager _subtitlesManager;

    // Start is called before the first frame update
    void Start()
    {
        _flashlight = GameObject.Find("Taschenlampe").GetComponent<Light>();
        
        foreach(Light light in lights)
        {
            light.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
