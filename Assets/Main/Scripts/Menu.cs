using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject[] Menus; 
    public GameObject buttonOpen;
    public GameObject configs;
    public int indexMenu;
    public GameObject buttonConfigs;


    void Start()
    {
        menu.SetActive(false);
        buttonOpen.SetActive(true);
        buttonConfigs.SetActive(true);
        indexMenu = 0;
        configs.SetActive(false);

        foreach (GameObject elemento in Menus)
        {
            // Faz algo com cada elemento
            elemento.SetActive(false);
        }
    }

    public void AbrirMenu()
    {

        Menus[indexMenu].SetActive(true);
        buttonOpen.SetActive(false);
        buttonConfigs.SetActive(false);
    }

    public void FecharMenu()
    {
        Menus[indexMenu].SetActive(false);
        buttonOpen.SetActive(true);
        buttonConfigs.SetActive(true);
    }

    public void NextMenu()
    {
        Menus[indexMenu].SetActive(false);

        if(indexMenu < Menus.Length - 1)
        {
            indexMenu += 1;
        } else
        {
            indexMenu = 0;
        }
        
        Menus[indexMenu].SetActive(true);
    }

    public void PreviousMenu()
    {
        Menus[indexMenu].SetActive(false);

        if (indexMenu <= 0)
        {
            indexMenu = Menus.Length - 1;
        }
        else
        {
            indexMenu -= 1;
        }

        Menus[indexMenu].SetActive(true);
    }

    public void OpenConfigs()
    {
        configs.SetActive(true);
    }

    public void CloseConfigs()
    {
        configs.SetActive(false);
    }
}
