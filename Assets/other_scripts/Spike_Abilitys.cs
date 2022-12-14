using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Abilitys : MonoBehaviour
{
    public Sprite[] Spike_sprite;
    void Start()
    {
        
    }
    public void Clicked(int Clicked_Count)
    {
   this.GetComponent<SpriteRenderer>().sprite=Spike_sprite[Clicked_Count];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
