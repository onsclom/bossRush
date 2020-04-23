using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour
{
    public float health;
    public float maxHealth;
    // Start is called before the first frame update
    public float lastHitTime;

    public string bossName;
    public int maxSize = 400;
    public GameObject bossHealthUI;

    public RectTransform healthBar;


    void Start()
    {
        if (health == 0)
        {
            health = 2;
        }
        lastHitTime = 1;

        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {  
       lastHitTime += Time.deltaTime; 
    }

    public void hit(GameObject orb)
    {

        if (lastHitTime < 1)
            return;

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

        lastHitTime = 0;


        if (health == 0)
        {
            var particles = 
            Instantiate(transform.parent.gameObject.GetComponent<EnemyManager>().deathParticles, transform.position, Quaternion.identity);
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
 
        if (col.gameObject.tag == "Player")
            col.gameObject.transform.parent.gameObject.GetComponent<CharacterController>().hit(transform);
    }
}
