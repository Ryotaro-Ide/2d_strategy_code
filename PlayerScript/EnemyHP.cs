using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyHP : MonoBehaviour
{
    public int maxHP = 2;
   public int currentHP=0;
   public static  int manybool_action=0;
   private float _time=1.0f;
   public float criticalpercentage=0.01f;
   public float avoidpercentage=0.02f;
   Move[] moves;
   EnemyManager[] enemies;
   Move _move;
   Move _Move;
   EnemyManager _enemyManager;
   EnemyManager enemy;
   EnemyCount _enemycountcs;
   PanelSet _panelset;
   EnemyHP _enemyHP;
   Renderer _renderer;
   MapArea mapArea;
  public AttackButton _attackbutton;
   public GameObject _destroyobj;
  [HideInInspector] public GameObject _AttackButton;
  public TextMeshProUGUI HPtext;
  public TextMeshProUGUI player_Attacktext;
  public TextMeshProUGUI enemy_Attacktext;
  AudioScript _AudioScript;
  
  
  private Animator anim;
 [HideInInspector] public bool bool_avoid=false;
 [HideInInspector] public bool bool_isattack=true;
    // Start is called before the first frame update
    void Start()
    {
        manybool_action=0;
        currentHP = maxHP;
        _AudioScript=FindObjectOfType<AudioScript>();
        moves=FindObjectsOfType<Move>();
        _move=GetComponent<Move>();
        _enemyManager=GetComponent<EnemyManager>();
        if(_move!=null){
        /*if(_move.Player_Number==1){
            avoidpercentage=0.05f; criticalpercentage=0.05f;
        }else if(_move.Player_Number==2){
             avoidpercentage=0.03f; criticalpercentage=0.03f;
        
        }*/
        }
        _enemycountcs=FindObjectOfType<EnemyCount>();
        _Move=FindObjectOfType<Move>();
        enemy=FindObjectOfType<EnemyManager>();
        enemies=FindObjectsOfType<EnemyManager>();
        _panelset=FindObjectOfType<PanelSet>();
        _renderer=gameObject.GetComponent<Renderer>();
        anim=GetComponent<Animator>();
        mapArea=FindObjectOfType<MapArea>();
         _enemyHP=gameObject.GetComponent<EnemyHP>();
         
    }

    // Update is called once per frame
   void FixedUpdate()
    {
        
        _attackbutton=FindObjectOfType<AttackButton>();
        UpdateText();
    }

    void UpdateText()
    {
        if (HPtext != null)
        {
            HPtext.text =currentHP.ToString();
        }
        if (player_Attacktext != null)
        {
            player_Attacktext.text =_move.AttackPoint.ToString();
        }
        if (enemy_Attacktext != null)
        {
            enemy_Attacktext.text =_enemyManager.AttackPoint.ToString();
        }
    }
    public IEnumerator TakeDamage(int damageAmount)
    {
        
       if(gameObject.tag=="Enemy")  _enemyHP=_attackbutton.move.GetComponent<EnemyHP>();
       if(gameObject.tag=="Player"){ 
        foreach(EnemyManager i in enemies){
       if(i.player!=null){   _enemyHP=i.player.GetComponent<EnemyHP>();
        break;
       }
       }
       }
        bool_avoid=false;
        if(avoidpercentage<=Random.value){
            if(_enemyHP!=null&&_enemyHP.criticalpercentage>=Random.value){ //クリティカル
            _AudioScript.audio_critical.Play();
            StartCoroutine(_panelset.CriticalPanel());
            Time.timeScale=0;
            
            yield return new WaitForSecondsRealtime(1.0f);
            if(gameObject.tag=="Enemy") manybool_action=2;
            if(gameObject.tag=="Player") manybool_action=12;
            Time.timeScale=1;
            currentHP -= 2*damageAmount;
             }else{currentHP -= damageAmount; manybool_action=1; //通常
             if(currentHP>0)  _AudioScript.audio_slash.Play(); 
             gameObject.transform.position+=new Vector3(0.2f,0.0f,0.0f);
             yield return new WaitForSeconds(0.02f);
             gameObject.transform.position+=new Vector3(-0.4f,0.0f,0.0f);
            yield return new WaitForSeconds(0.02f);
             gameObject.transform.position+=new Vector3(0.2f,0.0f,0.0f);
           }
        }else{
            StartCoroutine(Avoidance());
            bool_avoid=true;
            manybool_action=3;
        }
       
        if (currentHP <= 0)
        {
            
            if(gameObject.tag=="Player"){
                StartCoroutine(PlayerDestroy());
               StopCoroutine(TakeDamage( damageAmount));
            }
            
            if(_enemycountcs.enemyCounts.Length!=1||gameObject.tag!="Enemy"){
                StartCoroutine("Destroy");
            }else{
                StartCoroutine("DelayDestroy");
            }
        }
    }
  
public IEnumerator TakeDamage_Magic(int Player_Number,int damageAmount,GameObject attacker)
{
    yield return StartCoroutine(ShakePosition()); // 位置変更の処理を呼び出し

    if (Player_Number == 1)
    {
        HealPlayer();
    }
    else if (Player_Number == 2)
    {
        AttackDown();
    }
    else if (Player_Number == 3)
    {
        TakeDamageEscape(damageAmount,attacker);
    }
    else if (Player_Number == -1)
    {
        SpeedUp();
    }

    yield return null;
}
   
    private void HealPlayer()
{
    manybool_action = 11;
    _AudioScript.audio_heal.Play();
    for (int i = 0; i <= 1; i++)
    {
        if (currentHP < maxHP)
        {
            currentHP += 1;
        }
        else
        {
            break;
        }
    }
}
private void AttackDown()
{
    manybool_action = 12;
    _AudioScript.audio_ATKdown.Play();
     
    if (gameObject.tag == "Enemy")
    {
        enemy_Attacktext.color = new Color(0.0f, 1.0f, 0.8f, 1.0f);
        if (_enemyManager.AttackPoint >= 1) _enemyManager.AttackPoint -= 1;
    }
    else
    {
        player_Attacktext.color = new Color(0.0f, 1.0f, 0.8f, 1.0f);
        if (_move.AttackPoint >= 1) _move.AttackPoint -= 1;
    }
}
private void SpeedUp()
{
    manybool_action = -10;
    _AudioScript.audio_speedup.Play();
    for (int i = 0; i <= 1; i++)
    {
        _move.count += 1;
    }
    _move.count_F = _move.count;
}
private void TakeDamageEscape(int damageAmount,GameObject attacker) //ダメージを与えた後、動いて行動可能
{
    StartCoroutine(TakeDamage( damageAmount));
    StartCoroutine(mapArea.AreaEscape(attacker,2.56f,1));
}
    IEnumerator PlayerDestroy(){
        Destroy(_destroyobj); 
        
        yield return new WaitForSecondsRealtime(_time);
        
        anim.SetBool("Death",true);
         _AudioScript.audio_death.Play();
        yield return new WaitForSecondsRealtime(0.4f);
        Destroy(this.gameObject);
    }
    IEnumerator DelayDestroy(){
        Destroy(_destroyobj); 
        
        StartCoroutine(_Move.GameClear());
        yield return new WaitForSecondsRealtime(_time);
        anim.SetBool("Death",true);
        manybool_action=5;
        yield return new WaitForSecondsRealtime(0.4f);
        Destroy(this.gameObject);
    }
    IEnumerator Destroy(){
       
        anim.speed=0;
        bool_isattack=false;
        Destroy(_destroyobj);  
        _AudioScript.audio_slash.Play(); 
        for(int i=6; i>=1; i--){
        _renderer.material.color=new Color(1.0f,1.0f,1.0f,0.0f);
        yield return new WaitForSeconds(0.05f*i);
        _renderer.material.color=new Color(1.0f,1.0f,1.0f,1.0f);
        yield return new WaitForSeconds(0.05f*i);}
        anim.speed=1;
        anim.SetBool("Death",true);
        manybool_action=4;
            _AudioScript.audio_death.Play();
            yield return new WaitForSeconds(0.4f);
            Destroy(this.gameObject); // HPが0以下になったら死亡処理を実行
            bool_isattack=true;
    }
    IEnumerator Avoidance(){
        _AudioScript.audio_avoid.Play();
        if(gameObject.tag=="Player") transform.position+=enemy._vector3*0.2f;
        if(gameObject.tag=="Enemy")  transform.position+=_attackbutton.move.directionAttackMove(_attackbutton.move.CancelDirection);
        yield return new WaitForSeconds(0.5f);
        if(gameObject.tag=="Player") transform.position-=enemy._vector3*0.2f;
        if(gameObject.tag=="Enemy")  transform.position-=_attackbutton.move.directionAttackMove(_attackbutton.move.CancelDirection);
      
    }
      private IEnumerator ShakePosition()
{
    gameObject.transform.position += new Vector3(0.2f, 0.0f, 0.0f);
    yield return new WaitForSeconds(0.02f);
    gameObject.transform.position += new Vector3(-0.4f, 0.0f, 0.0f);
    yield return new WaitForSeconds(0.02f);
    gameObject.transform.position += new Vector3(0.2f, 0.0f, 0.0f);
}

}
