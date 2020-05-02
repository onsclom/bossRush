using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;

    public Vector2 offset;

    private Animator animator;

    private float timeElapsed;

    public GameObject bats;

    float time;
    string curMode;

    int curAttackNum;

    List<string> attacks;

    public GameObject slimes;
    // Start is called before the first frame update
    void Start()
    {
        attacks = new List<string>();
        attacks.Add("warlockBlueAttack");
        attacks.Add("warlockGreenAttack");
        attacks.Add("warlockGrayAttack");

        curAttackNum = 0;

        animator = gameObject.GetComponent<Animator>();

        time = 0;
        player = GameObject.Find("GameManager").transform.Find("Character").Find("Person").gameObject;
    
        offset = new Vector2(-.75f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnBats();
        }

        timeElapsed += Time.deltaTime;

        if (timeElapsed > 2)
        {
            timeElapsed = 0;
            curAttackNum += 1;

            if (curAttackNum % 12 == 0)
            {
                animator.Play(attacks[2]);
            }
            else if (curAttackNum % 6 == 0)
            {
                animator.Play(attacks[1]);
            }
            else
            {
                animator.Play(attacks[0]);
            }
        }
    }

    private void FixedUpdate() {
        // time += Time.deltaTime;

        // if (time>.5f)
        // {
        //     time -= .5f;
        //     Shoot();
        // }
    }

    void Shoot()
    {
        GameObject curBullet = Instantiate(bullet, transform.position + new Vector3 (offset.x, offset.y, -2), Quaternion.identity);
        
        float distX = transform.position.x+offset.x - player.transform.position.x;
        float distY = transform.position.y+offset.y - player.transform.position.y;
        
        curBullet.GetComponent<Bullet>().dir = new Vector2(-distX,-distY);

        curBullet = Instantiate(bullet, transform.position + new Vector3 (offset.x, offset.y, -2), Quaternion.identity);
        curBullet.GetComponent<Bullet>().dir = RotateVector(new Vector2(-distX,-distY),10);

        curBullet = Instantiate(bullet, transform.position + new Vector3 (offset.x, offset.y, -2), Quaternion.identity);
        curBullet.GetComponent<Bullet>().dir = RotateVector(new Vector2(-distX,-distY),-10);

    }

    void SpawnBats()
    {
        GameObject newBats = Instantiate(bats, new Vector3 (0, 20, -1), Quaternion.identity);
    }

    void SpawnSlimes()
    {
        GameObject newSlimes = Instantiate(slimes, new Vector3 (0, 0, -1), Quaternion.identity);
    }

    public Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle*Mathf.Deg2Rad;
        float _x = v.x*Mathf.Cos(radian) - v.y*Mathf.Sin(radian);
        float _y = v.x*Mathf.Sin(radian) + v.y*Mathf.Cos(radian);
        return new Vector2(_x,_y);
    }
}
