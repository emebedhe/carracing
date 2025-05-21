using UnityEngine;
using UnityEngine.EventSystems;

public class buttonscript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string buttonName;
    public string voids = "";

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (buttonName == "track1")
        {
            voids = "track1";
        }
        else if (buttonName == "track2")
        {
            voids = "track2";

        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        voids = "";
    }
}
