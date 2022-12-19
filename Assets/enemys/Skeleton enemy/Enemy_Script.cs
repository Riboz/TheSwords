using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    // Start is called before the first frame update
    //mezardan çıksın mezar kırılsın ve içinden bizim skelet çıksın ardından ana karaktere doğru koşsun
    //skeletonun raycasti ana karaktere çarpınca dursun ve saldırsın
    
    Statemachine st;
    Animator anim;
    Rigidbody2D rb;
     GameObject Player;
      public int level;

     [SerializeField] public bool Run_Enemy=true,Attack_enemy,Wait_enemy,Only_One_per_time,Looking_right,Hurt,first_Slow=false,Is_Death=false;

    public LayerMask Which_thing;
     //skeletonun olması gereken yükseklik
     private float final_height_y=-0.8f,Enemy_speed=0.5f,Enemy_power,Enemy_Health=20;
    const string Enemy_idle="skeleton_idle";
    const string Enemy_run="enemy_run";
    const string Enemy_attack="skeleton_attack"; 
    const string Enemy_hurt="enemy_hurt";
    const string Enemy_death="enemy_death";
    void Start()
    {
        
        rb=GetComponent<Rigidbody2D>();
        st=GetComponent<Statemachine>();
        anim=GetComponent<Animator>();
        Player=GameObject.FindGameObjectWithTag("Player");
        
        // bir enumun içinde mezarlık doğsun yavaşça yukarı çıksın mezarlık kırılsın ve içinden skeleton çıksın çıktıkdan 0.5 saniye sonra hareket etsin
        //
    }
    
   float first_Slow_timer;
    public void FixedUpdate()
    {
      if(!Is_Death)
      {
        Movement_To_Player();
        
      if(first_Slow)
      {
        first_Slow_timer+=Time.deltaTime;
        if(first_Slow_timer>=8)
        {
         first_Slow_timer=0;
         first_Slow=false;
        }
      }
      }
        
    }
     public void Movement_To_Player()
     {

      if( Run_Enemy && !Attack_enemy && !Wait_enemy &&!Hurt )
      {
        st.State_Machine(Enemy_run);

        this.transform.position=Vector2.MoveTowards(this.transform.position,new Vector2(Player.transform.position.x,final_height_y),Enemy_speed*Time.deltaTime);
      }

      if(this.transform.position.x-Player.transform.position.x<0 && !Hurt)
      {
      
        //saga bakiyor
        Looking_right=true;
        Debug.DrawRay(this.transform.position,Vector2.right,Color.red,0.5f);
        RaycastHit2D Right_Raycast=Physics2D.Raycast(this.transform.position,Vector2.right,0.38f,Which_thing);
        if(Right_Raycast.collider!=null)
        {
         if(Right_Raycast.collider.CompareTag("Player"))
          {
           Run_Enemy=false;
           Attack_enemy=true;
           if(Attack_enemy==true && Only_One_per_time && !Wait_enemy)
           { 
            StartCoroutine(Attack_Funciton());
            StopCoroutine(Wait_Function());
            Only_One_per_time=false;
           }
          }
          
         

        }
        else
          { 
            Attack_enemy=false;
            StopCoroutine(Attack_Funciton());
          }

       
        GetComponent<SpriteRenderer>().flipX=false;
      
      
      }
      // sola bakıyor
      else if(this.transform.position.x-Player.transform.position.x>0 && !Hurt)
      {
      Looking_right=false;
       Debug.DrawRay(this.transform.position,Vector2.left,Color.red,0.5f);
       RaycastHit2D Left_Raycast=Physics2D.Raycast(this.transform.position,Vector2.left,0.38f,Which_thing);
       
        if(Left_Raycast.collider!=null)
        {
         if(Left_Raycast.collider.CompareTag("Player"))
          {
           Run_Enemy=false;
           Attack_enemy=true;
           if(Attack_enemy==true && Only_One_per_time && !Wait_enemy && !Hurt)
           { 
            StartCoroutine(Attack_Funciton());
            StopCoroutine(Wait_Function());
            Only_One_per_time=false;
           }
          }
        
         

        }
          else
          { 
            Attack_enemy=false;
            StopCoroutine(Attack_Funciton());
          }
            GetComponent<SpriteRenderer>().flipX=true;
        
      }
     }
    // Update is called once per frame
    private IEnumerator Attack_Funciton()
    {
      st.State_Machine(Enemy_idle);
      yield return new WaitForSeconds(0.2f);
      st.State_Machine(Enemy_attack);
      yield return new WaitForSeconds(0.7f);
      if(Looking_right)
      {
      Collider2D[] Attack_Collider=Physics2D.OverlapBoxAll(this.transform.position+new Vector3(0.29f,0,0),new Vector2(0.26f,0.26f),0f);
      foreach(Collider2D Player in Attack_Collider)
       {
        Player_Movement Player_Hurt_Func=Player.GetComponent<Player_Movement>();
        if(Player_Hurt_Func!=null)
        {
          Player_Hurt_Func.Player_Health_Show(Enemy_power);
          Debug.Log("Vuruyor");
          Attack_Collider[0]=null;
        }

       }
       yield return new WaitForSeconds(0.3f);
        Only_One_per_time=true;
         StartCoroutine(Wait_Function());
        yield break;
      }
 else if(!Looking_right)
    {
      Collider2D[] Attack_Collider=Physics2D.OverlapBoxAll(this.transform.position+new Vector3(-0.29f,0,0),new Vector2(0.26f,0.26f),0f);
      foreach(Collider2D Player in Attack_Collider)
       {
        Player_Movement Player_Hurt_Func=Player.GetComponent<Player_Movement>();
        if(Player_Hurt_Func!=null)
        {
          Player_Hurt_Func.Player_Health_Show(Enemy_power);
          Debug.Log("Vuruyor");
          Attack_Collider[0]=null;
        }
       }
         yield return new WaitForSeconds(0.3f);
        Only_One_per_time=true;
             StartCoroutine(Wait_Function());
        yield break;
    }
   
    }
  
    private IEnumerator Wait_Function()
{
 st.State_Machine(Enemy_idle);
  yield return new WaitForSeconds(0.4f);
   Wait_enemy=false;
   Run_Enemy=true;
   
   yield break;
}

 public void Enemy_Health_Function(float Damage)
 {
  
  if(Enemy_Health<=0)
  {
    Is_Death=true;

    // düşman ölme animasyonu ve yer altına çekilme enumu 
    this.transform.position=this.transform.position+new Vector3(0,-0.01f,0);
    StartCoroutine(The_Death());
  }
  else
  {
   Enemy_Health-=Damage;
if(Run_Enemy)StartCoroutine(dontwalk());
 StartCoroutine(Health_taken());
  }
  
 }
 IEnumerator dontwalk()
 {
  Run_Enemy=false;
  
  yield return new WaitForSeconds(0.5f);
 Run_Enemy=true;
  yield break;
 }
 IEnumerator Health_taken()
 {
  Hurt=true;
  
  st.State_Machine(Enemy_hurt);
  yield return new WaitForSeconds(0.2f);
  Hurt=false;
  
  yield break;
 }
 public void Enemy_Spike_Effect()
 {
   
  StartCoroutine(Enemy_Spike_effect_Coroutine());
 }
 private IEnumerator Enemy_Spike_effect_Coroutine ()
 {
   Enemy_speed=0.25f;
  yield return new WaitForSeconds(2f);
  Enemy_speed=0.5f;
 
  yield break;
 }
 private IEnumerator The_Death()
 {
  float i=0;
  
  
  st.State_Machine(Enemy_death);
  yield return new WaitForSeconds(6);
  while(i<8)
  {
    i+=1;
    yield return new WaitForSeconds(0.2f);
    this.transform.position=Vector2.MoveTowards(this.transform.position,new Vector2(this.transform.position.x,this.transform.position.y-0.2f),0.02f);
    
  }
  Destroy(this.gameObject);
  yield break;
  
  
 }
}

 