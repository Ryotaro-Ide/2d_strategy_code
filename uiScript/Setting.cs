using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Setting : MonoBehaviour
{
    public GameObject _panel;
    AudioScript _AudioScript;
    
    // Start is called before the first frame update
    void Start()
    {
        _AudioScript=FindObjectOfType<AudioScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTouch(){
        _panel.SetActive(true);
        
        _AudioScript.audio_pause.Play();
        Time.timeScale=0;
    }
    public void OnTouch_false(){
        Time.timeScale=1;
         _AudioScript.audio_unpause.Play();
        _panel.SetActive(false);
       
        
    }
}
