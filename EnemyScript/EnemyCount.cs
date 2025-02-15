using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
   public int i=0;
   public int TurnCount=1;
   public EnemyManager[] enemyCounts;
    EnemyManager enemycount;
    private Move move;
    private Move move2; 
    UI_Script ui_Script;
    // Start is called before the first frame update
    void Start()
    {

        move=FindObjectOfType<Move>();
        move2=FindObjectOfType<Move>();
       enemycount=FindObjectOfType<EnemyManager>();
   enemyCounts = FindObjectsOfType<EnemyManager>();
    ui_Script=FindObjectOfType<UI_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        move2=FindObjectOfType<Move>();
        if(move2!=null){
        if(move==null){
            move=FindObjectOfType<Move>();
            move.Enemyact=0;
        }
        
        enemyCounts = FindObjectsOfType<EnemyManager>();
        if(move.Enemyact==1&&enemyCounts.Length>i){
           move.Enemyact=0;
           i++;
            if(enemyCounts[i-1]!=null){
           StartCoroutine(enemyCounts[i-1].EnemyCoroutine());
           
            }
        }else if(move.Enemyact==1&&enemyCounts.Length<=i){
            StartCoroutine(enemycount.EnemyTurnEnd());
            foreach(EnemyManager enemy in enemyCounts){
                enemy.count=enemy.startcount;
                enemy.MovementStatusEffect();
            }
            move.Enemyact=0;
            TurnCount++;
           // enemycount.obj_attack.SetActive(false);
        }
        
    }
    if(ui_Script.retryButton.activeSelf){ //非アクティブかどうか
        enabled=false;
        Debug.Log("PlayerDeath");
    }
    }
    

}


