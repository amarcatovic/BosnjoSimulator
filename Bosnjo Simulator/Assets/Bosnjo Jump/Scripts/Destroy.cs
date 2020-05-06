using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Destroy : MonoBehaviour
{
    [SerializeField] Gordo_behaviour player;
 
    public GameObject platform;
    [SerializeField] List<GameObject> money;
    private GameObject mayPlat;
    void Start()
    {

    }
    int br = 0;
    int i = 0;
    void Update()
    {
        int random = Random.Range(0, 100);
        if(random < 90)
        {
            i = Random.Range(0, 8);
        }
        else
        {
            i = Random.Range(8, 10);
        }
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(((collision.gameObject.name.StartsWith("12")|| collision.gameObject.name.StartsWith( "hammer-2669857_1280"))) && ( collision.gameObject.name=="12(Clone)" || collision.gameObject.name == "hammer-2669857_1280(Clone)"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.name.StartsWith("Rig") )
        {
            SceneManager.LoadScene(1);
        }
        if (collision.gameObject.name.StartsWith("wall-1846969_1280"))
        {
                float rand_y = (16f );
                float rand_x = Random.Range(-1.5f, 1.5f);
                Instantiate(money[i], new Vector2(rand_x, player.transform.position.y + rand_y+0.23f), Quaternion.identity);
                collision.gameObject.transform.position = new Vector2(rand_x, player.transform.position.y + rand_y);
            
        }
        
    }
}