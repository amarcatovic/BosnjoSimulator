using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admin : MonoBehaviour
{
    private void Awake()
    {
        if(FindObjectsOfType<Admin>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddMoney()
    {
        FindObjectOfType<Gameplay>().GetSaveData().playerMoney += 10000f;
        FindObjectOfType<GameSaveLogic>().Save();
    }
}
