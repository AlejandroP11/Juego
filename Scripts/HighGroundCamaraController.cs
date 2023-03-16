using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighGroundCamaraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = player.transform.position - new Vector3(0, (float)-17.5, 0);

    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
