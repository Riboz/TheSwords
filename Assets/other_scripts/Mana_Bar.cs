using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
  

public class Mana_Bar : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] manabar;
    public Sword sword;
    public Image Cerceve;
    bool Infinity=true;
    void Start()
    {
        StartCoroutine(Manabar_Animation());
        StartCoroutine(Cerceve_Animation());
    }
     IEnumerator Cerceve_Animation()
     {while(Infinity)
     {
        Cerceve.transform.DORotate(new Vector3(0,0,4),1);
      yield return new WaitForSeconds(1);
     Cerceve.transform.DORotate(new Vector3(0,0,-4),1f);
        yield return new WaitForSeconds(1);
     }
      
     }
    IEnumerator Manabar_Animation()
    {
        for(int i=0;i<=manabar.Length-1;i++)
        {
          this.gameObject.GetComponent<Image>().sprite=manabar[i];
          yield return new WaitForSeconds(0.1f);
          if(i==manabar.Length-1)
          {
            i=0;
          }
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Image>().fillAmount=sword.Sword_Fuel/50;
       
    }
}
