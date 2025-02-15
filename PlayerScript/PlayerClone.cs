using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    public Move move;
     SpriteRenderer cloneSprite;
     public SpriteRenderer oldCloneSprite;
     public GameObject clone;
    // Start is called before the first frame update
    void Start()
    {
        cloneSprite=move.gameObject.GetComponent<SpriteRenderer>();
        oldCloneSprite.sprite=cloneSprite.sprite;
        clone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        if(move.bool_clone){
            clone.SetActive(true);
        }else{
            clone.SetActive(false);
        }
    }
}
