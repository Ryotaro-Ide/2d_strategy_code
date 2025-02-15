using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasTransform : MonoBehaviour
{
    RectTransform _recttransform;
    // Start is called before the first frame update
    void Start()
    {
        _recttransform=gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
