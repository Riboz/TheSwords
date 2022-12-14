using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coll_sword : MonoBehaviour
{
    // Start is called before the first frame update
    // mousepos ve swordun x ve y sine göre
    public Sword sword_scr;
    public ParticleSystem Stone_particle,Blood_particle;
    
    public bool spawned=true;
    public static bool isin;
    Vector3 last_pos;
    int howmuch=0;
    float timer;
    bool a=false;
    public GameObject the_spike,spikes;
    public Transform point;
    public Transform[] pos;
    Rigidbody2D rb;
    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Sword.its_attack)
        {
            this.transform.Rotate(0, 0,sword_scr.angle_of_attack);
            a=true;
        }
       

         if(Input.GetMouseButtonDown(1)&&isin)
       {
        howmuch+=1;
        spikes.GetComponent<Spike_Abilitys>().Clicked(howmuch);
       
       ParticleSystem the_particle=Instantiate(Stone_particle,point.transform.position,Quaternion.identity);
        
        if(howmuch>=4)
        {
            spawned=true;
            howmuch=0;
            sword_scr.Right_click=false;
            sword_scr.Go_back=true;
             isin=false;
        }
       }

        rb.rotation=sword_scr.angle-45f;
         if(!sword_scr.Right_click && sword_scr.Wait_behind)
            {
               rb.rotation=180f;
            }
    }
     void OnTriggerEnter2D(Collider2D coll)
    {
        if(!Sword.its_attack){
     if(coll.CompareTag("ground"))
     {
        isin=true;
        ParticleSystem the_particle=Instantiate(Stone_particle,point.transform.position,Quaternion.identity);

        Destroy(the_particle,1f);

        sword_scr.Right_click=false;
        sword_scr.Wait_right_click=false;
        sword_scr.right_click_position=this.transform.position;
        last_pos=this.transform.position;
        
        Debug.Log("a");
         if(spawned)
        {
            spikes= Instantiate(the_spike,new Vector3(point.transform.position.x,-0.9f,0),Quaternion.identity)as GameObject;
            
            spawned=false;
        }
      
     }
        }
    }
    void OnTriggerStay2D(Collider2D coll)
    {
       
     if(!Sword.its_attack){
     if(coll.CompareTag("ground"))
     {
        sword_scr.Right_click=false;
      
        sword_scr.Wait_right_click=false;
         
        Debug.Log("a");
      
     }
     
     }
     else if(coll.CompareTag("Enemy"))
     {
        
      if(Sword.its_attack)
      {
       timer+=Time.deltaTime;
        if(timer>0.1f)
        {
         Vector3 Bloodpos=pos[Random.Range(0,2)].position;
        ParticleSystem Blood=Instantiate(Blood_particle,Bloodpos,Quaternion.identity);
        Blood.GetComponent<Renderer>().sortingOrder=Random.Range(-2,3);
        Cinemachine_Shake.Instance.Shake_Camera(0.4f,0.05f);
        coll.gameObject.GetComponent<Enemy_Script>().Enemy_Health_Function(1);
        Debug.Log("AAA");
        timer=0;
        }
       
      }
      
     }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if(!Sword.its_attack){
         if(coll.CompareTag("ground"))
     {
        spawned=true;
        isin=false;
        howmuch=0;
     }
        }
    }
    
}
