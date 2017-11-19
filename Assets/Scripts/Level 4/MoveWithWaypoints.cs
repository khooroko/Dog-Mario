using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithWaypoints : MonoBehaviour {

    // Use this for initialization
    private Transform startMarker, endMarker;
    public Transform[] waypoints;
    public int currentStartPoint;
    private float startTime, journeyLength;
    private float speed = 14f;


    void Start()
    {
        currentStartPoint = 0;
        SetPoints();

    }

    void SetPoints()
    {
        if (currentStartPoint == 1)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            speed = 1f;
            
        }

        if (currentStartPoint == 2)
        {
            speed = 5f;
        }
        startMarker = waypoints[currentStartPoint];
        if (currentStartPoint + 1 < waypoints.Length) {
            endMarker = waypoints[currentStartPoint + 1];
        }
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Update is called once per frame
    void Update()
    {
 
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        if (fracJourney >= 1f && currentStartPoint + 1 < waypoints.Length)
        {
            currentStartPoint++;
            SetPoints();
        }
    }
}
