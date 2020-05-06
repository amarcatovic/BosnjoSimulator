using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Karta : MonoBehaviour
{
    public bool blocked=false;
    public int index=0;
    int counter = 0;
    [SerializeField] MainController main;
    public Sprite face_cardss;
    [SerializeField] Sprite face_down_sprite;
    public bool face_down;
    public bool face_up;
    public bool pogodak=false;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => Flipup());
        face_down = true;
        face_up = false;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(counter);
       
        
    }
    public  void Flipup()
    {
        if (face_down == true && face_up == false && (main.FaceUp()==1 || main.FaceUp()==0) && blocked==false)
        { 
            gameObject.GetComponent<Button>().image.sprite = face_cardss;
            face_down = false;
            face_up = true;
            
            
        }
        else if(face_down == true &&   main.FaceUp() == 2 && face_up == false )
        {
            main.ResetCards();
        }
      
       
    }
   public void addFaceCards(Sprite karta)
    {
        face_cardss = karta;
    }
    public bool GetFaceUp()
    {
        return this.face_up;
    }
    public bool GetFaceDown()
    {
        return this.face_down;
    }
    public void DisableUp()
    {
        face_down = false;
        face_up = true;
    }
    public Sprite GetFaceCard()
    {
        return face_down_sprite;
    }
    public void ResetListener()
    {
        gameObject.GetComponent<Button>().onClick.RemoveListener(() => Flipup());
    }
}
