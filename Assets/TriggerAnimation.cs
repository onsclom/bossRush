using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    private bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (triggered==false && other.gameObject.tag=="Player")
        {
            triggered = true;
            other.transform.parent.gameObject.GetComponent<CharacterController>().playBossAnimation();
        }
           
    }
}
