using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hello : MonoBehaviour
{
    public GameObject bosnjo1;
    private Animator b1;

    public float period = 0f;
    private int random = 0;
    bool okreniStranu = false;

    // Start is called before the first frame update
    void Start()
    {
        b1 = bosnjo1.GetComponent<Animator>();  
        b1.SetTrigger("PlazaOdmor");
   
       
    }



    /*private void HandleBosnjoTrci()
    {
        Vector3 bosnjoTrci = new Vector3(bosnjo4.transform.position.x, bosnjo4.transform.position.y, -2);
        if (bosnjo4.transform.position.x > 2.5)
        {
            okreniStranu = true;
            Vector3 temp = bosnjo4.transform.localScale;
            temp.x = 0.1f;
            bosnjo4.transform.localScale = temp;
        }
        else if (bosnjo4.transform.position.x < -2.5)
        {
            okreniStranu = false;
            Vector3 temp = bosnjo4.transform.localScale;
            temp.x = -0.1f;
            bosnjo4.transform.localScale = temp;
        }
        if (!okreniStranu)
            bosnjoTrci.x += 0.01f;
        else
            bosnjoTrci.x -= 0.01f;

        bosnjo4.transform.position = bosnjoTrci;
    }*/
}
