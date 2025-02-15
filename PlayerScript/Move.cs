using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Move : MonoBehaviour
{
    public int Player_Number=1;
    public  int count=99;
    public  int count_F=99;
    private int startcount=10;
    private int TurnStop;
    public int AttackPoint=1;
    private int pre_AttackPoint;
    public int Enemyact;
    private int pre_turn;
    private int preDirection;
    float detectionRadius=1.0f;
    float dis=2.56f;
    private bool isNearbyTile=false;
    private bool bool_turncount=true;
    private bool bool_instantiate=true;
     public bool masicToPlayer; //trueなら味方に
     public bool attackMode=true;
    public GameObject prefab_cube;
    public GameObject cube;
    public GameObject white_cube;
    public GameObject Empty;
    private GameObject Clear_text;
    private GameObject textEnemyturn;
    GameObject cube1,cube2,cube3,cube4;
    private Vector2 tr;
    List <Vector3> cubeAttackStorage=new List<Vector3>();
    Transform parentTransform;   
    PanelSet _panelset;
    MapArea mapArea;
    Move[] move;
    EnemyCount _enemycountcs;
    EnemyManager[] enemyManager;
    AudioScript _AudioScript;
    UI_Script ui_Script;
    EnemyHP _enemyHP;
  
    public List<GameObject> cube_attack = new List<GameObject>();
    public List<Transform> objList=new List<Transform>();
    [HideInInspector] public Vector2 Cancel_pos;
   [HideInInspector] public Vector2 newposition;
   
   [HideInInspector] public bool turn=true;
   [HideInInspector] public bool Player_Select=false;
    
   [HideInInspector] public bool bool_clone=false; //trueでクローン出現
   [HideInInspector] public bool isClear=false;
   [HideInInspector] public bool PlayerTextWindow=false;
   
   [HideInInspector] public bool StopBool=false;
   [HideInInspector] public bool bool_EnemyClick=false; //クリックできるか判定
    
   [HideInInspector] public int _direction;
   [HideInInspector] public int CancelDirection;
   
  [HideInInspector] public  EnemyHP ColliderEnemy;
   
  [HideInInspector] public List<EnemyHP> list_ColliderEnemy=new List<EnemyHP>();
 [HideInInspector] public List<EnemyHP> list_ColliderPlayer=new List<EnemyHP>();
 
    // Start is called before the first frame update
    void Start()
    {   pre_AttackPoint=AttackPoint;
         parentTransform = transform;
        
        startcount=count;
        ui_Script=FindObjectOfType<UI_Script>();
         textEnemyturn=ui_Script.textEnemyturn;
         Clear_text=ui_Script.clearText;
        _AudioScript=FindObjectOfType<AudioScript>();
        _panelset=FindObjectOfType<PanelSet>();
        _enemycountcs=FindObjectOfType<EnemyCount>();
        mapArea=FindObjectOfType<MapArea>();
        _enemyHP=GetComponent<EnemyHP>();
       
        CancelDirection=0;
        _direction=0;
        
        Enemyact=0;
        turn=true;
         ui_Script.attackButton.SetActive(false);
        ui_Script.magicButton.SetActive(false);
        newposition=transform.position;
        tr = transform.position;
        
        
        Cancel_pos=tr;
       
        TurnStop=0;
       
    }
    void OnMove(){
         _AudioScript.audio_move.Play();
          preDirection=_direction;
         count_F--;
         ui_Script.attackButton.SetActive(false);
                ui_Script.magicButton.SetActive(false);
    }
    void Update()
    {   
        if(pre_AttackPoint!=AttackPoint){//攻撃力が下げられたときの条件

       if(bool_turncount){     pre_turn=_enemycountcs.TurnCount; bool_turncount=false;}
       if(pre_turn==_enemycountcs.TurnCount-2){ AttackPoint=pre_AttackPoint; _enemyHP.player_Attacktext.color=new Color(1.0f,1.0f,1.0f,1.0f);  bool_turncount=true; }
            
        }
        if(PlayerTextWindow){
             ui_Script.countText.text="行動力:" + count_F.ToString();
             ui_Script.playerText.text=gameObject.name.ToString();
        }
        move=FindObjectsOfType<Move>();
        enemyManager=FindObjectsOfType<EnemyManager>();
        if(Input.GetKeyDown(KeyCode.L)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(turn){
            MovementStatusEffect();
            TurnStop=0;
        if(gameObject!=null&&Player_Select){
         // tr 位置に近くに "Tile" タグを持つオブジェクトがあるかを判定する
        
           
            
        if (count_F >= 1&&!StopBool)
        {
            
if (( Input.GetKeyDown(KeyCode.D)||Stick.bool_stick==1))
            {
                 Stick.bool_stick=0;
                newposition+=new Vector2(dis,0.0f);
                IsMove();
        if(isNearbyTile){
                OnMove();
                  _direction=0;
                 
                cube.transform.position += new Vector3(dis, 0.0f, 0.0f);
                foreach(GameObject obj in cube_attack){
                    obj.SetActive(true);
                    
                     obj.transform.position=direction(obj,cube.transform.position,preDirection,_direction);
                }
            
                tr += new Vector2(dis, 0.0f);
            
            }else{
                newposition+=new Vector2(-dis,0.0f);
                _AudioScript.audio_fail.Play();
            }
            }
 if (tr.x > -6.5f && (Input.GetKeyDown(KeyCode.A)||Stick.bool_stick==2))
              {
               Stick.bool_stick=0;
                newposition+=new Vector2(-dis,0.0f);
                IsMove();
        if(isNearbyTile){
                 OnMove();
                 _direction=2;
                
                cube.transform.position += new Vector3(-dis, 0.0f, 0.0f);
                foreach(GameObject obj in cube_attack){
                    obj.SetActive(true);
                    
                        obj.transform.position=direction(obj,cube.transform.position,preDirection,_direction);
                }
                    
                    tr += new Vector2(-dis, 0.0f);
                    
                    
                    
             }else{
                newposition+=new Vector2(dis,0.0f);
                _AudioScript.audio_fail.Play();
             }
              }
 if ( (Input.GetKeyDown(KeyCode.W)||Stick.bool_stick==3))
                  {
                    Stick.bool_stick=0; 
                newposition+=new Vector2(0.0f,dis);
                IsMove();
        if(isNearbyTile){
                     OnMove();
                 _direction=1;
                
                cube.transform.position += new Vector3(0.0f, dis, 0.0f);
                foreach(GameObject obj in cube_attack){
                    obj.SetActive(true);
                    
                        obj.transform.position=direction(obj,cube.transform.position,preDirection,_direction);
                }
                        
                        tr += new Vector2(0.0f, dis);
                        
                        
                        
                  }else{
                    newposition+=new Vector2(0.0f,-dis);
                    _AudioScript.audio_fail.Play();
                  }
                  }
 if ( (Input.GetKeyDown(KeyCode.S)||Stick.bool_stick==4))
         {
                Stick.bool_stick=0;
                newposition+=new Vector2(0.0f,-dis);
                IsMove();
        if(isNearbyTile){
                          OnMove();   
                 _direction=3;
                
                cube.transform.position += new Vector3(0.0f, -dis, 0.0f);
                foreach(GameObject obj in cube_attack){
                    obj.SetActive(true);
                    
                        obj.transform.position=direction(obj,cube.transform.position,preDirection,_direction);
                }
                            
                            tr += new Vector2(0.0f, -dis);
                            
                            
                           
                       }else{
                        newposition+=new Vector2(0.0f,dis);
                        _AudioScript.audio_fail.Play();
                       }
                      }
        }else if(count_F<=0&&!StopBool){
            EnemyHP.manybool_action=7; 
           if(Stick.bool_stick>=1&&Stick.bool_stick<=4){
            Stick.bool_stick=0;
            _AudioScript.audio_fail.Play();
           }
        }
        if(Input.GetKeyDown(KeyCode.F)||Stick.bool_stick==5)
        {
            Stick.bool_stick=0;
            if(!IsEnemyPresent(tr)){
                mapArea.DestroyCube();
             ui_Script.directionButton.SetActive(true);
            ui_Script.cancelButton.SetActive(false);
            transform.position = tr;
             cube.transform.position=transform.position;
            if(_direction==2){
                parentTransform.rotation=Quaternion.Euler(0f, 180f, 0f);
                Empty.transform.rotation=Quaternion.Euler(0f, 0f, 0f);
            }else{
            parentTransform.rotation=Quaternion.Euler(0f, 0f, 0f);
            Empty.transform.rotation=Quaternion.Euler(0f, 0f, 0f);
            }
            EnemyManager.isflashing=false;
            bool_clone=false;
            Cancel_pos=tr;
           count=count_F;
           CancelDirection=_direction;
           cubeAttackStorage.Clear();
           
        foreach (GameObject i in cube_attack)
        {
      if (i != null) // 配列の要素がnullでないことを確認
        {
            cubeAttackStorage.Add(i.transform.position);
        }
        }
        var StrageArray=cubeAttackStorage.ToArray();
        IsEnemyAttack(StrageArray);
            }else{
                _AudioScript.audio_fail.Play();
            }
        }
        if(Input.GetKeyDown(KeyCode.C)||Stick.bool_stick==6){ //移動キャンセル
            MoveCancel();
        }
        if(Stick.bool_stick==7&&count>=1){ //Direcion
        int directionCount=_direction;
        ui_Script.directionButton.SetActive(false);
        ui_Script.cancelButton.SetActive(true);
       
       
       if(bool_instantiate){
        bool_instantiate=false;
        _AudioScript.audio_button.Play();
         cube1=Instantiate(white_cube,transform.position+new Vector3(dis,0.0f,0.0f),Quaternion.identity);
         cube2=Instantiate(white_cube,transform.position+new Vector3(-dis,0.0f,0.0f),Quaternion.identity);
         cube3=Instantiate(white_cube,transform.position+new Vector3(0.0f,dis,0.0f),Quaternion.identity);
         cube4=Instantiate(white_cube,transform.position+new Vector3(0.0f,-dis,0.0f),Quaternion.identity);
       }
            if(Stick.bool_direct_stick==1){ //right
            _direction=0;
            parentTransform.rotation=Quaternion.Euler(0f, 0f, 0f);
            Empty.transform.rotation=Quaternion.Euler(0f, 0f, 0f);
            cube.transform.rotation=Quaternion.Euler(0f, 0f, 0f);
            for(int i=0; i<cube_attack.Count; i++){
            cube_attack[i].transform.position=direction(cube_attack[i],transform.position,directionCount,_direction);
            }
            }else if(Stick.bool_direct_stick==2){ //left
                _direction=2;
                 parentTransform.rotation=Quaternion.Euler(0f, 180f, 0f);
                Empty.transform.rotation=Quaternion.Euler(0f, 0f, 0f);
                cube.transform.rotation=Quaternion.Euler(0f, 0f, 0f);
               for(int i=0; i<cube_attack.Count; i++){
            cube_attack[i].transform.position=direction(cube_attack[i],transform.position,directionCount,_direction);
            }
            }else if(Stick.bool_direct_stick==3){ //up
                _direction=1;
                for(int i=0; i<cube_attack.Count; i++){
            cube_attack[i].transform.position=direction(cube_attack[i],transform.position,directionCount,_direction);
            }
            }else if(Stick.bool_direct_stick==4){ //down
                _direction=3;
                for(int i=0; i<cube_attack.Count; i++){
            cube_attack[i].transform.position=direction(cube_attack[i],transform.position,directionCount,_direction);
            }
            }else if(Stick.bool_direct_stick==5){
                Stick.bool_direct_stick=0;
                Stick.bool_directing=false;
                Stick.bool_stick=5;
                count--; count_F=count;
                Destroy(cube1); Destroy(cube2); Destroy(cube3); Destroy(cube4);
                bool_instantiate=true;
            }
        }else if(Stick.bool_stick==7&&count<=0){
            
       _AudioScript.audio_fail.Play();
        Stick.bool_direct_stick=0;
        Stick.bool_directing=false;
        Stick.bool_stick=0;
        
       
        }
        } 
    }else{
       
        if(TurnStop==0){

             _AudioScript.bgm.Stop();
        StartCoroutine("EnemyTurn");
        cube.transform.position=transform.position;
        tr=transform.position;
        foreach(GameObject obj in cube_attack){
            obj.SetActive(false);
        }
        count=startcount;
        count_F=startcount;
        TurnStop++;
        
        }
    }
        }
    
     void MoveCancel(){
        ui_Script.cancelButton.SetActive(false); ui_Script.directionButton.SetActive(true);
            if(cube1!=null){
                Destroy(cube1);  Destroy(cube2);  Destroy(cube3);  Destroy(cube4);
                Stick.bool_directing=false;
                bool_instantiate=true;
            }
            bool_clone=false;
            Stick.bool_stick=0;
            count_F=count;
            cube.transform.position=transform.position;
            newposition=Cancel_pos;
          
           foreach(GameObject obj in cube_attack){
            obj.transform.position=direction(obj,cube.transform.position,_direction,CancelDirection);
           }
          
            tr=Cancel_pos;
            _direction=CancelDirection;
         
     }
     bool IsEnemyPresent(Vector2 position)
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f); 

        foreach (Collider2D collider in colliders)
        {
            if(collider.gameObject!=gameObject){
            if (collider.CompareTag("Enemy")||collider.CompareTag("Player"))
                return true;
            }
        }

        return false;
    }
    void IsEnemyAttack(Vector3[] positions)
{
    if(!attackMode){
        count=0; count_F=0;
        attackMode=true;
        return;
    }
    ColliderEnemy=null;
    list_ColliderEnemy.Clear();
    foreach (Vector3 position in positions)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy") )
            {
                
                ui_Script.attackButton.SetActive(true);
               if(!masicToPlayer) ui_Script.magicButton.SetActive(true);
                 ColliderEnemy=collider.GetComponent<EnemyHP>();
                 list_ColliderEnemy.Add(ColliderEnemy);
                
            }else if(collider.CompareTag("Player")){
              if(masicToPlayer)  ui_Script.magicButton.SetActive(true);
              ColliderEnemy=collider.GetComponent<EnemyHP>();
            list_ColliderPlayer.Add(ColliderEnemy);
            
        }
        }
        
    }
    
EnemyHP.manybool_action=-3;
   
}
    void IsMove(){
            bool_clone=true;
            EnemyManager.isflashing=false;
Collider2D[] colliders = Physics2D.OverlapCircleAll(newposition, detectionRadius);
                  isNearbyTile = false;
                   objList.Clear();
            foreach(GameObject obj in cube_attack){
               objList.Add(obj.transform); }
                 mapArea.SuperMap(gameObject,transform.position,dis,count,objList);
        foreach (Collider2D collider in colliders)
        {
            if(collider.CompareTag("Enemy")){
                    isNearbyTile=false;
                    break;

            } 
            if (collider.CompareTag("Tile")||collider.CompareTag("Player")) //playerを感知した場合、そのタイルには移動できないように
            {
                // "Tile" タグを持つオブジェクトが存在する
                isNearbyTile = true;
                
            }
        }
        ui_Script.cancelButton.SetActive(true);
        ui_Script.directionButton.SetActive(false);
    }
    IEnumerator EnemyTurn(){
        yield return new WaitForSeconds(0.2f);
    EnemyManager.isflashing=false;    
    Player_Select=false;
    EnemyHP.manybool_action=0;
    ui_Script.attackButton.SetActive(false); ui_Script.magicButton.SetActive(false); ui_Script.endButton.SetActive(false); ui_Script.directionButton.SetActive(false); ui_Script.cancelButton.SetActive(false);
    Time.timeScale = 0;  // 時間停止
    textEnemyturn.SetActive(true);
    MovementStatusEffect();
    // 2秒間の待機
    float timer = 0;
    float duration = 3;

    while (timer < duration)
    {
       

        // タイマーを更新
        timer += Time.unscaledDeltaTime;
        yield return null;  // 次のフレームまで待機
    }
    mapArea.DestroyCube();
    textEnemyturn.SetActive(false);
   _AudioScript.bgm_enemy.Play();
    Time.timeScale = 1;  // 時間再開
    Enemyact=1;
   // turn=true;
    }
  public  IEnumerator GameClear(){
        ui_Script.endButton.SetActive(false);
        StopBool=true;
        _AudioScript.bgm.Stop();
        _AudioScript.audio_win.Play();
        isClear=true;
        Time.timeScale=0;
        ui_Script.fourButton.SetActive(false);
          ui_Script.movementButton.SetActive(false);
        yield return new WaitForSecondsRealtime(1.0f);
         Time.timeScale=1;
        foreach(Move i in move){
           
        i.StopBool=true;
        }
        ui_Script.oKButton.SetActive(true);
        ui_Script.nextButton.SetActive(true);
         Clear_text.SetActive(true);
         
     
    }
   public  Vector2 direction(GameObject obj,Vector2 p,int afterDir,int beforeDir){ //回転させるオブジェクト、中心、移動前、移動後
        
        obj.transform.RotateAround(p,Vector3.forward,(beforeDir-afterDir)*90);
        return obj.transform.position;
    }
    public Vector3 directionAttackMove(int CancelDir){
        if(CancelDir==0) return new Vector3(0.5f,0.0f,0.0f);
        if(CancelDir==1) return new Vector3(0.0f,0.5f,0.0f);
        if(CancelDir==2) return new Vector3(-0.5f,0.0f,0.0f);
        if(CancelDir==3) return new Vector3(0.0f,-0.5f,0.0f);
        return new Vector3(0.0f,0.0f,0.0f);
    }
    public void OnClick(){
       
       Move[] isAttakking=FindObjectsOfType<Move>();
       foreach(Move m in isAttakking){
        if(m.StopBool) return;
       }
        if(_panelset.bool_click&&turn&&Stick.bool_stick!=7){
           _AudioScript.audio_select.Play();
           foreach(Move i in move){
            i.MoveCancel();
            i.ColliderEnemy=null; i.bool_clone=false;  i.PlayerTextWindow=false;  i.Player_Select=false;
            
            foreach(GameObject objAttack in i.cube_attack){
               
                 objAttack.SetActive(false);
            }
           
        } 
            objList.Clear();
           
         ui_Script.attackButton.SetActive(false); ui_Script.magicButton.SetActive(false);  ui_Script.cancelButton.SetActive(false);  ui_Script.directionButton.SetActive(true);
        
        Stick.bool_stick=5;//決定
        newposition=transform.position;
        ColliderEnemy=null;
    list_ColliderEnemy.Clear();
        PlayerTextWindow=true;
        Player_Select=true;
        EnemyManager.isflashing=true;
        EnemyHP.manybool_action=6; 
        foreach(GameObject obj in cube_attack){
            
                obj.SetActive(true);
                objList.Add(obj.transform);}    
        
        
        StartCoroutine(PlayerFlashing());
   
}
    }
    IEnumerator PlayerFlashing(){
        yield return null;
        mapArea.SuperMap(gameObject,transform.position,dis,count,objList);
        EnemyManager.isflashing=false;
        yield return new WaitForSeconds(0.8f);
        if(EnemyManager.inCroutine){ 
        EnemyManager.inCroutine=false;
        
         Renderer playerRenderer=gameObject.GetComponent<Renderer>();
        playerRenderer.enabled=true;
         EnemyManager.isflashing=true;
       
        while(EnemyManager.isflashing){
           
            playerRenderer.enabled=!playerRenderer.enabled;
            yield return new WaitForSeconds(0.5f);
        }
        EnemyManager.inCroutine=true;
        playerRenderer.enabled=true;
    }
    }
    void MovementStatusEffect(){
        if(count<=0){
            gameObject.GetComponent<SpriteRenderer>().color= new Color(0.5f,0.5f,0.5f,1.0f);
        }else if(count>0){
            gameObject.GetComponent<SpriteRenderer>().color= new Color(1.0f,1.0f,1.0f,1.0f);
        }
    }
}