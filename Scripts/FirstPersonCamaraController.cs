using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamaraController : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
