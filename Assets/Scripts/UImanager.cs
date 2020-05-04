using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public List<UnityEngine.UI.Image> health;
    public Sprite active;
    public Sprite inactive;
    public CharacterController character;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < health.Count; x++)
        {
            if (character.health>x)
            {
                health[x].GetComponent<Image>().sprite = active;
            }
            else
            {
                health[x].GetComponent<Image>().sprite = inactive;
            }
        }
    }
}
