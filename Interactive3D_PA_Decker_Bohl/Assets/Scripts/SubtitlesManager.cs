using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitlesManager : MonoBehaviour
{
    // Array of all subtitles
    public string[] strings;
    
    // The subtitles game object
    private TMP_Text _subtitles;

    
    // Start is called before the first frame update
    void Start()
    {
        _subtitles = GameObject.Find("Subtitles").GetComponent<TMP_Text>();
        _subtitles.gameObject.SetActive(false);
        StartCoroutine(IntroMonologue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // The subtitles to be displayed by the start of the application
    private IEnumerator IntroMonologue()
    {
        yield return StartCoroutine(WriteSubtitles(0, 3f));
        yield return StartCoroutine(WriteSubtitles(1, 3f));
    }

    // Writes the subtitles into the subtitles game object for the specified time
    public IEnumerator WriteSubtitles(int index, float WaitTime)
    {
        _subtitles.gameObject.SetActive(true);
        _subtitles.text = strings[index];
        yield return new WaitForSeconds(WaitTime);
        _subtitles.gameObject.SetActive(false);
    }
}
