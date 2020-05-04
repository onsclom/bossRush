using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ManaScript : MonoBehaviour
{
    public float maxSize = 41;
    public CharacterController characterInfo;
    public RectTransform manaBar;
    public RectTransform manaBar2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        manaBar.sizeDelta = new Vector2(characterInfo.curMana/100*maxSize ,manaBar.sizeDelta.y); 
        manaBar2.sizeDelta = new Vector2(characterInfo.curMana/100*maxSize ,manaBar2.sizeDelta.y);     
    }
}
