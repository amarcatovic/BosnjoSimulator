using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class money : MonoBehaviour
{
    Text txt;
    [SerializeField] Gordo_behaviour gord;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();  
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = (gord.getMoney().ToString("f2"))+" KM";
    }
}
