using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
[ExecuteAlways]
public class Stage_Name : MonoBehaviour
{
TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text=GetComponent<TextMeshProUGUI>();
        _text.text=SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
