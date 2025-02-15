using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class EnemyManager : MonoBehaviour
{
   public int EnemyNumber=2;
   public int AttackPoint=1;
   public int count=2;
  [HideInInspector] public int startcount;
   private int beforeDirection=2;
   private int afterDirection;
    private float l=0.0f;
  [HideInInspector]  public float lerp=0.05f;
    public float percentage_magic1=0.2f;
    private  float dis=2.56f;
    private GameObject cube;
    
   private Move move;
   private Move[] moves;
   private Vector3 EnemyMove_left;
   private Vector3 EnemyMove_right;
   private Vector3 EnemyMove_up;
   private Vector3 EnemyMove_down;
   private Vector3 newposition_one; 
   private Vector3 preposition=new Vector3(-10.0f,-10.0f,0.0f); 
   List<Vector3> attackRoute=new List<Vector3>();
   private GameObject[] obj_attack_array;
   private EnemyCount _Enemycount;
   private GameObject[] PlayerCount;
   private GameObject chiefplayer; 
   private GameObject textPlayerTurn;
   private GameObject Lose_Text;
   Vector2 Distance;
   Vector2 min;
   GameObject minObject;
   int j;
   GameObject[] objectsWithTag;
   
    bool AllEven=true;
    bool Bool_Attack=false;
    bool bool_magic;
 
    int bool_sp=0;
    SpriteRenderer sp;
    AudioScript _AudioScript;
    UI_Script ui_Script;
    List<Vector3> attackPos=new List<Vector3>();
    [HideInInspector] public  MapArea mapArea;
     public List<GameObject> obj_attack = new List<GameObject>();
    [HideInInspector] public bool Lose_Bool=true;
     [HideInInspector] public  static bool isflashing=true;
      [HideInInspector] public  static bool inCroutine=true;
    [HideInInspector] public EnemyHP player;
    [HideInInspector] public  Vector3 _vector3;
   [HideInInspector] List<Vector3> newposition=new List<Vector3>();
    void Start()
    {
        startcount=count;
        EnemyMove_left=new Vector3(-dis,0.0f,0.0f);
        EnemyMove_right=new Vector3(dis,0.0f,0.0f);
        EnemyMove_up=new Vector3(0.0f,dis,0.0f);
        EnemyMove_down=new Vector3(0.0f,-dis,0.0f);
        moves=FindObjectsOfType<Move>();
        foreach(Move i in moves){
            if(i.Player_Number==1){
                chiefplayer=i.gameObject;
            }
        }
        //panel=GetComponent<Image>();
        sp=gameObject.GetComponent<SpriteRenderer>();
        _AudioScript=FindObjectOfType<AudioScript>();
        ui_Script=FindObjectOfType<UI_Script>();
        textPlayerTurn=ui_Script.textPlayerTurn;
        Lose_Text=ui_Script.loseText;
        _Enemycount=FindObjectOfType<EnemyCount>();
        mapArea=FindObjectOfType<MapArea>();
        
   
      move=FindObjectOfType<Move>();
      
     
    objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
    gameObject.GetComponent<Renderer>().material.color=new Color(1.0f,1.0f,1.0f,0.0f);
    foreach(GameObject obj in obj_attack){
        obj.SetActive(false);
    }
    }

    // Update is called once per frame
    void Update()
    {
        moves=FindObjectsOfType<Move>();
        move=FindObjectOfType<Move>();
     PlayerCount=GameObject.FindGameObjectsWithTag("Player");
     if(chiefplayer==null&&Lose_Bool){ //主人公が死んだらゲームオーバーになる
        StartCoroutine(PlayerLose());
        Lose_Bool=false;
     }
         
      }
    IEnumerator PlayerLose(){
        _AudioScript.bgm_enemy.Stop();
        Lose_Text.SetActive(true);
        ui_Script.retryButton.SetActive(true);
        ui_Script.loseSelectButton.SetActive(true);
        ui_Script.movementButton.SetActive(false); ui_Script.fourButton.SetActive(false);
        ui_Script.pauseButton.SetActive(false); 
        foreach(Move m in moves){
            Destroy(m.gameObject);
        }
        StopCoroutine(EnemyCoroutine());
        yield return null;
    } 
    
 public IEnumerator EnemyCoroutine(){
    
   AllEven=true;
    move.Enemyact=0;
      player=null;
      preposition=transform.position;
      List<Transform> objList=new List<Transform>();
        foreach(GameObject _obj in obj_attack){
          objList.Add(_obj.transform); }
         if(!mapArea.AreaCheck(gameObject,transform.position,dis,count,objList)){ //ここで攻撃範囲内にプレイヤーがいるかどうか確認bool
            yield return new WaitForSeconds(1.5f);
            mapArea.DestroyCube();
            count=0; 
            MovementStatusEffect();
            if(move!=null)  move.Enemyact=1;
        yield break;} //ここで行動終了
   foreach(GameObject g in obj_attack){
    g.SetActive(true);
   }

    yield return new WaitForSeconds(1.5f);
    mapArea.DestroyCube();
     List<Transform> objAttackArea=new List<Transform>();
        foreach(GameObject _obj in obj_attack){
          objAttackArea.Add(_obj.transform);
        }
        attackRoute.Clear();
        int routeNumber=0;
    mapArea.AreaMove(gameObject,transform.position,dis,count,objAttackArea,ref attackRoute);
    Debug.Log(gameObject.name);
    if(attackRoute.Count==0){
        count=0; 
            MovementStatusEffect();
            if(move!=null)  move.Enemyact=1;
            yield break;
    } 
    foreach(Vector3 v in attackRoute){
            Debug.Log(v);
        }
    for( j=startcount; j>=1; j--){
        if(gameObject!=null){
    
    foreach(GameObject o in obj_attack){
            o.GetComponent<Renderer>().material.color=new Color(1.0f,0.0f,0.0f,0.0f);
         }
    
                   
 
    PlayerCount=GameObject.FindGameObjectsWithTag("Player");
    
    
    Vector3 tr=transform.position; //enemy
        newposition.Clear();
        newposition.Add(tr+EnemyMove_right);
        newposition.Add(tr+EnemyMove_left);
        newposition.Add(tr+EnemyMove_up);
        newposition.Add(tr+EnemyMove_down);
        
        
        newposition_one=attackRoute[routeNumber];
        routeNumber++;
}
    
 _vector3=newposition_one-transform.position;
 afterDirection=NewPositionDirection(_vector3);
 
   if(AllEven){ //エネミーが動いている時の処理
    if(newposition_one==transform.position+EnemyMove_right) bool_sp=1; if(newposition_one==transform.position+EnemyMove_left) bool_sp=2;
    if(newposition_one==transform.position+EnemyMove_up||newposition_one==transform.position+EnemyMove_down) bool_sp=0;
     if(bool_sp!=0){
    sp.flipX= bool_sp==1? true:false;
     }
     while(l<=1.0f){
    l+=Time.deltaTime*2.0f;
     transform.position=  Vector3.Lerp(preposition,newposition_one,l);
     yield return null;
     } 
     yield return null;
     foreach(GameObject obj in obj_attack){
       
        
        obj.transform.RotateAround(transform.position,Vector3.forward,(afterDirection-beforeDirection)*90);
       
     }
    beforeDirection=afterDirection;
    preposition=transform.position;
   l=0.0f;
  GameObject[] positions = new GameObject[obj_attack.Count]; //攻撃範囲を取得
for(int i=0; i<obj_attack.Count; i++){
    if (obj_attack[i] != null) 
        {
            positions[i] = obj_attack[i];
        }
}
    if(percentage_magic1>=Random.value){
        StartCoroutine(OnMagic(positions));
    }else{
   StartCoroutine(OnAttack(positions));} //動いた際の攻撃
   }else{ //エネミーが止まっているときの処理
if(newposition_one==transform.position+EnemyMove_right) bool_sp=1; if(newposition_one==transform.position+EnemyMove_left) bool_sp=2;
    if(newposition_one==transform.position+EnemyMove_up||newposition_one==transform.position+EnemyMove_down) bool_sp=0;
     if(bool_sp!=0){
    sp.flipX= bool_sp==1? true:false;
     }
     foreach(GameObject obj in obj_attack){
        obj.SetActive(true);
        obj.transform.RotateAround(transform.position,Vector3.forward,(afterDirection-beforeDirection)*90);
     }
     beforeDirection=afterDirection;
    
GameObject[] positions = new GameObject[obj_attack.Count];
for(int i=0; i<obj_attack.Count; i++){
    if (obj_attack[i] != null) 
        {
            positions[i] = obj_attack[i];
        }
}

StartCoroutine(OnAttack(positions)); 
   }
   if(Bool_Attack)
    yield return new WaitForSeconds(3.0f);
    Bool_Attack=false;
    count--;
    }
    for(int i=0; i<obj_attack.Count; i++){
    obj_attack[i].SetActive(false);
    obj_attack[i].GetComponent<Renderer>().material.color=new Color(1.0f,0.0f,0.0f,1.0f);
    }
   
     yield return new WaitForSeconds(1);
    MovementStatusEffect();
    if(move!=null)
   move.Enemyact=1;
    
 }
public IEnumerator EnemyTurnEnd(){
    if(Lose_Bool){
        yield return new WaitForSeconds(1.5f);
_AudioScript.bgm_enemy.Stop();
textPlayerTurn.SetActive(true);
Time.timeScale=0;
float timer = 0;
    float duration = 3;

    while (timer < duration)
    {
       

        // タイマーを更新
        timer += Time.unscaledDeltaTime;
        yield return null;  // 次のフレームまで待機
    }
    mapArea.DestroyCube();
    FindObjectOfType<UI_Script>().endButton.SetActive(true);
    textPlayerTurn.SetActive(false);
    Time.timeScale=1;
    _Enemycount.i=0;
    
    _AudioScript.bgm.Play();
    foreach(Move m in moves){
        m.turn=true;
    }
    obj_attack_array=GameObject.FindGameObjectsWithTag("EneCubeAttack");
    foreach(GameObject i in obj_attack_array){
        i.SetActive(false);
    }
     
     move.Enemyact=0;
}else{
    _AudioScript.bgm_enemy.Stop();
}
}
IEnumerator OnAttack(GameObject[] poses){
    Vector3 directionAttack=Vector3.zero;
    attackPos.Clear();
    foreach(GameObject obj in poses){
        Transform attackTransform=obj.transform;
            
            for(int i=1; i<=4; i++){
            attackTransform.RotateAround(transform.position,Vector3.forward,90.0f);
            attackPos.Add(attackTransform.position);
            }
          
    }
    Collider2D _collider=null;
    bool  Playerbool = false; //playerを見つけたかどうか
    foreach(Vector3 pos in attackPos){
       
   Collider2D[]  colliders = Physics2D.OverlapCircleAll(pos,0.5f);
    
                
    foreach (Collider2D collider in colliders)
        {
          
            if(collider.CompareTag("Player")&&pos==attackRoute[attackRoute.Count-1]){
                    Playerbool=true;
                    _collider=collider;
                    directionAttack=pos-transform.position;
                   afterDirection= NewPositionDirection(directionAttack);
                   foreach(GameObject obj in obj_attack){
                    
                   obj.transform.RotateAround(transform.position,Vector3.forward,(afterDirection-beforeDirection)*90);
                      }
                     
                    beforeDirection=afterDirection;
                    break;
            }
            } 
           
        }

   
    if(Playerbool){

         j=0; count=0;

       Bool_Attack=true;
         player=_collider.GetComponent<EnemyHP>();
         foreach(GameObject o in obj_attack){
            o.GetComponent<Renderer>().material.color=new Color(1.0f,0.0f,0.0f,1.0f);
         }
         
        yield return new WaitForSeconds(0.5f);
        transform.position+=directionAttack*0.2f;
        yield return new WaitForSeconds(0.8f);
        for(int i=1; i<=3; i++){
                player.gameObject.GetComponent<Renderer>().material.color=new Color(1.0f,1.0f,1.0f,0.0f);
                
                yield return new WaitForSeconds(0.1f);
                player.gameObject.GetComponent<Renderer>().material.color=new Color(1.0f,1.0f,1.0f,1.0f);
                yield return new WaitForSeconds(0.1f);}
       StartCoroutine(player.TakeDamage(AttackPoint));
        if(!player.bool_avoid)  StartCoroutine(DamageEffect(player));
        yield return new WaitForSeconds(0.3f);
        transform.position-=directionAttack*0.2f;
       
       
    
   
    }
}
IEnumerator OnMagic(GameObject[] poses){
    attackPos.Clear();
    foreach(GameObject obj in poses){
        Transform attackTransform=obj.transform;
            
            for(int i=1; i<=4; i++){
            attackTransform.RotateAround(transform.position,Vector3.forward,90.0f);
            attackPos.Add(attackTransform.position);
            }
          
    }
     Collider2D _collider=null;
    bool  Playerbool = false; //playerを見つけたかどうか
   
    foreach(Vector3 pos in attackPos){
       
   Collider2D[]  colliders = Physics2D.OverlapCircleAll(pos,0.5f);
    
                
    foreach (Collider2D collider in colliders){
        if(!bool_magic&&collider.CompareTag("Player")){
                    Playerbool=true;
                    _collider=collider;
                    break;
            }else if(bool_magic&&collider.CompareTag("Enemy")){

            }
    }
}
if(Playerbool){
         j=0; count=0;
       Bool_Attack=true;
         player=_collider.GetComponent<EnemyHP>();
        
       
        yield return new WaitForSeconds(0.4f);
        for(int i=1; i<=3; i++){
                player.gameObject.GetComponent<Renderer>().material.color=new Color(1.0f,1.0f,1.0f,0.0f);
                yield return new WaitForSeconds(0.1f);
                player.gameObject.GetComponent<Renderer>().material.color=new Color(1.0f,1.0f,1.0f,1.0f);
                yield return new WaitForSeconds(0.1f);}
       StartCoroutine(player.TakeDamage_Magic(EnemyNumber,AttackPoint,gameObject));
        yield return new WaitForSeconds(0.3f);
        
       
     
   
    }
}
IEnumerator DamageEffect(EnemyHP _player){

   yield return new WaitForSeconds(0.2f);
    if(_player!=null){
    _player.GetComponent<Renderer>().material.color=new Color(1.0f,0.0f,0.0f,1.0f);
    
        yield return new WaitForSeconds(0.1f);
    }
        if(_player!=null){
        _player.GetComponent<Renderer>().material.color=new Color(1.0f,1.0f,1.0f,1.0f);
        yield return new WaitForSeconds(0.1f);
        }
   

}
float Euclid(Vector3 p1,Vector3 p2){
    float p3;
    p3=(p1.x-p2.x)*(p1.x-p2.x)+(p1.y-p2.y)*(p1.y-p2.y);
    return p3;
}
int NewPositionDirection(Vector3 vec3){
    if(vec3.x>0.1f) return 0;
    if(vec3.x<-0.1f) return 2;
    if(vec3.y>0.1f) return 1;
    if(vec3.y<-0.1f) return 3;
    return -1;
}
public void OnClick(){
    
   PanelSet panel=FindObjectOfType<PanelSet>();
   if(!panel.bool_click) return;
    if(move.turn){
         _AudioScript.audio_select.Play();
        
       
        
      foreach (Move m in moves)
        {
            m.cube_attack.ForEach(_cubeAttack => _cubeAttack.SetActive(false));
        }
       
       List<Transform> objList=new List<Transform>();
        foreach(GameObject _obj in obj_attack){
          objList.Add(_obj.transform);
        }
        
        mapArea.SuperMap(gameObject,transform.position,dis,count,objList);
       
       
         StartCoroutine(EnemyFlashing());
}
}
 IEnumerator EnemyFlashing(){
         isflashing=false;
         yield return new WaitForSeconds(0.8f);
         
         if(inCroutine){ 
        inCroutine=false;
        
         isflashing=true;
        Renderer enemyRenderer=gameObject.GetComponent<Renderer>();
        while(isflashing){
           
            enemyRenderer.enabled=!enemyRenderer.enabled;
            for(int i=0; i<10; i++){
                if(!isflashing) break;
                yield return new WaitForSeconds(0.05f);

            }
            
        }
        enemyRenderer.enabled=true;
        inCroutine=true;
    }
}
public void MovementStatusEffect(){
        if(count<=0){
            gameObject.GetComponent<SpriteRenderer>().color= new Color(0.5f,0.5f,0.5f,1.0f);
        }else if(count>0){
            gameObject.GetComponent<SpriteRenderer>().color= new Color(1.0f,1.0f,1.0f,1.0f);
        }
    }
}
