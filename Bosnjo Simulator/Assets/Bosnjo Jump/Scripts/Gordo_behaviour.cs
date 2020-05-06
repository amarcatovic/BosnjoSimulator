using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
public class Gordo_behaviour : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Slider sliderE;
    int max_health = 100;
    int current_health;
    public int jump_counter = 0;
    Animator animator;
    float min = 1000;
    int poz = 0;
    [SerializeField] GameObject up_arr;
    [SerializeField] List<GameObject> trava;
    static public float money = 0;
    [SerializeField] GameObject FireworksAll;
    private AudioSource audio;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("12") || collision.gameObject.name.StartsWith("hammer-2669857_1280"))
        {
            slider.value = slider.value - 25;
            Explode();
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("10km_fe_lice_velika"))
        {          
            money = money + 10;
            Destroy(collision.gameObject);
            audio.Play();
        }
        else if (collision.gameObject.name.StartsWith("20km_fe_lice_velika"))
        {           
            money = money + 20;
            Destroy(collision.gameObject);
            audio.Play();
        }
        else if (collision.gameObject.name.StartsWith("50maraka-696x338"))
        {          
            money = money + 50;
            Destroy(collision.gameObject);
            audio.Play();
        }
        else if (collision.gameObject.name.StartsWith("1_km_lice"))
        {          
            money = money + 1;
            Destroy(collision.gameObject);
            audio.Play();
        }
        else if (collision.gameObject.name.StartsWith("2_km_nalicje"))
        {
            money = money + 2;
            Destroy(collision.gameObject);
            audio.Play();
        }
        else if (collision.gameObject.name.StartsWith("5_km_lice"))
        {
            money = money + 5;
            Destroy(collision.gameObject);
            audio.Play();
        }
        else if (collision.gameObject.name.StartsWith("10kf"))
        {
            money = money + 0.1f;
            Destroy(collision.gameObject);
            audio.Play();
        }
        else if (collision.gameObject.name.StartsWith("20_kf_lice"))
        {
            money = money + 0.2f;
            Destroy(collision.gameObject);
            audio.Play();
        }
        else if (collision.gameObject.name.StartsWith("50_kf_lice"))
        {
            money = money + 0.5f;
            Destroy(collision.gameObject);
            audio.Play();
        }
        
    }

    // Start is called before the first frame update
    public void Start()
    {
        slider.value = max_health;
        sliderE.value = max_health;
        animator = gameObject.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    Transform trans;
    bool desno = true, lijevo = true;
    // Update is called once per frame
    Rigidbody2D rigBody;

   
    [SerializeField] TextMeshProUGUI topScore;
    void Update()
    {
        if(gameObject.GetComponent<Rigidbody2D>().velocity.y == 0 && jump_counter != 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 450);
        }

        topScore.text = (money.ToString("f1"))+" KM";
        Play();
        if((gameObject.GetComponent<Rigidbody2D>().velocity.y<=-10f) || (slider.value==0))
        {
            SceneManager.LoadScene("End Game");
        }
      

    }
    IEnumerator counter()
    {
        int b = 0;
        
            while (b<=21)
            {
                sliderE.value = sliderE.value + 5;
                b++;
                yield return new WaitForSeconds(1f);
            }
            jump_counter = 0;
            up_arr.SetActive(true);
        
    }
    
    public void Play()
    {

        if(CrossPlatformInputManager.GetAxis("Horizontal")==1 && desno==true)
       // if( Input.GetKeyDown(KeyCode.RightArrow)&&  lijevo==false)
        {
            if(jump_counter == 0)
            {
                jump_counter++;
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 450);
            }
            trans =gameObject.transform.GetChild(0).gameObject.GetComponent<Transform>();
            trans.localScale = new Vector3(-1f, trans.localScale.y, trans.localScale.z);
            gameObject.transform.GetChild(0).gameObject.GetComponent<Transform>().localScale = trans.localScale;
            gameObject.GetComponent<PolygonCollider2D>().offset = new Vector2(2.63f, 0f);
            animator.SetBool("Run_01", true);
            desno = false;
            lijevo = true;
        Pomjeri();
            
        }
         else if (CrossPlatformInputManager.GetAxis("Horizontal")==-1 && lijevo==true)
       // else if (Input.GetKeyDown(KeyCode.LeftArrow) && lijevo == true)
        {
            if (jump_counter == 0)
            {
                jump_counter++;
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 450);
            }
            trans = gameObject.transform.GetChild(0).gameObject.GetComponent<Transform>();
            trans.localScale = new Vector3(1f, trans.localScale.y, trans.localScale.z);
            gameObject.transform.GetChild(0).gameObject.GetComponent<Transform>().localScale = trans.localScale;
            gameObject.GetComponent<PolygonCollider2D>().offset = new Vector2(-0.4f, 0f);
            animator.SetBool("Run_01", true);
            lijevo = false;
            desno = true;
            Pomjeri();
           
        }
        if(lijevo==false || desno==false)
        {
            Pomjeri();
        }
        else if(CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Pomjeri();
        }
       

    }
    float speed = 1f;
    public void Pomjeri()
    {

         var moveInput = CrossPlatformInputManager.GetAxis("Horizontal")*Time.deltaTime ;
        //var moveInput = Input.GetAxis("Horizontal")*Time.deltaTime ;
        var pozicijax = transform.position.x+moveInput;
        
        transform.position = new Vector2(pozicijax,transform.position.y);
        //rigBody.velocity = new Vector2(moveInput * speed, rigBody.velocity.y);
         if (CrossPlatformInputManager.GetButtonDown("Jump") && desno == true /*&& jump_counter==0*/)
        // if (Input.GetKeyDown (KeyCode.Space) && desno == true)
        {
            sliderE.value = 0;
            StartCoroutine(counter());
            //animator.SetTrigger("Is_jump");
            jump_counter++;
            up_arr.SetActive(false);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveInput * speed- 1f, 8f);

            animator.SetBool("Run_01", true);

        }
        else if (CrossPlatformInputManager.GetButtonDown("Jump") && lijevo == true /*&& jump_counter==0*/)
       // if (Input.GetKeyDown(KeyCode.Space) && lijevo == true)
        {
            sliderE.value = 0;
            StartCoroutine(counter());
            //animator.SetTrigger("Is_jump");
            jump_counter++;
            up_arr.SetActive(false);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveInput * speed + 1f, 8f);

            animator.SetBool("Run_01", true);

        }
        else if(CrossPlatformInputManager.GetButtonDown("Jump") /*&& jump_counter == 0*/ && lijevo==false && desno==false)
        {
            sliderE.value = 0;
            StartCoroutine(counter());
            //animator.SetTrigger("Is_jump");
            jump_counter++;
            up_arr.SetActive(false);

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveInput * speed, 8f);

            animator.SetBool("Run_01", true);

        }
        
    }
    public double getMoney()
    {
        return money;
    }
    public void playAgain()
    {
        Gameplay gp = FindObjectOfType<Gameplay>();
        SaveData sd = gp.GetSaveData();

        string text = "";
        if(money == 0)
        {
            gp.SetNewActivity(sd.playerName + " je završio posao, ali nije ništa zaradio");
        }
        else
        {
            gp.KreditManager(ref money, ref text);
            gp.SetNewActivity(sd.playerName + " je završio posao na skeli i zaradio " + money.ToString("#.##") + "KM. " + text);
        }
        sd.playerMoney += (float)money;
        sd.playerHealth -= 0.1f;
        sd.playerHunger -= 0.1f;

        FindObjectOfType<GameSaveLogic>().Save();

        SceneManager.LoadScene("MainGame");
        money = 0;
    }
    void Explode()
    {
        GameObject firework = Instantiate(FireworksAll, gameObject.transform.position, Quaternion.identity);
        firework.GetComponent<ParticleSystem>().Play();
    }
}
