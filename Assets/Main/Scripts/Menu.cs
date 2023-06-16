using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject buttonOpen;

    void Start()
    {
        menu.SetActive(false);
        buttonOpen.SetActive(true);
    }

    public void AbrirMenu()
    {
        menu.SetActive(true);
        buttonOpen.SetActive(false);
    }

    public void FecharMenu()
    {
        menu.SetActive(false);
        buttonOpen.SetActive(true);
    }
}
