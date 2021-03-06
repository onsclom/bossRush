﻿using System.Collections;
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
    public ParticleSystem charging;
    public ParticleSystem charged;
    public float attackRadius = .5f;
    public bool following = true;
    public int framesSinceFroze = 60;
    private float chargedTime = 0;
    private bool isCharged = false;
    public Color inactiveIndicatorColor;
    public Color chargedIndicatorColor;
    private float attackingTime = 0;
    public SimpleCameraShakeInCinemachine sceenShake;
    public Transform enemyManager;

    public int health = 4;
    public float invincibleTime = 0;

    public float curMana = 100f;

    private bool ranOut = false;
    private HashSet<GameObject> thingsHit;

    private Animator animator;

    public GameObject explosionSound;
    public GameObject chargedSound;
    public GameObject teleportSound;
    public GameObject hurtSound;
    public BossActivate bossAnimationThing;

    // Start is called before the first frame update
    void Start()
    {
        thingsHit = new HashSet<GameObject>();
        character = transform.Find("Person").gameObject;
        ball = transform.Find("Ball").gameObject;
        particles = ball.transform.Find("Particle System").gameObject.GetComponent<ParticleSystem>();
        charging = ball.transform.Find("charging").gameObject.GetComponent<ParticleSystem>();
        charged = ball.transform.Find("charged").gameObject.GetComponent<ParticleSystem>();
        animator = transform.Find("Person").gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.O))
        {
            print("Wow");
            animator.Play("teleportOut");
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("New Animation") && (h!=0 || v!=0))
        {
            print("running");
            animator.Play("running");
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("running") && h==0 && v==0)
        {
            print("idling");
            animator.Play("New Animation");
        }

        attackingTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (curMana>= 33)
            {
                particles.Play();
                Instantiate(explosionSound);
                attackingTime = 0;
                sceenShake.setShake();
                curMana -= 33;
                thingsHit = new HashSet<GameObject>();
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //switch locations teleport thing
            if (curMana >= 20f)
            {
                curMana -= 20;
                animator.Play("teleportOut");
                Instantiate(teleportSound);
            }
        }

        if (attackingTime < .5)
        {
            attack();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (curMana > 0)
            {
                following = false;
                charging.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            following = true;
            framesSinceFroze = 0;

            charging.Stop();
        }

        if (!following)
        {
            chargedTime += Time.deltaTime;

            if (!isCharged && chargedTime > .66)
            {
                isCharged = true;
                charged.Play();
                Instantiate(chargedSound);
                charging.Stop();

                character.transform.Find("Indicator").GetComponent<SpriteRenderer>().color = chargedIndicatorColor;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject newEnemy = Instantiate(enemy, new Vector2(0,4), Quaternion.identity);
            newEnemy.transform.parent = enemyManager.transform;
            newEnemy.SetActive(true);
        }

        if (h==0 && v==0)
        {
            idleTime += Time.deltaTime;
            character.GetComponent<Animator>().SetBool("running", false);
        }
        else
        {
            idleTime = 0;
            character.GetComponent<Animator>().SetBool("running", true);
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

        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            character.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            character.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;

            var cur = character.GetComponent<SpriteRenderer>().color;
            cur.a = .5f;
            character.GetComponent<SpriteRenderer>().color = cur;
        }
        else
        {
            var cur = character.GetComponent<SpriteRenderer>().color;
            cur.a = 1f;
            character.GetComponent<SpriteRenderer>().color = cur;
        }

        curMana += Time.deltaTime*20;

        curMana = Mathf.Min(100f, curMana);
        curMana = Mathf.Max(0f, curMana);
    }

    public void playBossAnimation()
    {
        bossAnimationThing.runAnim();
    }

    public void Teleport()
    {
        Vector3 oldOrbPos = ball.transform.position;
        ball.transform.position = character.transform.position;
        character.transform.position = oldOrbPos;

        ball.GetComponent<TrailRenderer>().Clear();
    }

    void FixedUpdate()
    {
        Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
        Rigidbody2D charRB = character.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(h, v);
        dir.Normalize();
        charRB.MovePosition(charRB.position + dir * Time.fixedDeltaTime * speed);

        if (following) {
            float yDiff = (charRB.position.y - ball.transform.position.y)+1+idleOffsetY;
            float xDiff = (charRB.position.x - ball.transform.position.x)+idleOffsetX;
        //     ball.transform.position = new Vector3 (ball.transform.position.x + xDiff/20, ball.transform.position.y + yDiff/20, ball.transform.position.z);
        

            ballRB.AddForce(new Vector2(xDiff*4,yDiff*4));


            if (framesSinceFroze < 1)
            {
                // ballRB.AddForce(new Vector2(xDiff*8,yDiff*8)); //add the force again for an initial boost!
                Vector2 angle = new Vector2(xDiff, yDiff);
                angle.Normalize();

                if (isCharged && !ranOut)
                {
                    ballRB.AddForce(angle*700);
                    ballRB.drag = 1;
                }
                else if (!ranOut)
                {
                    ballRB.AddForce(angle*350*chargedTime);
                }

                ranOut = false;

                chargedTime = 0;
                isCharged = false;
                character.transform.Find("Indicator").GetComponent<SpriteRenderer>().color = inactiveIndicatorColor;
                
                framesSinceFroze += 1;
            }
        }
        else
        {
            ballRB.velocity = (new Vector2(0,0));

            curMana -= Time.deltaTime*30;

            curMana = Mathf.Max(curMana, 0);

            if (curMana == 0)
            {
                ranOut = true;
                following = true;
                charging.Stop();
            }
        }

        if (ballRB.drag < 1.5)
        {
            ballRB.drag += (float)Time.deltaTime*.3f;
            ballRB.drag = Mathf.Min(1.5f, ballRB.drag);
            print(ballRB.drag);
        }
    }

    void attack()
    {
        Collider2D[] cur = Physics2D.OverlapCircleAll(ball.transform.position,  attackRadius, LayerMask.GetMask("enemy"));
        
        foreach (Collider2D collision in cur)
        {
            if ( thingsHit.Contains(collision.gameObject) == false )
            {
                thingsHit.Add(collision.gameObject);
                collision.gameObject.GetComponent<EnemyScript>().hit(ball);
            }
        }
    }

    public void hit(Transform hitter)
    {
        if (invincibleTime > 0)
            return;

        float xDiff = hitter.position.x - character.transform.position.x;
        float yDiff = hitter.position.y - character.transform.position.y;
    
        print(yDiff+" "+xDiff);

        Vector2 dir = new Vector2(xDiff, yDiff);

        dir.Normalize();

        character.GetComponent<Rigidbody2D>().AddForce(dir * -1200);

        invincibleTime = 2;

        health -= 1;

        Instantiate(hurtSound);
    }
}
