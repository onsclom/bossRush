using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 dir;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 4.25f;
        dir.Normalize();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3 (dir.x, dir.y, 0) * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent.gameObject.GetComponent<CharacterController>().hit(transform);
        }
    }
}
