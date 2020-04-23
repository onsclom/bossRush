using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = (GameObject.Find("Character").transform.Find("Person"));

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector2.Distance(player.position, transform.position);

        if (dist < 5 || gameObject.GetComponent<EnemyScript>().health < 2)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, player.position, 0*Time.deltaTime);
    }
}
