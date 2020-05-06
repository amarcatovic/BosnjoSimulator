using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
   [SerializeField] List<GameObject> base1;
    [SerializeField] GameObject cigla;
    [SerializeField] GameObject cekic;
 
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = shoot(i);
        StartCoroutine(coroutine);
    }
    
    // Update is called once per frame
    private IEnumerator coroutine;
   
    IEnumerator shoot(int i)
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            cigla.SetActive(true);
            cekic.SetActive(true);
            Instantiate(base1[i % 2]);
            base1[i % 2].transform.position = new Vector3(Random.Range(-2f, 2f), gameObject.transform.position.y, 0);
            base1[i % 2].transform.Rotate(0, 0, 45*Time.deltaTime);
            base1[i % 2].GetComponent<Rigidbody2D>().velocity =new  Vector3(0f,0f,87*Time.deltaTime);
            //yield return new WaitForSeconds(5f);
            //Destroy(base1[i % 2]);
            i++;
        }
        
    }
}
