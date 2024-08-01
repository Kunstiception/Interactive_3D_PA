using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSpot : MonoBehaviour
{
    // Text to be displayed in the subtitles
    public string text;

    // The collider to which the player needs to point
    public Collider photoCollider;
    
    // The subtitles gameobject
    private TMP_Text _subtitles;

    // Reference to the progression script
    private Progression _progression;

    // Start is called before the first frame update
    void Start()
    {
        _subtitles = GameObject.Find("Subtitles").GetComponent<TMP_Text>();
        _progression = GameObject.Find("GameManager").GetComponent<Progression>();
        photoCollider.enabled = false;
    }


    // If all lights are turned on, allow the taking of the photo
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _progression.lightsOn)
        {
            _subtitles.text = text;
            photoCollider.enabled = true;
        }
    }
}
