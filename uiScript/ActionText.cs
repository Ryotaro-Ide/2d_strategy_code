using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ActionText : MonoBehaviour
{

    //-10からは魔法にかんするテキスト
    //1から10はプレイヤーに関するテキスト
    public TextMeshProUGUI _actiontext;
    [SerializeField] AttackButton _AttackButton;
    [SerializeField] AttackButton _MagicButton;
    private Move[] moves;
    private Move _move;
    [SerializeField] GameObject _attackbutton;
    [SerializeField] GameObject _magicbutton;
    // Start is called before the first frame update
    void Start()
    {
        _actiontext.text="";
        _AttackButton=FindObjectOfType<AttackButton>();
    }

    // Update is called once per frame
    void Update()
    {  
        StartCoroutine(_ActionText());
    }
    IEnumerator _ActionText(){
        yield return null;
        if(_magicbutton==null){
            _magicbutton=GameObject.FindGameObjectWithTag("MagicButton");
        }
        if(_magicbutton!=null){
            _MagicButton=_magicbutton.GetComponent<AttackButton>();
        }
         if (_attackbutton != null)
        {
          //  _attackbutton=GameObject.FindGameObjectWithTag("AttackButton");
            _AttackButton = _attackbutton.GetComponent<AttackButton>();
        }
        moves=FindObjectsOfType<Move>();
       
        if(EnemyHP.manybool_action==0){ //デフォルト
            _actiontext.text="";
        }else if(EnemyHP.manybool_action==-2){
            _actiontext.text="プレイヤーをタッチ";
        }else if(EnemyHP.manybool_action==6){ //選択したときの名前
            foreach(Move i in moves){
                if(i.Player_Select) _move=i;
            }
             _actiontext.text=_move.gameObject.name;
        }
        if(_AttackButton!=null&&_AttackButton.enemy!=null){
           
        
                    
         if(EnemyHP.manybool_action==-1){//攻撃ボタンを押したとき
            _actiontext.text=_AttackButton.move.gameObject.name+"の攻撃";
            
        }else if(EnemyHP.manybool_action==1){ //プレイヤーの攻撃が当たったとき
            _actiontext.text=_AttackButton.enemy.gameObject.name+"に\n"+_AttackButton.move.AttackPoint+"ダメージ！";
        }else if(EnemyHP.manybool_action==2){//プレイヤーのクリティカル攻撃が当たったとき
            _actiontext.text="ラッキー！\n"+_AttackButton.enemy.gameObject.name+"に\n"+2*_AttackButton.move.AttackPoint+"ダメージ与えた！";
        }else if(EnemyHP.manybool_action==3){//プレイヤーの攻撃が避けられたとき
            _actiontext.text=_AttackButton.enemy.gameObject.name+"に\n"+"当たらなかった！";
        }else if(EnemyHP.manybool_action==4){//敵を倒したとき
            _actiontext.text=_AttackButton.enemy.gameObject.name+"を\n倒した";
        }else if(EnemyHP.manybool_action==8){//二回攻撃
            _actiontext.text="二回目の攻撃";
        }
    }
    if(_MagicButton!=null){
        if(EnemyHP.manybool_action==-10){ //スピードアップ
            _actiontext.text=_MagicButton.enemy.gameObject.name+"の\n行動力が2アップ";
        }else if(EnemyHP.manybool_action==-11){ //ヒーリング
             _actiontext.text=_MagicButton.enemy.gameObject.name+"の\nHPが2回復";
        }else if(EnemyHP.manybool_action==-12){//攻撃力ダウン
             _actiontext.text=_MagicButton.enemy.gameObject.name+"の\n攻撃力が2ダウン";
        }
    }
    if(EnemyHP.manybool_action==-3){ //プレイヤーがFキーを押したときの判定
            foreach(Move i in moves){
                if(i.ColliderEnemy!=null) _move=i;}
                
                if(_move!=null&&_move.ColliderEnemy!=null){
                   
             _actiontext.text=_move.ColliderEnemy.gameObject.name;
                }else{
                    _actiontext.text="なにもない";
                }
        }else if(EnemyHP.manybool_action==5){ //ゲームクリアしたとき
            _actiontext.text="GAME CLEAR!!";
        }else if(EnemyHP.manybool_action==7){
            _actiontext.text="もう動けない";
        } 
    }
}
