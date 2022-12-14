using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statemachine : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private string Current_state;
    void Start()
    {
    animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    public void State_Machine(string New_state)
    {
      if(Current_state==New_state){return;}
      
      animator.Play(New_state);

      Current_state=New_state;
    }
}
