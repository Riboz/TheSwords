using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
   
   
    public Transform Sword_base_position;
    Vector2 lookdir;
    public static bool its_attack=false;
    public Vector3 right_click_position;
    public Camera mainCamera;
    public float angle,Sword_Fuel=80, angle_of_attack , timera,timerb ;
    public float floatspeed;
    [SerializeField] public bool Damage=false,Right_click=false,Go_back=false,Wait_behind=false,Wait_right_click=false,Only_One_Attack,only_one_refill;
    Rigidbody2D rb;
   // sağ tıklanan yerin noktasını alıcak 
   //ve oraya doğru gidicek eğer bir yer yoksa sword base positiona gidilecek sonra pozisyonda durucak 
   // c ye basılınca kendi yerine gidicek
   // collider duvara çarpınca this transfrom rightclik olsun
    void Start()
    {
    rb=GetComponent<Rigidbody2D>();    
   
    }
    
    // Update is called once per frame
    void Update()
    {
       Clicks();
        Go_back_Sword();
    }
    void Go_back_Sword()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Go_back=true;
            Right_click=false;
            Wait_behind=true;
            Wait_right_click=false;
            its_attack=false;
            
        }
        if(Go_back)
        {
              lookdir =Sword_base_position.position-this.transform.position;
              angle=Mathf.Atan2(lookdir.y,lookdir.x)*Mathf.Rad2Deg-45f;
             if(!its_attack){ rb.DORotate(angle,0.025f);}
              this.transform.position=Vector3.MoveTowards(this.transform.position,Sword_base_position.position,3*Time.deltaTime);
              if(this.transform.position==Sword_base_position.position)
              {
                Go_back=false;
                Wait_behind=true;
              rb.DORotate(-135f,0.1f);
              }

        }
         if(Wait_behind)
                {
                    this.transform.position=Vector3.MoveTowards(this.transform.position,Sword_base_position.position+new Vector3(0,Mathf.Sin(3*Time.time)/10,0),3*Time.deltaTime);
                    
                }
    }
    void Clicks()
    {
        if(Input.GetMouseButton(0)&&Sword_Fuel>0&&!coll_sword.isin &&!Wait_behind)
        {
            its_attack=true;
            only_one_refill=true;
          
            
             Vector3 Mousepos=mainCamera.ScreenToWorldPoint(Input.mousePosition);
             Mousepos=new Vector3(Mousepos.x,Mousepos.y,0);
             angle_of_attack=1500*Time.deltaTime;
          this.transform.Rotate(0, 0,angle_of_attack);
          this.transform.position=Vector3.MoveTowards(this.transform.position,Mousepos,floatspeed*Time.deltaTime);
          right_click_position=this.transform.position;
            
             
          timerb+=Time.deltaTime;
            Debug.Log("saldiri acik");
            floatspeed=5;
            if(Only_One_Attack && timerb>1f)
            {
              timerb=0;
             StartCoroutine(Attack_Stamina());
             Only_One_Attack=false;
        
              StopCoroutine(Attack_Stamina_refill());
            }
            else return;
            //kılıcı rotate et 
        }
        else
        {
             timera +=Time.deltaTime;
           if(Wait_right_click &&!Right_click){ rb.DORotate(-135f,0.05f);}
          
            floatspeed=2.5f;
            StopCoroutine(Attack_Stamina());
            its_attack=false;
             Debug.Log("saldiri kapali");
            Only_One_Attack=true;
         if(only_one_refill && timera>=1f)
         {
          timera=0;
            StartCoroutine(Attack_Stamina_refill());
            only_one_refill=false;
         }
         
        }

    if(!Right_click&&!Go_back&&!its_attack)
        {
        Vector3 Mousepos=mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
 
        if(Input.GetMouseButtonDown(1))
          {
            Wait_behind=false;
            right_click_position=Mousepos;
            right_click_position=new Vector3(right_click_position.x,right_click_position.y,0);
            Right_click=true;
          }
          
        }
        if(Right_click&&!its_attack)
          {
            Wait_right_click=false;
            lookdir =right_click_position-this.transform.position;
            angle=Mathf.Atan2(lookdir.y,lookdir.x)*Mathf.Rad2Deg-45f;
            if(!its_attack){rb.DORotate((int)angle,0.025f);}
            this.transform.position=Vector3.MoveTowards(this.transform.position,right_click_position,floatspeed*Time.deltaTime);
            if(this.transform.position==right_click_position)
            {
                Right_click=false;
               Wait_right_click=true;
               

            }
          }
          if(Wait_right_click &&!Right_click&&!its_attack)
          {
            this.transform.position=new Vector3(this.transform.position.x,right_click_position.y+Mathf.Sin(3*Time.time)/10,0);
          }
           
    }
   public IEnumerator Attack_Stamina()
   {
    while(its_attack)
    {
        Sword_Fuel-=0.06f;
        // stamina barı azalt
        yield return new WaitForSeconds(0.01f);
        if(Sword_Fuel<=0)
        {
            its_attack=false;
            yield break;
        }
    }
    
   }
   public IEnumerator Attack_Stamina_refill()
   {
    Wait_right_click=false;
    while(!its_attack && Sword_Fuel<80)
    {
        Sword_Fuel+=0.015f;
        // stamina barı azalt  
       
        yield return new WaitForSeconds(0.005f);
        if(Sword_Fuel>=80)
        {
           Sword_Fuel=80;
           yield break;
        }
    }
    
   }
    
}
