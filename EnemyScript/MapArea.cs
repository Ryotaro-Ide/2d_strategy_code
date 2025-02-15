
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//全てのタイルを取得し、敵か障害物のあるタイルは除外し、最初の1行動力で移動できる場所を取得し、そこから蜘蛛の巣のように行ける場所を調べていく
public class MapArea : MonoBehaviour
{
    public Tilemap groundMap;
    public GameObject cube;
    public GameObject attackCube;
    public GameObject targetFrame;
    GameObject cube1;
    public List<GameObject> cubes=new List<GameObject>();
    Vector3 targetPlayer;
    bool isNoCubeArea=true;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SuperMap(GameObject target ,Vector3 tr, float dis, int count,List<Transform> objList )
    {
        
        foreach(GameObject c in cubes){
            Destroy(c);
        }
        cubes.Clear(); 
        Vector3 newpos = tr;
        List<Vector3> points = new List<Vector3>(); //移動はできるが滞在はできない
        List<Vector3> truePoints = new List<Vector3>();//移動も滞在もできる
        points.Add(newpos); truePoints.Add(newpos);
        int pCount = 1;
        Vector3[] fourDir =GetDirections(dis);
        

        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < pCount; j++)
            {
                newpos = points[j];
                for (int k = 0; k < 4; k++)
                {
                    Vector3 currentPos = points[j];
                foreach (Vector3 dir in fourDir)
                {
                    Vector3 newPos = currentPos + dir;
                    if (IsValidTile(newPos, target, points))
                    {
                        points.Add(newPos);
                        if (isPlayerCollision(newPos,target)) truePoints.Add(newPos);
                    }
                    }
                }
            }
            pCount = points.Count;
        }
            
        foreach (Vector3 point in truePoints)
        {
          cube1=  Instantiate(cube, point, Quaternion.identity);
            cubes.Add(cube1);
        }
         if(target.CompareTag("Player")) return;
    foreach(Vector3 cubeposition in truePoints){
                foreach(Transform obj_attack in objList){
                    
                    obj_attack.position-=tr-cubeposition; //仮の点に存在する攻撃範囲
            
            for(int i=1; i<=4; i++){
            obj_attack.RotateAround(cubeposition,Vector3.forward,90.0f);
             isNoCubeArea=true;
            foreach(GameObject vec in cubes){
            float distance=Vector3.Distance(obj_attack.position,vec.transform.position);

                if(distance<=0.5f) isNoCubeArea=false;
            }
            Vector3Int cellPos = groundMap.WorldToCell(obj_attack.position);
              TileBase   tile=groundMap.GetTile(cellPos);
            if(isNoCubeArea&&tile!=null&&isEnemyCollision(obj_attack.position)){
                
                cube1=Instantiate(attackCube,obj_attack.position,Quaternion.identity);
                cubes.Add(cube1);
            }
            }
          obj_attack.position+=tr-cubeposition;
                }
        
    
    }
    if(objList.Count>=2) Debug.Log(objList.Count);
    }
    public void DestroyCube(){
        foreach(GameObject c in cubes){
            Destroy(c);
        }
        cubes.Clear();
    }
    public bool AreaCheck(GameObject target,Vector3 tr, float dis, int count ,List<Transform> objList){
        SuperMap(target,tr,dis,count,objList);
        foreach(GameObject _cube in cubes){
            if(isPointHitted(_cube.transform.position)){
               
                StartCoroutine(FrameFlashing(targetFrame,_cube.transform.position));
                return true;
            }
        }
        return false;
    }
    IEnumerator FrameFlashing(GameObject targetFrame,Vector3 cubeTr){
        
         yield return new WaitForSeconds(0.4f);
         targetFrame=Instantiate(targetFrame,cubeTr,Quaternion.identity);
        Renderer frameRenderer=targetFrame.GetComponent<Renderer>();
        for(int i=0; i<8; i++){
             frameRenderer.enabled=!frameRenderer.enabled;
            yield return new WaitForSeconds(0.1f);

        }
        
        Destroy(targetFrame);
    }
    public void AreaMove(GameObject target,Vector3 tr, float dis, int count ,List<Transform> objList,ref List<Vector3> attackRoute){ //攻撃できる経路を探してその経路を保存する
        Vector3 newpos = tr;
        List<Vector3> points = new List<Vector3>();
        List<List<Vector3>>  routes=new List<List<Vector3>>();
        points.Add(newpos);
        int pCount = 1;
        Vector3[] fourDir =GetDirections(dis);
             newpos = points[0];
             if(isAttackArea(tr,newpos,objList)){
                attackRoute.Add(newpos);
                attackRoute.Add(targetPlayer);
                return;
             }
        for (int i = 0; i < count; i++) //行動力分
        {
            for (int j = 0; j < pCount; j++)
            {
            for (int k = 0; k < 4; k++){
                     isNoCubeArea=true;
                    newpos = points[j] + fourDir[k];
                    Vector3Int cellPos = groundMap.WorldToCell(newpos);
                    TileBase tile=groundMap.GetTile(cellPos);
                    var bounds = groundMap.cellBounds;
                    foreach(Vector3 point in points){
                        float distance=Vector3.Distance(newpos,point);
                        if(distance<=0.5f) isNoCubeArea=false;
                    }
                    // タイルマップの範囲内かつ、ポイントリストに含まれていないかをチェック
                    if (isNoCubeArea&&tile!=null&&isPlayerCollision(newpos,target))
                    {   
                            points.Add(newpos);
                            if(i==0){
                                
                                List<Vector3> newRoute=new List<Vector3>{ newpos};
                                routes.Add(newRoute);
                            }else{
                                
                            foreach(List<Vector3> route in routes){
                                if(route.Contains(points[j])){
                                    
                                    List<Vector3> sublist=new List<Vector3>(route);
                                    sublist.Add(newpos);
                                    routes.Add(sublist);
                                    break;
                                }
                            }
                            }
                            
                            if(isAttackArea(tr,newpos,objList)&&isEnemyCollision(newpos)){
                                    
                                    foreach(List<Vector3> route in routes){
                                    if(route.Contains(newpos)){
                                       
                                   attackRoute=route;
                                    attackRoute.Add(targetPlayer);
                                    return;
                              }
                          }
                      }
                     
                    }
          }
        }
         pCount = points.Count;
        }
        foreach(List<Vector3> l in routes){
            Debug.Log("n");
            foreach(Vector3 v in l){
                
                Debug.Log(v);
            }
            }
    }
    public IEnumerator AreaEscape(GameObject attacker, float dis, int count){
        yield return new WaitForSeconds(1.0f);
        Vector3 newpos = attacker.transform.position;
        Vector3 destination=newpos;
        Vector3[] fourDir =GetDirections(dis);
        List<Vector3> points = new List<Vector3>();
        cubes.Clear(); 
        points.Add(newpos);
        int pCount = 1;
        for(int i=0; i<count; i++){
            for (int j = 0; j < pCount; j++)
            {
                newpos = points[j];
            for(int k=0; k<fourDir.Length; k++){
                isNoCubeArea=true;
                    newpos = points[j] + fourDir[k];
                    Vector3Int cellPos = groundMap.WorldToCell(newpos);
                    TileBase tile=groundMap.GetTile(cellPos);
                    var bounds = groundMap.cellBounds;
                    foreach(Vector3 point in points){
                        float distance=Vector3.Distance(newpos,point);
                        if(distance<=0.5f) isNoCubeArea=false;
                    }
                    // タイルマップの範囲内かつ、ポイントリストに含まれていないかをチェック
                    if (isNoCubeArea&&tile!=null&&isCharactorCollision(newpos)) //ここは更新時注意
                    {
                             points.Add(newpos); 
                    }
            }
        }
         pCount = points.Count;
    }
    
    foreach (Vector3 point in points)
        {
            
            destination=point;
          cube1=  Instantiate(cube, point, Quaternion.identity);
            cubes.Add(cube1);
        }
        if(attacker.tag=="Enemy"){
        yield return new  WaitForSeconds(0.5f);
        float l=0;
        Vector3 preposition=attacker.transform.position;
    while(l<=1.0f){
    l+=Time.deltaTime*2.0f;
     attacker.transform.position=  Vector3.Lerp(preposition,destination,l);
     yield return null;
     } 
     DestroyCube();
    }else if(attacker.tag=="Player"){
      Move move=  attacker.GetComponent<Move>();
      move.count++; move.count_F=move.count;
      move.attackMode=false;
    }
    }
    public bool isAttackArea(Vector3 tr,Vector3 point,List<Transform> objList){
        foreach(Transform obj_attack in objList){
              obj_attack.position-=tr-point; //仮の点に存在する攻撃範囲
         for(int i=1; i<=4; i++){
           
           obj_attack.RotateAround(point, Vector3.forward, 90.0f);
            if (isPointHitted(obj_attack.position))
            {
                targetPlayer=obj_attack.position;
                 obj_attack.RotateAround(point, Vector3.forward, -90.0f*i);
                 obj_attack.position+=tr-point;
                return true;
               
            }
        }
        obj_attack.position+=tr-point;
    }
    return false;
    }
    public bool isPointHitted(Vector3 point){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, 0.1f);
                        
                        foreach (Collider2D collider in colliders)
                        {
                            if (collider.CompareTag("Player"))
                            {
                               return true;
                            }
                        }
                        return false;
    }
    public bool isPlayerCollision(Vector3 point,GameObject target){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, 0.1f);
                        
                        foreach (Collider2D collider in colliders)
                        {
                            if (collider.CompareTag("Player")&&target.CompareTag("Enemy"))
                            {
                               return false;
                            }
                            if (collider.CompareTag("Enemy")&&target.CompareTag("Player"))
                            {
                               return false;
                            }
                        }
                        return true;
    }
    public bool isEnemyCollision(Vector3 point){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, 0.1f);
                        
                        foreach (Collider2D collider in colliders)
                        {
                            if (collider.CompareTag("Enemy"))
                            {
                               return false;
                            }
                        }
                        return true;
    }
    public bool isCharactorCollision(Vector3 point){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, 0.1f);
                        
                        foreach (Collider2D collider in colliders)
                        {
                            if (collider.CompareTag("Enemy")||collider.CompareTag("Player"))
                            {
                               return false;
                            }
                        }
                        return true;
    }
    private Vector3[] GetDirections(float dis)
    {
        return new Vector3[]
        {
            new Vector3(dis, 0.0f, 0.0f),
            new Vector3(-dis, 0.0f, 0.0f),
            new Vector3(0.0f, dis, 0.0f),
            new Vector3(0.0f, -dis, 0.0f)
        };
    }
    private bool IsValidTile(Vector3 newPos, GameObject target, List<Vector3> points)
    {
        if (!IsAreaClear(newPos, points)) return false;

        Vector3Int cellPos = groundMap.WorldToCell(newPos);
        TileBase tile = groundMap.GetTile(cellPos);

        return tile != null && isPlayerCollision(newPos, target);
    }
    private bool IsAreaClear(Vector3 newPos, List<Vector3> points)
    {
        foreach (Vector3 point in points)
        {
            if (Vector3.Distance(newPos, point) <= 0.5f) return false;
        }
        return true;
    }

}