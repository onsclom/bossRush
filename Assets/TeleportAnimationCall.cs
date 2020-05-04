using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAnimationCall : MonoBehaviour
{
    public CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TP() 
    {
        cc.Teleport();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
