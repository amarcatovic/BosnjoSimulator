using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KladionicaManager : MonoBehaviour
{
    /* TODO: ADD EVENT RANDOMIZER */

    public void Tiket()
    {
        SceneManager.LoadScene("Tiket");
    }

    public void Bingo()
    {
        SceneManager.LoadScene("Bingo");
    }

    public void UtrkaKonja()
    {
        SceneManager.LoadScene("TrkeKamiona");
    }

}
