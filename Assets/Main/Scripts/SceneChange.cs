using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void Entrar(string nomeDaScene) 
    {
        SceneManager.LoadScene(nomeDaScene);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
