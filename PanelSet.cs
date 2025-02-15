using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelSet : MonoBehaviour
{
     private Image panel;
     private GameObject Start_text;
     private GameObject Button_Cancel;
     private GameObject directionButton;
     AudioScript _AudioScript;
     UI_Script ui_Script;
     Move move;
     Move moves;
     public bool bool_click=false;
      bool io=true;
      GameObject[] Enemies;
      
    // Start is called before the first frame update
    void Start()
    {
      Time.timeScale=1;
      ui_Script=FindObjectOfType<UI_Script>();
      panel=ui_Script.panel;
      Start_text=ui_Script.startText;
      Button_Cancel=ui_Script.cancelButton;
      directionButton=ui_Script.directionButton;
        Enemies=GameObject.FindGameObjectsWithTag("Enemy");
        _AudioScript=FindObjectOfType<AudioScript>();
        StartCoroutine("Panel");
        move=FindObjectOfType<Move>();
        
        ui_Script.endButton.SetActive(false);
        Button_Cancel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      if(directionButton==null) directionButton=GameObject.FindGameObjectWithTag("Vector");
        Enemies=GameObject.FindGameObjectsWithTag("Enemy");
       if(move!=null){
        if(move.isClear&&io){
 StartCoroutine("ClearPanel");
 io=false;
        }
       }
    }
    IEnumerator Panel(){
        
        float c=1.0f;
        float t=0.0f;
    while(c>0.0f){
        c-=0.1f;
        panel.GetComponent<Image>().color=new Color(0.0f,0.0f,0.0f,c);
        yield return new WaitForSeconds(0.05f);

    }
    
    foreach(GameObject i in Enemies){
        yield return new WaitForSeconds(0.25f);
        t=0.0f; 
        _AudioScript.audio_spawn.Play();
        while(t<1.0f){
            
            t+=0.1f;
            i.GetComponent<Renderer>().material.color=new Color(1.0f,1.0f,1.0f,t);
            yield return new WaitForSeconds(0.05f);
        }
    }
    Start_text.SetActive(true);
    Time.timeScale=0;
    yield return new WaitForSecondsRealtime(1);
    Time.timeScale=1;
    Start_text.SetActive(false);
    ui_Script.endButton.SetActive(true);
    Button_Cancel.SetActive(true);
    _AudioScript.bgm.Play();
    bool_click=true;
    EnemyHP.manybool_action=-2;

    }
  public  IEnumerator ClearPanel(){
       directionButton.SetActive(false);
        panel.GetComponent<Image>().color=new Color(0.0f,0.0f,0.0f,0.8f);
        
      yield return new  WaitForSecondsRealtime(0.1f);
        panel.GetComponent<Image>().color=new Color(1.0f,1.0f,1.0f,0.6f);
      yield return new  WaitForSecondsRealtime(0.1f);
        panel.GetComponent<Image>().color=new Color(0.0f,0.0f,0.0f,0.8f);
      yield return new  WaitForSecondsRealtime(0.1f);
        panel.GetComponent<Image>().color=new Color(1.0f,1.0f,1.0f,0.6f);
        yield return new  WaitForSecondsRealtime(0.1f);
        panel.GetComponent<Image>().color=new Color(0.0f,0.0f,0.0f,0.0f);
    }
    public  IEnumerator CriticalPanel(){
       
        
        panel.GetComponent<Image>().color=new Color(1.0f,1.0f,1.0f,0.6f);
      yield return new  WaitForSecondsRealtime(0.1f);
        panel.GetComponent<Image>().color=new Color(0.0f,0.0f,0.0f,0.0f);
      yield return new  WaitForSecondsRealtime(0.1f);
        panel.GetComponent<Image>().color=new Color(1.0f,1.0f,1.0f,0.6f);
        yield return new  WaitForSecondsRealtime(0.1f);
        panel.GetComponent<Image>().color=new Color(0.0f,0.0f,0.0f,0.0f);
    }
}
