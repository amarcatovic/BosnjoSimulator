using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPath : MonoBehaviour
{
    CoinDropConfig coinConfig;
    List<Transform> waypoints;
    Coin coin;
    CoinDropGamePlay gameplay;

    int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = coinConfig.GetWaypoints();
        transform.position = waypoints[currentIndex].transform.position;
        coin = gameObject.GetComponent<Coin>();
        gameplay = FindObjectOfType<CoinDropGamePlay>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (currentIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[currentIndex].transform.position;
            var movementThisFrame = coinConfig.GetSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                currentIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
            gameplay.AddMoneyWasted(coin.GetValue());
            
        }
    }

    public void SetWaveConfig(CoinDropConfig wc)
    {
        coinConfig = wc;
    }
}
