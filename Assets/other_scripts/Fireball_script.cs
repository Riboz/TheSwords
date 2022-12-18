using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_script : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Follow_Fireball_Spawn=true,Just_Once=true;
    public Transform Fireball_pos;
    private Vector3 Player_last_pos;
    public GameObject Player;
   void Start()
   {

   Player=GameObject.FindGameObjectWithTag("Player");

   }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Follow_Fireball_Spawn)
        {
            Fireball_pos.transform.position=new Vector3(Fireball_pos.transform.position.x,Fireball_pos.transform.position.y+Mathf.Sin(13*Time.time)/60,0);
            this.transform.position=Vector3.MoveTowards(this.transform.position,Fireball_pos.transform.position,1.8f*Time.deltaTime);
        }
        else 
        {
            if(Just_Once)
            {
                Player_last_pos=Player.transform.position;
               
            }
             if(Player_last_pos!=null)
                {
                     Just_Once=false;

                } 
            if(!Just_Once)
            {
                this.transform.position=Vector3.MoveTowards(this.transform.position,Player_last_pos,4*Time.deltaTime);
                
            }
            
            
        }
    }
}
