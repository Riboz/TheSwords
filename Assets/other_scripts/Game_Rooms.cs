using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum The_Room
{
  Room1,Room2,Room3,Room4
}
public class Game_Rooms : MonoBehaviour
{
    public Game_Generator The_generator;
        public The_Room the_room;

    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
  if(coll.gameObject.CompareTag("Player"))
  {
    if(the_room==The_Room.Room1)
    {
       The_generator.Game_level_start(1);
        Destroy(this.gameObject);
    }
    else if(the_room==The_Room.Room2)
    {
       The_generator.Game_level_start(2);
        
          Destroy(this.gameObject);
    }
    else if(the_room==The_Room.Room3)
    {
       The_generator.Game_level_start(3);
          Destroy(this.gameObject);
    }
     else if(the_room==The_Room.Room4)
    {
       
        The_generator.Game_level_start(4);
          Destroy(this.gameObject);
    }
  }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
