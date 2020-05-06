using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropMainManager : MonoBehaviour
{
    public float totalMoney;
    public float taxMoney;
    public float money;

    private void Awake()
    {
        int count = FindObjectsOfType<CoinDropMainManager>().Length;
        if (count > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public float GetTotalMoney()
    {
        return totalMoney;
    }
}
