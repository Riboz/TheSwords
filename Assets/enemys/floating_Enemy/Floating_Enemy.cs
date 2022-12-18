using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating_Enemy : MonoBehaviour
{
    // Start is called before the first frame update,
    Statemachine st;
    Animator anim;
    Rigidbody2D rb;
    [SerializeField] GameObject fireball,Player;
     Vector3 The_Point;
     public Transform fireball_pos;
    public float timer,cooldown,Enemy_Health=15,Enemy_speed;
    public bool Is_Death,Run_Enemy,Hurt,place_found,fireball_instantitated;
    const string Enemy_idle="enemy_idle";
    const string Enemy_run="enemy_run";
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
    

public void FixedUpdate()
{
   Floating_Movement();
   Fireball();
}
IEnumerator fireballthrow()
{ 
GameObject fireball_1= Instantiate(fireball,fireball_pos.transform.position,Quaternion.identity);
fireball_1.GetComponent<Fireball_script>().Fireball_pos=fireball_pos;
yield return new WaitForSeconds(2.5f);
this.GetComponent<SpriteRenderer>().color=Color.yellow;
yield return new WaitForSeconds(0.5f);
    this.GetComponent<SpriteRenderer>().color=Color.white;
    fireball_1.GetComponent<Fireball_script>().Follow_Fireball_Spawn=false;
    cooldown=0;
    fireball_instantitated=false;
    yield break;
}
public void Fireball()
{
   cooldown+=Time.deltaTime;
    if(cooldown>3&&!fireball_instantitated)
    {
        
            fireball_instantitated=true;
          StartCoroutine(fireballthrow());
       //firbal.GetComponent< kodun adı >().throw=true
        
    }
        
        
      
    
}
 public void Floating_Movement()
 {
   if(!Is_Death)
    {


   timer+=Time.deltaTime;

   if(!place_found) The_Point=new Vector3((int)Random.Range(-10,10),Random.Range(1,2.5f),0);
   
    if(Vector2.Distance(Player.transform.position,The_Point)>0.25f &&Vector2.Distance(Player.transform.position,The_Point)<5f &&Vector2.Distance(this.transform.position,The_Point)>1f)

    {
        place_found=true;
 
    }

    if(place_found)
    {
         Run_Enemy=true;
         this.transform.position=Vector2.MoveTowards(this.transform.position,The_Point,0.5f*Time.deltaTime);
           
    if(this.transform.position==The_Point)
    {
        
        place_found=false;
    }
    }
    }
    // iskelet sürekli olarak  karakterin menzili içerisinde belirli bir uzaklıkda bir nokta seçer oraya gider giderken ateş edebilir ama yavaş ateş eder 

 }








    // hurt and death 
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
  st.State_Machine(Enemy_idle);
  yield break;
 }


 private IEnumerator The_Death()
 {
  
  st.State_Machine(Enemy_death);
  yield return new WaitForSeconds(0.6f);
  
  Destroy(this.gameObject);
  yield break;
  
  
 }
}

