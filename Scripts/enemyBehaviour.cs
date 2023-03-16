using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private Vector3 positionToGo;
    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.z > 20 && player.transform.position.z < 40)
        {
            positionToGo = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, positionToGo, speed * Time.deltaTime);
            
        }
    }
}
