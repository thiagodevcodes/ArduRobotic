using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ControlRobot: MonoBehaviour
{

    [SerializeField] ARPlaneManager planeManager;
    [SerializeField] GameObject placementIndicator;
    [SerializeField] ARRaycastManager arraycastManager;
    
    //Lista de hits e lista de objetos na cena
    List<GameObject> objectsInScene = new();
    List<ARRaycastHit> hits = new();

    //Objetos para inserir no plano
    [SerializeField] GameObject[] objectsPrefabs;

    //Lista de gameObjets de boto�es e info
    public GameObject spawnButton;
    public GameObject Joystick;
    public GameObject info;

    private int touchCount = 0;
    private readonly float doubleTouchTimeThreshold = 0.5f;
    private float lastTouchTime;

    int indexObject;

    //Camera AR
    Camera arCamera;

    //Objeto j� criado na cena
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
        //Ativa o joystick caso exista algum carro spawnado na cena
        GameObject carObject = GameObject.FindGameObjectWithTag("Car");
        Joystick.SetActive(carObject);

        //Verifica��es para ativar alguns bot�es e avisos
        spawnButton.SetActive(indexObject > 0 && OnLoadPlanes());
        placementIndicator.SetActive(OnLoadPlanes() && indexObject > 0);
        info.SetActive(indexObject > 0 && !OnLoadPlanes());

        //Chamada de fun��es para mover e destruir objetos
        MoveObject();
        DestroyObjects();
    }

    //Instanciar o objeto na cena
    public void SpawnPrefab()
    {
        //Verificar se algum plano foi carregado
        if (OnLoadPlanes())
        {
            //Instacia o objeto e adicona na lista de objetos na cena
            spawnObject = Instantiate(objectsPrefabs[indexObject], placementIndicator.transform.position, Quaternion.identity);
            objectsInScene.Add(spawnObject);
            indexObject = -1;
        }
    }

    //Modificar o objeto que ser� instanciado
    public void ChangeObject(int index)
    {
        indexObject = index;
    }

    //Mover o objeto instanciado que for tocado e arrastado
    public void MoveObject()
    {
        if (Input.touchCount == 0)
            return;

        //Cria um raio apartir do toque na tela
        Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);

        //Se o raycast for bem-sucedido (acertou algum objeto), a execu��o continua dentro deste bloco condicional.
        if (arraycastManager.Raycast(Input.GetTouch(0).position, hits))
        {
            //Verifica se o toque na tela est� na fase in�cio e se spawnObject � nulo
            if (Input.GetTouch(0).phase == TouchPhase.Began && spawnObject == null)
            {
                //Realiza um raio usando a f�sica Unity a partir do raio criado anteriormente
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    //Verifica se o objeto atingido pelo raio possui a tag "ItemSpawn".
                    if (hit.collider.gameObject.CompareTag("ItemSpawn"))
                    {
                        spawnObject = hit.collider.gameObject;
                    }
                }

            }
            //Se possui um toque de movimento ele vai alterando a posi��o do objeto capturado
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnObject != null)
            {
                spawnObject.transform.position = hits[0].pose.position;
            }

            //Assim que o objeto para de ser tocado ele se torna nulo.
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                spawnObject = null;
            }
        }
    }

    //Destruir o objeto que foi tocado duas vezes
    private void DestroyClickedObject()
    {
        Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Verificar se o objeto atingido pode ser destru�do
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
                //Conta um toque
                touchCount = 1;
            }

            lastTouchTime = currentTime;
        }
    }

    //Quando o objeto � habilitado no cen�rio ou o script � ativado
    private void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    //Quando o objeto � desabilitado no cen�rio ou o script � desativado
    private void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    //� chamado sempre que h� uma altera��o nos planos detectados
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
                return true; // Pelo menos um plano est� sendo rastreado
            }
        }
        return false; // Nenhum plano est� sendo rastreado
    }
}
