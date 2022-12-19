using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Generator : MonoBehaviour
{
    // Start is called before the first frame updat
    public GameObject Skeleton,Floating_Skull,Sword_Curser,player;
    int level;
    public void Game_level_start(int Levels)
    {
     level=Levels;
     StartCoroutine(spawner());
    // int e göre puzzle spawnlar ve gereken yerlerde sürekli düşman spawnlanır wave halinde
    }
    //randomu karaktere göre alırken köşelerden daha fazla bir sayı çıkmasın
    public IEnumerator spawner()
    {
        for(int i=0;i<=10*level;i++)
        {
            yield return new WaitForSeconds(4f);
             Again:
            int whic_direction=(int)Random.Range(0,100);
            if(whic_direction<50){ 
             
             float x_pos=player.transform.position.x+(int)Random.Range(8,11);
               if(x_pos<7.8f)
               {
                Instantiate(Skeleton,new Vector3(x_pos,player.transform.position.y,0),Quaternion.identity);
               } 
               else {goto Again;}}

             if(whic_direction>=50)
             {
             float x_pos=player.transform.position.x+(int)Random.Range(-8,-11);
               if(x_pos>-7.8f)
               {
                Instantiate(Skeleton,new Vector3(x_pos,player.transform.position.y,0),Quaternion.identity);
               } 
               else {goto Again;}
               
            
               
               }
                
        }
    }
    public void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    
}
