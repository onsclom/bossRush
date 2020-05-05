using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossActivate : MonoBehaviour
{
    public Animator bossPlayer;
    public UnityEngine.UI.Image mainImage;
    // Start is called before the first frame update
    void Start()
    {
        bossPlayer = gameObject.GetComponent<Animator>();   
        mainImage = gameObject.GetComponent<UnityEngine.UI.Image>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            runAnim();
        }
    }

    public void runAnim()
    {
        Color curColor = mainImage.color;
        curColor.a = 1;
        mainImage.color = curColor;
        bossPlayer.Play("bossAnim", -1, 0);
    }

    public void removeAnim()
    {
        Color curColor = mainImage.color;
        curColor.a = 0;
        mainImage.color = curColor;
    }
}
