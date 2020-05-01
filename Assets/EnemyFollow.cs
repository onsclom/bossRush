using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed;
    private Vector3 startLocation;
    public Vector3 targetLocation;
    public float randomWalkRange = 3;
    // Start is called before the first frame update
    void Start()
    {
        targetLocation = transform.position;
        startLocation = transform.position;
        player = GameObject.Find("Character").transform.Find("Person");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector2.Distance(player.position, transform.position);

        if (dist < 5 || gameObject.GetComponent<EnemyScript>().health < 2)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
        else
        {
            if (transform.position.x == targetLocation.x && transform.position.y == targetLocation.y )
            {
                print("wow they are ==");
                targetLocation = new Vector3 (startLocation.x + Random.Range(-randomWalkRange, randomWalkRange), startLocation.y + Random.Range(-randomWalkRange, randomWalkRange), transform.position.z );
            }
            else
            {
                print("following with speed: " + speed);
                print(transform.position + " " + targetLocation);
            }

            transform.position = Vector2.MoveTowards(transform.position, targetLocation, speed*Time.deltaTime);
        }
    }
}
