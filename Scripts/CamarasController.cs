using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamarasController : MonoBehaviour
{
    public GameObject[] cameras;
    private int nCameras = 3;

    // Start is called before the first frame update
    void Start()
    {
        cameras[0].gameObject.SetActive(true);
    }

    // Método que apaga todas las cámaras.
    void TurnOffCameras()
    {
        for(int i = 0; i < nCameras; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }

    // Método con el que sabremos si el jugador presiona una tecla, y nos devuelve su valor si es una de las preseleccionadas para las cámaras, si no devuelve 0000.
    public int GetKeyboardInput()
    {
        Keyboard keyboard = InputSystem.GetDevice<Keyboard>();
        if (keyboard != null)
        {
            if (keyboard.numpad1Key.isPressed || keyboard.digit1Key.isPressed)
            {
                return 1;
            }
            else if (keyboard.numpad2Key.isPressed || keyboard.digit2Key.isPressed)
            {
                return 2;
            }
            else if (keyboard.numpad3Key.isPressed || keyboard.digit3Key.isPressed)
            {
                return 3;
            }
            else
            {
                return 0000;
            }
        }
        else
        {
            return 0000;
        }
    }

    // Update is called once per frame
    void Update()
        {
            int numPressed = GetKeyboardInput();
            if(numPressed == 1)
            {
                TurnOffCameras();
                cameras[0].gameObject.SetActive(true);
            }
            if(numPressed == 2)
            {
                TurnOffCameras();
                cameras[1].gameObject.SetActive(true);
            }
            if(numPressed == 3)
            {
                TurnOffCameras();
                cameras[2].gameObject.SetActive(true);
            }
        }
    }
