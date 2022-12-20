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
        // 1.oda için
       if(level==1)
       {
        for(int i=0;i<=24*level;i++)
        {
            yield return new WaitForSeconds(5f);
            // eğer mezar olayı yapılabilirse her türlü çıkılsın 
             Again:
            int whic_direction=(int)Random.Range(0,100);

            if(whic_direction<50){ 
             float y_pos=Random.Range(1,2.5f);
             float x_pos=player.transform.position.x+(int)Random.Range(8,14);
               if(player.transform.position.x-x_pos<-7.2f)
               {
                float change=Random.Range(0,100);
                if(change>0&&change<65)
                {
                Instantiate(Skeleton,new Vector3(x_pos,player.transform.position.y,0),Quaternion.identity);
                }
               else if(change>65)
               {
                  Instantiate(Floating_Skull,new Vector3(x_pos,y_pos,0),Quaternion.identity);
               }
               } 
               else {goto Again;}}

             if(whic_direction>=50)
             {
               float y_pos=Random.Range(1,2.5f);
             float x_pos=player.transform.position.x+(int)Random.Range(-8,-14);
               if(player.transform.position.x-x_pos>7.2f)
               {
                 float change=Random.Range(0,100);
                 if(change>0&&change<65)
                {
                Instantiate(Skeleton,new Vector3(x_pos,player.transform.position.y,0),Quaternion.identity);
                }
               else if(change>65)
               {
                  Instantiate(Floating_Skull,new Vector3(x_pos,y_pos,0),Quaternion.identity);
               }
               } 
               else {goto Again;}
             }
                
        }
       }
    }
    public void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    
}
