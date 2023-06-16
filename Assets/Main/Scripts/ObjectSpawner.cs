using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectSpawner : MonoBehaviour
{
    private GameObject objectToSpawn;
    private PlacementIndicator placementIndicator;
    public GameObject indicator;
    private GameObject protoboard;
    private GameObject placa;
    private GameObject led;
    private GameObject resistor;
    private GameObject arduino;
    

    public GameObject menu;
    private string nameObj;

    private int placedPrefabCount;
    public int placedPrefabMax;

    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        menu.SetActive(false);
        indicator.SetActive(false);
        
    }

    public void SpawnObject()
    {
        if (placedPrefabCount < placedPrefabMax && nameObj == "Protoboard" && protoboard == null) {
            protoboard = Instantiate(objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
            placedPrefabCount++;
            indicator.SetActive(false);
            
        }
        else if (placedPrefabCount < placedPrefabMax && nameObj == "Placa" && placa == null)
        {
            placa = Instantiate(objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
            placedPrefabCount++;
            indicator.SetActive(false);
        }
        else if (placedPrefabCount < placedPrefabMax && nameObj == "Resistor" && resistor == null)
        {
            resistor = Instantiate(objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
            placedPrefabCount++;
            indicator.SetActive(false);
        }
        else if(placedPrefabCount < placedPrefabMax && nameObj == "Led" && led == null)
        {
            led = Instantiate(objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
            placedPrefabCount++;
            indicator.SetActive(false);
        }
        else if(placedPrefabCount < placedPrefabMax && nameObj == "Arduino" && arduino == null) 
        {
            arduino = Instantiate(objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
            placedPrefabCount++;
            indicator.SetActive(false);
        }
    }

    public void SetPrefab(GameObject prefabType)
    {
        objectToSpawn = prefabType;
        indicator.SetActive(true);
        DesativarFerramentas();
    }

    public void SetNameObject(string Nome)
    {
        nameObj = Nome;
    }

    public void DestroyObject()
    {
        Destroy(protoboard);
        Destroy(placa);
        Destroy(resistor);
        Destroy(led);
        Destroy(arduino);
        placedPrefabCount = 0;
    }

    public void AtivarFerramentas() 
    {
        menu.SetActive(true);
    }

    public void DesativarFerramentas()
    {
        menu.SetActive(false);
    }
}
