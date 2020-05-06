using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] List<CoinDropConfig> coinConfigs;
    [SerializeField] int startingWave = 0; //index 0 je start
    [SerializeField] float offset = 0.5f;

    void Start()
    {      
      SpawnAllWaves();
    }

    void SpawnAllWaves()
    {
        for (int i = startingWave; i < coinConfigs.Count; i++)
        {
            var currentCoin = coinConfigs[i];
            StartCoroutine(SpawnAllCoinsInWave(currentCoin));
        }
    }

    IEnumerator SpawnAllCoinsInWave(CoinDropConfig coin)
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0f + offset, 10f));
            offset += 0.2f;
            int randomNumberForHighCoins = Random.Range(0, 100);
            int randomCoin;
            if (randomNumberForHighCoins < 90)
            {
                randomCoin = Random.Range(0, 9);
            }
            else
            {
                randomCoin = Random.Range(9, 11);
            }
            var newCoin = Instantiate(coin.GetCoins()[randomCoin], coin.GetWaypoints()[0].transform.position, Quaternion.identity);
            newCoin.GetComponent<CoinPath>().SetWaveConfig(coin);
        }
    }
}
