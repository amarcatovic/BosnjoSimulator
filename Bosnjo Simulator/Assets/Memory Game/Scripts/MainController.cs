using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MainController : MonoBehaviour
{
    public static int money=0;
    public static int attemps=0;
    [SerializeField] TextMeshProUGUI atmp;
    [SerializeField] List<GameObject> karta_list;
    [SerializeField] List<Karta> kart;
    public static int size;
    [SerializeField] List<Sprite> face_cards;

    int kattemps = 0;
    bool hasEnded = false;

    Gameplay gp;
    SaveData sd;

    public List<int> facultiesYears;
    public List<string> facultiesNames;

    void Start ()
    {
        kattemps = 0;
        attemps = 0;
        Screen.orientation = ScreenOrientation.Landscape;
        gp = FindObjectOfType<Gameplay>();
        sd = gp.GetSaveData();

        size = kart.Count;
        int index1 = 0;
        int brojac = 0;
        
       
        for(int i=0;i<face_cards.Count;i++)
        {
            for (int j=brojac;j<brojac+2;j++)
            {
              
                kart[j].addFaceCards(face_cards[i]);
                kart[j].index = index1;
            }
            brojac+=2;
            index1++;
        }
        for (int i = 0; i < kart.Count; i++)
        {
            
            int randomIndex = Random.Range(i, kart.Count-1);
            Sprite temp;
            temp = kart[i].face_cardss;
            int index = kart[i].index;

            kart[i].face_cardss = kart[randomIndex].face_cardss;
            kart[i].index = kart[randomIndex].index;

            kart[randomIndex].face_cardss = temp;
            kart[randomIndex].index = index;
        }

        facultiesYears = new List<int>();
        this.facultiesYears.Add(3);
        this.facultiesYears.Add(3);
        this.facultiesYears.Add(3);
        this.facultiesYears.Add(4);
        this.facultiesYears.Add(3);
        this.facultiesYears.Add(4);
        this.facultiesYears.Add(4);
        this.facultiesYears.Add(3);
        this.facultiesYears.Add(3);
        this.facultiesYears.Add(3);
        this.facultiesYears.Add(6);

        facultiesNames.Add("Šumarski Fakultet");
        facultiesNames.Add("Prehrambeni Fakultet");
        facultiesNames.Add("Saobraćajni Fakultet");
        facultiesNames.Add("Ekonomski Fakultet");
        facultiesNames.Add("Metalurški Fakultet");
        facultiesNames.Add("Arhitektonski Fakultet");
        facultiesNames.Add("Pravni Fakultet");
        facultiesNames.Add("Informatički Fakultet");
        facultiesNames.Add("Mašinski Fakultet");
        facultiesNames.Add("Elektrotehnički Fakultet");
        facultiesNames.Add("Medicinski Fakultet");
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
       if(FaceUp()%2==0)
        {
            
            for(int i=0;i<kart.Count-1;i++)
            {
                for(int j=i+1;j<kart.Count;j++)
                {

                    if ((kart[i].face_up && kart[j].face_up) && kart[i].index == kart[j].index)
                    {
                       
                        kart[i].blocked = true;
                        kart[j].blocked = true;
                        kart[i].ResetListener();
                        kart[j].ResetListener();
                        kart.RemoveAt(i);
                        kart.RemoveAt(j-1);
                        //attemps++;
                    }
                   
                }
            }
            
        }

        if(!hasEnded)
        {
           atmp.text = "Pokušaji: " + attemps.ToString();
        }

        if(kart.Count==0 && !hasEnded)
        {
            kattemps = size / 2;
            if (attemps == kattemps || attemps == kattemps + 1)
            {
                StartCoroutine(EndGame("je položio ispit s ocjenom 10"));
            }
            if (attemps == kattemps + 2 || attemps == kattemps + 3)
            {
                StartCoroutine(EndGame("je položio ispit s ocjenom 9"));
            }
            if (attemps == kattemps + 4 || attemps == kattemps + 5)
            {
                StartCoroutine(EndGame("je položio ispit s ocjenom 8"));
            }
            if (attemps == kattemps + 6 || attemps == kattemps + 7)
            {
                StartCoroutine(EndGame("je položio ispit s ocjenom 7"));
            }
            if (attemps == kattemps + 8 || attemps == kattemps + 9)
            {
                StartCoroutine(EndGame("je položio ispit s ocjenom 6"));
            }
            if (attemps == kattemps + 10 || attemps == kattemps + 11)
            {
                StartCoroutine(EndGame("se izvukao na integralnom i upisao 6"));
            }
            if (attemps > kattemps + 11)
            {
                StartCoroutine(EndGame("je pao ispit i godinu"));
            }

        }
    }
    public int FaceUp()
    {
        int faceupcounter = 0;
        for (int i = 0; i < kart.Count; i++)
        {
            if (kart[i].GetFaceUp() == true && !kart[i].GetFaceDown()) faceupcounter++;
        }
        return faceupcounter;
    }
    public int FaceDown()
    {
        int facedowncounter = 0;
        for (int i = 0; i < kart.Count; i++)
        {
            
            if (kart[i].GetFaceUp() == false && kart[i].GetFaceDown()) facedowncounter++;
        }
        return facedowncounter;
    }
    public void ResetCards()
    {
        for(int i=0;i<kart.Count;i++)
        {
            if (kart[i].blocked == false)
            {
                kart[i].GetComponent<Button>().image.sprite = kart[i].GetFaceCard();
                kart[i].face_up = false;
                kart[i].face_down = true;
            }
        }
        attemps++;
    }

    public IEnumerator EndGame(string endGameText)
    {
        hasEnded = true;
        FakultetManager fm = FindObjectOfType<FakultetManager>();
        string endGameFinalText = sd.playerName + " " + endGameText;
        atmp.text = endGameFinalText;

        if(!(attemps > kattemps + 11))
        {
            int indexFakulteta = sd.playerCurrentFaculties.IndexOf(fm.odabraniFakultet);
            int indexTrajanjaFakulteta = facultiesYears[facultiesNames.IndexOf(fm.odabraniFakultet)];
            if (indexTrajanjaFakulteta == fm.godinaFakulteta)
            {
                sd.playerFinishedFaculties.Add(fm.odabraniFakultet);
                sd.playerCurrentFaculties.RemoveAt(indexFakulteta);
                sd.playerCurrentFacultiesYear.RemoveAt(indexFakulteta);
                endGameFinalText += ". Završio je " + fm.odabraniFakultet + ", čestitamo!";
                gp.SetNewActivity(sd.playerName + " " + endGameText + " i time je završio " + fm.odabraniFakultet + ", čestitamo!");
            }
            else
            {
                sd.playerCurrentFacultiesYear[indexFakulteta]++;
                gp.SetNewActivity(sd.playerName + " " + endGameText + ". " + fm.odabraniFakultet + " mu uručuje uvjerenje za završenu " + fm.godinaFakulteta + ". godinu studija.");
                endGameFinalText += ". Završio je " + fm.godinaFakulteta + ". godinu " + fm.odabraniFakultet;
            }
        }
        else
        {
            gp.SetNewActivity(sd.playerName + " " + endGameText);
        }

        Destroy(fm.gameObject);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainGame");
    }
}
