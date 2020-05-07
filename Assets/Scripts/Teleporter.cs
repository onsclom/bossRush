using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public Animator transition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        print("wow");
        if (other.gameObject.tag == "Player")
        {
         //   transition.Play("fadeOut");
            print("TELEPORT");

            DontDestroyOnLoad(GameObject.Find("GameManager"));

            StartCoroutine(LoadLevelAfterDelay(.9f, other));

            
        }
    }

    IEnumerator LoadLevelAfterDelay(float delay, Collider2D other)
    {
        yield return new WaitForSeconds(delay);
        other.gameObject.transform.position = new Vector2(0,0);
        SceneManager.LoadScene("Boss1");
    }
}
