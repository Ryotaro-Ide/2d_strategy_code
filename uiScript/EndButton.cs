using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{
    public GameObject confirmImage;
    private Move[] move;
    AudioScript _AudioScript;
    // Start is called before the first frame update
    void Start()
    {
        _AudioScript=FindObjectOfType<AudioScript>();
        move=FindObjectsOfType<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        move=FindObjectsOfType<Move>();
    }
    public void _ConfirmButton(){
        _AudioScript.audio_button.Play();
        confirmImage.SetActive(true);
    }
    public void _EndButton(){
        StartCoroutine("EndTime");
        
    }
    public void _CancelButton(){
        _AudioScript.audio_button.Play();
        confirmImage.SetActive(false);
    }
    IEnumerator EndTime(){
        EnemyHP.manybool_action=0;
        _AudioScript.audio_button.Play();
        yield return new WaitForSeconds(0.3f);
        foreach(Move i in move){
        i.turn=false;
        }
        confirmImage.SetActive(false);
    }
}
