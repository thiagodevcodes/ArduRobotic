using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehind : MonoBehaviour, IPointerDownHandler
{
    private bool isButtonSelected = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonSelected = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    private void Update()
    {
        if (isButtonSelected && Input.GetMouseButtonDown(0))
        {
            // Impede que outros objetos recebam eventos de clique enquanto o botão estiver selecionado
            EventSystem.current.SetSelectedGameObject(null);
            isButtonSelected = false;
        }
    }
}



