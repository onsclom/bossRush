using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScript : MonoBehaviour
{
    private Animator animator;
    public List<string> startups;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 1;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            count += 1;
            animator.Play("startup"+count.ToString());
        }
    }

    public void Done()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
