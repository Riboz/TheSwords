using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mana_sword_script : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public ParticleSystem Stone_particle,Blood_particle;
     public Transform point,enemy_blood_point;
    void Start()
    {
     rb=GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Manasword_Destroy()
    {
        rb.constraints=RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().DOFade(0,3f);
        yield return new WaitForSeconds(2.8f);
        Destroy(this.gameObject);
        yield break;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            ParticleSystem the_particle=Instantiate(Stone_particle,point.transform.position,Quaternion.identity);
            StartCoroutine(Manasword_Destroy());
                    
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy_Script>().Enemy_Health_Function(15);
             ParticleSystem the_blood=Instantiate(Blood_particle,enemy_blood_point.transform.position,Quaternion.identity);
             
        }
        if(collision.gameObject.CompareTag("Enemy2"))
        {
             collision.GetComponent<Floating_Enemy>().Enemy_Health_Function(15);
              ParticleSystem the_blood=Instantiate(Blood_particle,enemy_blood_point.transform.position,Quaternion.identity);
            
        }
    }
}
