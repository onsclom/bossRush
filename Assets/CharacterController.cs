using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject character;
    public GameObject ball;
    public float h;
    public float v;
    public float speed;
    public ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        character = transform.Find("Person").gameObject;
        ball = transform.Find("Ball").gameObject;
        particles = ball.transform.Find("Particle System").gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("wow");
            particles.Play();
        }

    }

    void FixedUpdate()
    {
        Rigidbody2D charRB = character.GetComponent<Rigidbody2D>();
        charRB.MovePosition(charRB.position + new Vector2(h, v) * Time.fixedDeltaTime * speed);

         float yDiff = (charRB.position.y - ball.transform.position.y);
         float xDiff = (charRB.position.x - ball.transform.position.x);
    //     ball.transform.position = new Vector3 (ball.transform.position.x + xDiff/20, ball.transform.position.y + yDiff/20, ball.transform.position.z);
    
        Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
        ballRB.AddForce(new Vector2(xDiff*4,yDiff*4));
    }

}
