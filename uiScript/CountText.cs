using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CountText : MonoBehaviour
{
  public TextMeshProUGUI _turntext;
  Move move;
    // Start is called before the first frame update
    void Start()
    {
        move=FindObjectOfType<Move>();
       _turntext.text="自分のターン";
    }

    // Update is called once per frame
    void Update()
    {
         move=FindObjectOfType<Move>();
       if(!move.turn&&move!=null){
        _turntext.text="敵のターン";
       }
    }
}
