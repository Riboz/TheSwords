using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour 
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Statemachine st;
    [SerializeField] private bool Just_Once=true,hurting;
    [SerializeField] private float speed,Player_Health;

    // Animation States
    const string Player_idle="player_idle";
    const string Player_run="player_run";
    const string Player_attack="player_attack"; 
    const string Player_hurt="player_hurt";

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        st=GetComponent<Statemachine>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement_Input();
    }
    private void Movement_Input()
    {
        float Horizontal_Axis = Input.GetAxis("Horizontal");
        rb.velocity=new Vector2(Horizontal_Axis*speed,rb.velocity.y);
        if(Horizontal_Axis<0)
        {
             if(!hurting)st.State_Machine(Player_run);
          //sağa baktır run
          this.transform.localScale=new Vector2(-1,1);
          if(Just_Once)
          {

           StartCoroutine(Input_Speed_Control());
           Just_Once=false;
          }
 
          //ienumla speedi yavaşça arttır
        }
        else if(Horizontal_Axis>0)
        {
             this.transform.localScale=new Vector2(1,1);
            if(!hurting)  st.State_Machine(Player_run);
          //sola baktır run
           //ienumla speedi yavaşça arttır
          if(Just_Once)
          {
           StartCoroutine(Input_Speed_Control());
           Just_Once=false;
          }
        }

        else
        {
             if(!hurting)st.State_Machine(Player_idle);
            //idle
             //speedi defaulta götür
             StopCoroutine(Input_Speed_Control());
             speed=0.9f;
             Just_Once=true;
        }
    }
    private IEnumerator Input_Speed_Control()
    {
        for(int i=0;i<=3;i++)
        {
            yield return new WaitForSeconds(0.15f);
            if(speed<1.3f)
            {
                  
                   speed+=0.1f;
            }
           else
           {
            break;
           }
            
        }
        yield break;
    }
   public void Player_Health_Show(float Hurt)
   {
    
    //ienumeratorle
    StartCoroutine(Player_Hurting());
   
    Player_Health-=Hurt;
   }
   IEnumerator Player_Hurting()
   {
    hurting=true;
    st.State_Machine(Player_hurt);
    yield return new WaitForSeconds(0.15f);
    hurting=false;
    yield break;
   }
}
