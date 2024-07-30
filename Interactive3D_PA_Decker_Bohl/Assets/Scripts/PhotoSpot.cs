using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSpot : MonoBehaviour
{
    public string text;

    public Collider photoCollider;
    
    private TMP_Text _subtitles;

    private Progression _progression;

    // Start is called before the first frame update
    void Start()
    {
        _subtitles = GameObject.Find("Subtitles").GetComponent<TMP_Text>();
        _progression = GameObject.Find("GameManager").GetComponent<Progression>();
        photoCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _progression.lightsOn)
        {
            _subtitles.text = text;
            photoCollider.enabled = true;
        }
    }
}
