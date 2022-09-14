using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
public class ClickableObject : MonoBehaviour, IPointerClickHandler {
    public GameObject Easter;
    public GameObject MainMenu;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Left click");
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            //Debug.Log("Middle click");
        }   
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            MainMenu.SetActive(false);
            Easter.SetActive(true);
        }

    }
}