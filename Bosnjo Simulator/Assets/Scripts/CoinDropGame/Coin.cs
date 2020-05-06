using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float value;
    private CoinDropMainManager manager;
    private CoinDropGamePlay gameplay;

    private void OnTriggerEnter2D(Collider2D withBosnjo)
    {
        manager.totalMoney += value;
        gameplay.AddMoneyAndChangeScore(value);
        Destroy(gameObject);
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Awake()
    {
       manager = FindObjectOfType<CoinDropMainManager>();
       gameplay = FindObjectOfType<CoinDropGamePlay>();
    }

     public float GetValue() { return value;  }
}
