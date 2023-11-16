using System;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.VisualScripting;
using UnityEngine;

public class SM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
