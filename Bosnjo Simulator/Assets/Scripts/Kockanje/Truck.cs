using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField] string truckName;
    [SerializeField] Transform waypointEnd;

    float changeSpeedSec = 0.5f;
    float secPassed = 0.7f;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<RaceDirector>().RegisterTruckFinish(truckName);
    }

    private void Move()
    {
        float randomVelocity = UnityEngine.Random.Range(0.1f, 1f);
        var targetPosition = waypointEnd.transform.position;
        var movementThisFrame = randomVelocity * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
    }
}
