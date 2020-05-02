using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ManaScript : MonoBehaviour
{
    public float maxSize = 114f;
    public CharacterController characterInfo;
    public RectTransform manaBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        manaBar.sizeDelta = new Vector2(characterInfo.curMana/100*114 ,manaBar.sizeDelta.y);   
    }
}
