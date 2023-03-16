using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Variables p�blicas que pueden ser modificadas desde el inspector
    public float playerHealth = 100;
    public float playerDamage = 10;
    public float speed = 0;

    // Componentes privados
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Contador de objetos recogidos
    private int PickUpsRecogidos = 0;

    // Puertas para los diferentes niveles
    public GameObject doorLevel1;
    public GameObject doorLevel2;

    // Arreglo de los datos de los enemigos
    private DataEnemy[] enemiesData;

    // Contador de los enemigos que se han eleminado
    private int enemyKills = 0;

    // Arreglo de todos los pickups
    private GameObject[] pickups;

    // Funci�n llamada una vez al inicio
    void Start()
    {
        // Buscamos el componente Rigidbody en el objeto del jugador
        rb = GetComponent<Rigidbody>();

        // Buscamos todos los objetos Enemy y les agregamos su respectivo DataEnemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesData = new DataEnemy[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            DataEnemy dataEnemy = enemies[i].GetComponent<DataEnemy>();
            if (dataEnemy != null)
            {
                enemiesData[i] = dataEnemy;
            }
        }

        // Buscar todos los objetos con la tag PickUp
        pickups = GameObject.FindGameObjectsWithTag("PickUp");
    }

    // Funci�n llamada cada vez que se detecta un movimiento del joystick o teclado
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Funci�n llamada en cada frame en el que el objeto tenga f�sica
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    // Funci�n para reiniciar el nivel
    void ResetLevel()
    {
        // Reiniciar la posici�n del jugador y su salud
        transform.position = new Vector3(0, 0, 0);

        // Activar todos los objetos de recolecci�n
        foreach (GameObject pickup in pickups)
        {
            pickup.SetActive(true);
        }

        // Reiniciar el contador de objetos recogidos
        PickUpsRecogidos = 0;
        playerHealth = 100;

        // Vuelve a abrir la puerta del segundo nivel
        doorLevel2.transform.position = new Vector3(-6, 0, 20);
    }

    // Funci�n llamada en cada frame
    private void Update()
    {
        // Si el jugador llega al final del nivel 1, se mueven las puertas
        if (transform.position.z >= 21)
        {
            doorLevel1.transform.position = new Vector3(0, 0, 10);
            doorLevel2.transform.position = new Vector3(0, 0, 20);
        }

        // Si la salud del jugador llega a 0, se reinicia el nivel
        if (playerHealth == 0)
        {
            ResetLevel();
        }

        // Si el jugador se cae del mapa, vuelve a la plataforma del parkour
        if(transform.position.y <= -1)
        {
            transform.position = new Vector3(0, 0.5f, 49);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que choc� tiene la etiqueta "PickUp"
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Desactivar el objeto que choc�
            other.gameObject.SetActive(false);

            // Incrementar el contador de objetos recolectados
            PickUpsRecogidos++;

            Debug.Log("Objetos recogidos: " + PickUpsRecogidos.ToString());

            // Verificar si se han recolectado 10 objetos
            if (PickUpsRecogidos == 10)
            {
                // Mover la puerta del nivel 1 a su posici�n desbloqueada
                doorLevel1.transform.position = new Vector3(6, -1, 10);
            }
        }

        if (other.gameObject.CompareTag("MedKit"))
        {
            // Desactivar el objeto que choc�
            other.gameObject.SetActive(false);

            Debug.Log("Recogiste un botiqu�n, vida restaurada al m�ximo");

            //Recuperar la vida del jugador al m�ximo
            playerHealth = 100;
        }

        if (other.gameObject.CompareTag("Portal"))
        {
            if(enemyKills == 6)
            {
                transform.position = new Vector3(0, 0.5f, 49);
            }
            else
            {
                Debug.Log("Debes eleminar a todos los enemigos si quieres usar el portal");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto que choc� tiene la etiqueta "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DataEnemy enemyData = null;

            // Buscar el componente DataEnemy en el objeto que choc�
            for (int i = 0; i < enemiesData.Length; i++)
            {
                if (enemiesData[i] != null && enemiesData[i].gameObject == collision.gameObject)
                {
                    enemyData = enemiesData[i];
                    break;
                }
            }

            // Si se encontr� el componente DataEnemy
            if (enemyData != null)
            {
                // Reducir la salud del enemigo con el da�o del jugador
                enemyData.enemyHealth -= playerDamage;

                // Reducir la salud del jugador con el da�o del enemigo
                playerHealth -= enemyData.damage;

                // Imprimir un mensaje en la consola con la cantidad de da�o que recibi� el jugador
                Debug.Log("Vida = " + playerHealth);

                // Verificar si la salud del enemigo es menor o igual a cero
                if (enemyData.enemyHealth <= 0)
                {
                    // Desactivar el objeto enemigo
                    collision.gameObject.SetActive(false);

                    // A�adimos uno a los enmigos eleminados
                    enemyKills++;

                    Debug.Log("Enemigos eleminados: " + enemyKills);
                }
            }
        }
    }
}