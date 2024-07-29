using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitlesManager : MonoBehaviour
{
    public string[] strings;
    
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

    private IEnumerator IntroMonologue()
    {
        yield return StartCoroutine(WriteSubtitles(0, 3f));
        yield return StartCoroutine(WriteSubtitles(1, 3f));
    }

    public IEnumerator WriteSubtitles(int index, float WaitTime)
    {
        _subtitles.gameObject.SetActive(true);
        _subtitles.text = strings[index];
        yield return new WaitForSeconds(3f);
        _subtitles.gameObject.SetActive(false);
    }
}
