using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScript : MonoBehaviour
{
    private Animator slideshowAnimator;
    public List<string> startups;
    public int count;
    public Animator clickAnimator;
    public Animator fadeAnimator;
    private float timeInIdle;

    // Start is called before the first frame update
    void Start()
    {
        timeInIdle = 0;
        count = 0;
        slideshowAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (count == 0)
            {
                print("wow");
                fadeAnimator.Play("FadeIn");
            }

            count += 1;
            slideshowAnimator.Play("startup"+count.ToString());
            clickAnimator.Play("hidden");
            timeInIdle = 0;
        }

        bool proceedPlaying = clickAnimator.GetCurrentAnimatorStateInfo(0).IsName("mouseClick");
        string curClipName = slideshowAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        if ( (!proceedPlaying && curClipName.Substring(0,4)=="idle"))
        {
            timeInIdle += Time.deltaTime;

            if (timeInIdle > 2)
            {
                clickAnimator.Play("mouseClick");
            }
        }

        if (count == 0)
        {
            timeInIdle += Time.deltaTime;
            if (timeInIdle > 3)
                clickAnimator.Play("clickToProcee");
        }
    }

    public void Done()
    {
        print("done");
        SceneManager.LoadScene("SampleScene");
        fadeAnimator.Play("fadeOut");
    }
}
