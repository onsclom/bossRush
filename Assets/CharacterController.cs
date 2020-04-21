using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject character;
    public GameObject ball;
    public GameObject enemy;
    public float h;
    public float v;
    public float speed;
    public float idleRange;
    private float idleOffsetX;
    private float idleOffsetY;
    private float idleTime;
    public ParticleSystem particles;
    public float attackRadius = .2f;
    public bool following = true;
    public int framesSinceFroze = 60;
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

        if (Input.GetKeyDown(KeyCode.M))
        {
            print("wow");
            particles.Play();
            attack();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            following = false;
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            following = true;
            framesSinceFroze = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject newEnemy = Instantiate(enemy, new Vector2(0,4), Quaternion.identity);
        }

        if (h==0 && v==0)
        {
            idleTime += Time.deltaTime;
        }
        else
        {
            idleTime = 0;
        }
        
        if (idleTime > 5)
        {
            int cur = (int)Mathf.Floor(idleTime);
            
            if (cur % 2 % 2 == 0)
            {
                idleOffsetX = (float).2;
            }
            else
            {
                idleOffsetX = (float)-.2;
            }

            cur = (int)Mathf.Floor(idleTime);
            cur %= 3;
            idleOffsetY = (float)(((float)cur-1)/7);

            
        }
        else
        {
            idleOffsetX = 0;
            idleOffsetY = 0;
        }

    }

    void FixedUpdate()
    {
        Rigidbody2D charRB = character.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(h, v);
        dir.Normalize();
        charRB.MovePosition(charRB.position + dir * Time.fixedDeltaTime * speed);

        if (following) {
            float yDiff = (charRB.position.y - ball.transform.position.y)+1+idleOffsetY;
            float xDiff = (charRB.position.x - ball.transform.position.x)+idleOffsetX;
        //     ball.transform.position = new Vector3 (ball.transform.position.x + xDiff/20, ball.transform.position.y + yDiff/20, ball.transform.position.z);
        
            Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
            ballRB.AddForce(new Vector2(xDiff*4,yDiff*4));


            if (framesSinceFroze < 1)
            {
                // ballRB.AddForce(new Vector2(xDiff*8,yDiff*8)); //add the force again for an initial boost!
                Vector2 angle = new Vector2(xDiff, yDiff);
                angle.Normalize();
                ballRB.AddForce(angle*500);
                
                framesSinceFroze += 1;
            }
        }
        else
        {
            Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
            ballRB.velocity = (new Vector2(0,0));
        }
    }

    void attack()
    {
        Collider2D cur = Physics2D.OverlapCircle(ball.transform.position,  attackRadius, LayerMask.GetMask("enemy"));
        if (cur != null)
        {
            Destroy(cur.gameObject);
        }
    }

}
