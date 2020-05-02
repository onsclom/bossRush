using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject slime;
    private float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > 3)
        {
            Instantiate(slime, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
