using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public string bossName;
    public int maxSize = 400;
    public GameObject bossHealthUI;

    public RectTransform healthBar;

    public float spawnTime;

    public float timeSinceHit;

    public bool scareCrow;


    void Start()
    {
        timeSinceHit = 1;

        if (health == 0)
        {
            health = 2;
        }

        maxHealth = health;

        bossHealthUI = GameObject.Find("GameManager").transform.Find("UI").Find("Boss Bar").gameObject;
        healthBar = GameObject.Find("GameManager").transform.Find("UI").Find("Boss Bar").Find("Health").gameObject.GetComponent<RectTransform>();
    
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceHit += Time.deltaTime;

        if (timeSinceHit < .2)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void hit(GameObject orb)
    {
        if (scareCrow)
        {
            print("WOW");
            gameObject.GetComponent<Animator>().Play("scarecrowHit");
            return;
        }

        timeSinceHit = 0;

        health -= 1;

        if (bossName != "")
        {
                bossHealthUI.SetActive(true);
                healthBar.sizeDelta = new Vector2(health/maxHealth*400, 20);
        }
        
        print("KNOCKBACK!");

        float xDiff = orb.transform.position.x - transform.position.x;
        float yDiff = orb.transform.position.y - transform.position.y;
    
        Vector2 dir = new Vector2(xDiff, yDiff);

        dir.Normalize();

        gameObject.GetComponent<Rigidbody2D>().AddForce(dir * -200);


        if (health == 0 && !scareCrow)
        {
            var particles = transform.Find("Particle System").gameObject;
            particles.transform.parent=transform.parent;
            particles.GetComponent<ParticleSystem>().Play();

            Destroy(gameObject);

            if (bossName != "")
                bossHealthUI.SetActive(false);
        }
    }

    void showBossHealth() {
        bossHealthUI.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D col) {
 
        if (col.gameObject.tag == "Player" && !scareCrow)
            col.gameObject.transform.parent.gameObject.GetComponent<CharacterController>().hit(transform);
    }
}
