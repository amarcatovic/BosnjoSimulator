using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Coin Drop Config")]
public class CoinDropConfig : ScriptableObject
{
    [SerializeField] List<GameObject> coins;
    [SerializeField] GameObject path;
    [SerializeField] float speed;

    public List<GameObject> GetCoins() { return coins; }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in path.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }

    public float GetSpeed() { return speed; }
}
