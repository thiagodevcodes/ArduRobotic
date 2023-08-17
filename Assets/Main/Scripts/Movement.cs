using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Movement : MonoBehaviour
{
    [SerializeField]
    ARPlaneManager planeManager;

    [SerializeField]
    GameObject placementIndicator;

    List<GameObject> objectsInScene = new();

    //Detectar a colisão
    [SerializeField]
    ARRaycastManager arraycastManager;

    //Lista de colisões
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //Objetos para inserir no plano

    [SerializeField]
    GameObject[] objectsPrefabs;

    public GameObject Joystick;

    private int touchCount = 0;
    private float doubleTouchTimeThreshold = 0.5f;
    private float lastTouchTime;

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
        placementIndicator.SetActive(false);
    }

    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Car"))
        {
            Joystick.SetActive(true);
        } else
        {
            Joystick.SetActive(false);
        }

        MovimentObject();
        DestroyObjects();
    }

    public void DestroyObjects()
    {
        // Verificar se houve um toque duplo
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            float currentTime = Time.time;
            if (currentTime - lastTouchTime <= doubleTouchTimeThreshold)
            {
                // Um toque duplo ocorreu
                DestroyClickedObject();
                touchCount = 0;
            }
            else
            {
                touchCount = 1;
            }

            lastTouchTime = currentTime;
        }
    }

    public void MovimentObject()
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
                    if (hit.collider.gameObject.CompareTag("ItemSpawn"))
                    {
                        spawnObject = hit.collider.gameObject;

                    }
                    /*
                    else 
                    {
                        SpawnPrefab(hits[0].pose.position);
                    }
                    */
                }

            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnObject != null)
            {
                spawnObject.transform.position = hits[0].pose.position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                spawnObject = null;
            }
        }
    }

    public void SpawnPrefab(/*Vector3 spawnPosition*/)
    {
        if(placementIndicator.activeInHierarchy)
        {
            spawnObject = Instantiate(objectsPrefabs[indexObject], placementIndicator.transform.position, placementIndicator.transform.rotation);
            //spawnObject = Instantiate(objectsPrefabs[indexObject], placementIndicator.transform.position, Quaternion.Euler(0f, 0f, 0f));
            objectsInScene.Add(spawnObject);
            placementIndicator.SetActive(false);

            if (spawnObject.CompareTag("Car"))
            {
                Joystick.SetActive(true);
            }
        }
    }

    public void ChangeObject(int index)
    {
        indexObject = index;
        placementIndicator.SetActive(true);
    }

    private void DestroyClickedObject()
    {
        Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Verificar se o objeto atingido pode ser destruído
            if (hit.transform != null && (hit.transform.CompareTag("ItemSpawn") || hit.transform.CompareTag("Car")))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}





