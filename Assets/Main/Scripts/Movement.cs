using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Movement : MonoBehaviour
{

    [SerializeField] ARPlaneManager planeManager;
    [SerializeField] GameObject placementIndicator;
    [SerializeField] ARRaycastManager arraycastManager;
    
    List<GameObject> objectsInScene = new();
    List<ARRaycastHit> hits = new();

    //Objetos para inserir no plano
    [SerializeField] GameObject[] objectsPrefabs;

    public GameObject spawnButton;
    public GameObject Joystick;
    public GameObject info;

    private int touchCount = 0;
    private readonly float doubleTouchTimeThreshold = 0.5f;
    private float lastTouchTime;

    int indexObject;

    //Camera AR
    Camera arCamera;

    //Objeto já criado na cena
    GameObject spawnObject;
  
    void Start()
    {
        spawnObject = null;
        spawnButton.SetActive(false);
        arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
        indexObject = -1;
        placementIndicator.SetActive(false);
        info.SetActive(false);
    }

    void Update()
    {
        GameObject carObject = GameObject.FindGameObjectWithTag("Car");
        Joystick.SetActive(carObject);

        spawnButton.SetActive(indexObject > 0 && OnLoadPlanes());
        placementIndicator.SetActive(OnLoadPlanes() && indexObject > 0);
        info.SetActive(indexObject > 0 && !OnLoadPlanes());

        MovimentObject();
        DestroyObjects();
    }

    //Gerar o objeto
    public void SpawnPrefab()
    {
        if (OnLoadPlanes())
        {
            spawnObject = Instantiate(objectsPrefabs[indexObject], placementIndicator.transform.position, Quaternion.identity);
            objectsInScene.Add(spawnObject);
            indexObject = -1;

            if (spawnObject.CompareTag("Car"))
            {
                Joystick.SetActive(true);
            }
        }
    }

    //Modificar o objeto
    public void ChangeObject(int index)
    {
        indexObject = index;
    }

    //Mover o objeto
    public void MovimentObject()
    {

        if (Input.touchCount == 0)
            return;

        Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);

        if (arraycastManager.Raycast(Input.GetTouch(0).position, hits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && spawnObject == null)
            {
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag("ItemSpawn"))
                    {
                        spawnObject = hit.collider.gameObject;

                    }
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

    //Destruir o objeto 
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

    //Verificar se houve dois cliques para remover o objeto
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

    private void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added.Count > 0 || args.updated.Count > 0)
        {
            // Pelo menos um plano foi adicionado ou atualizado
            Debug.Log("Plano carregado.");
        }
    }

    //Verificar se algum plano foi carregado
    private bool OnLoadPlanes()
    {
        foreach (var plane in planeManager.trackables)
        {
            if (plane.trackingState == TrackingState.Tracking)
            {
                return true; // Pelo menos um plano está sendo rastreado
            }
        }
        return false; // Nenhum plano está sendo rastreado
    }
}





