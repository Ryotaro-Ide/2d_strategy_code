using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Load_Select : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public  void  Onload(){
    SceneManager.LoadScene("Select");
   }
   public void LoseLoad(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}
