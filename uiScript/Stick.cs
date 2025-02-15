using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stick : MonoBehaviour
{
    public static int bool_stick=0;
    public static int bool_direct_stick=0;
    public static bool bool_directing=false;
    AudioScript _AudioScript;
    
    // Start is called before the first frame update
    void Start()
    {
        _AudioScript=FindObjectOfType<AudioScript>();
        bool_stick=0;
        bool_direct_stick=0;
        bool_directing=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void stick_right(){
       
        if(!bool_directing)
        bool_stick=1;
        else bool_direct_stick=1;
        
    }
    public void stick_left(){
       
        if(!bool_directing)
        bool_stick=2;
        else bool_direct_stick=2;
    }
    public void stick_up(){
       
        if(!bool_directing)
        bool_stick=3;
        else bool_direct_stick=3;
    }
    public void stick_down(){
       
        if(!bool_directing)
        bool_stick=4;
        else bool_direct_stick=4;
    }
    public void stick_confirm(){
         _AudioScript.audio_button.Play();
        if(!bool_directing)
        bool_stick=5;
        else bool_direct_stick=5;
    }
    public void stick_cancel(){
         _AudioScript.audio_button.Play();
        bool_stick=6;
    }
    public void stick_direction(){
      
        bool_stick=7;
        bool_directing=true;
    }
}
