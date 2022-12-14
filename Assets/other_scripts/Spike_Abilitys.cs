using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Abilitys : MonoBehaviour
{
    public Sprite[] Spike_sprite;
    public float timer,timer2;
        public ParticleSystem Stone_particle;
    int Spike_Health;
    void Start()
    {
        
    }
    public void Clicked(int Clicked_Count)
    {
    Spike_Health+=1;
   this.GetComponent<SpriteRenderer>().sprite=Spike_sprite[Clicked_Count];
    }
    // Update is called once per frame
    
     void OnTriggerEnter2D(Collider2D Coll)
    {
        if(Coll.gameObject.CompareTag("Enemy"))
        {
          Coll.gameObject.GetComponent<Enemy_Script>().Enemy_Spike_Effect();
        }
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        if(Coll.gameObject.CompareTag("Enemy"))
        {
            
          timer+=Time.deltaTime;
          timer2+=Time.deltaTime;
          if(timer2>0.75f)
          {
                    Instantiate(Stone_particle,this.transform.position,Quaternion.identity);
                    timer2=0;
          }
      
          if(timer>0.5f)
          {
            timer=0;
             Spike_Health-=1;
             
            if(Spike_Health>=0) this.GetComponent<SpriteRenderer>().sprite=Spike_sprite[Spike_Health];
             if(Spike_Health<=0)
             {
                Destroy(this.gameObject);
             }
              Coll.gameObject.GetComponent<Enemy_Script>().Enemy_Health_Function(1);
             
          }
            
            
        }

    }
 
    

   
}
