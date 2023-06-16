using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Movement : MonoBehaviour
{
    [SerializeField]
    ARPlaneManager planeManager;

    List<GameObject> objectsInScene = new();

    //Detectar a colisão
    [SerializeField]
    ARRaycastManager arraycastManager;

    //Lista de colisões
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //Objetos para inserir no plano

    [SerializeField]
    GameObject[] objectsPrefabs;

    int indexObject;

    //Camera AR
    Camera arCamera;

    //Objeto já criado na cena
    GameObject spawnObject;
  
    void Start()
    {
        spawnObject = null;
        arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
        indexObject = -1;
    }


    void Update()
    {
        if (Input.touchCount == 0) 
            return;

        RaycastHit hit;

        Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);

        if (arraycastManager.Raycast(Input.GetTouch(0).position, hits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && spawnObject == null)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.gameObject.CompareTag("ItemSpawn"))
                    {
                        spawnObject = hit.collider.gameObject;
                        
                    }
                    else 
                    {
                        SpawnPrefab(hits[0].pose.position);
                    }
                }

            }
            else if(Input.GetTouch(0).phase == TouchPhase.Moved && spawnObject != null)
            {
                spawnObject.transform.position = hits[0].pose.position;
            }

            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                spawnObject = null;
            }
        }
    }

    public void SpawnPrefab(Vector3 spawnPosition)
    {
        spawnObject = Instantiate(objectsPrefabs[indexObject], spawnPosition, Quaternion.identity);
        objectsInScene.Add(spawnObject);
 
    }

    public void ChangeObject(int index)
    {
        indexObject = index;
    }

    public void DestroyObject()
    {
        foreach (GameObject elemento in objectsInScene)
        {
            // Faz algo com cada elemento
            Destroy(elemento);
            objectsInScene.Remove(elemento);
        }
        indexObject = -1;
    }
}





