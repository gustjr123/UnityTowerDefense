using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    
    public GameObject Enemybug;
    public int Creature_Damage = 10;    
    public float Speed;
    // 
    public Transform[] waypoints;
    int curWaypointIndex = 0;
    public float previous_Speed;
    public Animator anim;
    public EnemyHp Enemy_Hp;
    public Transform target;
    public GameObject EnemyTarget;
    public GameObject CoinPrefab;
        
    private bool isCoin = true;

    void Start()
    {            
        anim = GetComponent<Animator>();
        Enemy_Hp = Enemybug.GetComponent<EnemyHp>();
        previous_Speed = Speed;        
    }

    // Attack

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Castle")
        {
            Speed = 0;
            EnemyTarget = other.gameObject;
            target = other.gameObject.transform;
            Vector3 targetPosition = new Vector3(EnemyTarget.transform.position.x, transform.position.y, EnemyTarget.transform.position.z);            
            transform.LookAt(targetPosition);
            anim.SetBool("RUN", false);
            anim.SetBool("Attack", true);
        }
    }
    void GetDamage ()
    {   
        if (EnemyTarget) {
            Debug.Log("GetDamage " + EnemyTarget.name);
            EnemyTarget.GetComponent<TowerHP>().Dmg_2(Creature_Damage);
        }     
    }

    


    void Update () 
	{
        // MOVING
        #region 이동 이벤트
        if (curWaypointIndex < waypoints.Length){
            
            if (waypoints[curWaypointIndex] != null)
            {
                transform.position = Vector3.MoveTowards(transform.position,waypoints[curWaypointIndex].position,Time.deltaTime*Speed);
                // Debug.Log("WayPoint" + waypoints[curWaypointIndex].name);
            
                if (!EnemyTarget)
                {
                    transform.LookAt(waypoints[curWaypointIndex].position);
                }
        
                if(Vector3.Distance(transform.position,waypoints[curWaypointIndex].position) < 0.5f)
                {
                    curWaypointIndex++;
                }    
            }
	    }          
        else
        {
            anim.SetBool("Victory", true);  // Victory
        }
        #endregion

        // DEATH
        #region 몬스터 죽을 때 이벤트
        // Death time 
        if (Enemy_Hp.EnemyHP <= 0)
        {
            Speed = 0;
            Destroy(gameObject, 0.5f);
            anim.SetBool("Death", true);

            // Coin을 한번만 생성
            if (isCoin){
                Vector3 v = gameObject.transform.position;
                v.y += (float)0.5;
                Instantiate(CoinPrefab, v, gameObject.transform.rotation); 
                isCoin = false;
            }
        }
        #endregion

        // Attack to Run
        #region 성 파괴시 이벤트
        // 최종 목표 파괴시 idle 상태로 전환 및 더이상 이동 X
        if (EnemyTarget) {
            if (EnemyTarget.CompareTag("Castle_Destroyed")) // get it from BuildingHp
            {
                anim.SetBool("Attack", false);
                anim.SetBool("Idle", true);
                Speed = 0;
                EnemyTarget = null; 
                // 성 파괴시 이동을 막기위해 다음 이동을 위한 index를 최종 값으로 변환시킴 # 62번째 줄에서 if문을 통해 이동 방지
                curWaypointIndex = waypoints.Length;
            }
        }
        #endregion
    }


    #region NotUse
    // public Transform shootElement;
    // public GameObject bullet;
    // Attack
    //void Shooting ()
    //{
        // if (EnemyTarget)
        // {           
            // GameObject с = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
            // с.GetComponent<EnemyBullet>().target = target;
            // с.GetComponent<EnemyBullet>().twr = this;
        // }  

    // }
        
    #endregion 
   
}

