using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    Vector2 startPosition;
    Vector3 point;

    public Camera cam;

    public int towerSlot;
    public string toweSlot_String;


    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    BuildManager buildManager;

    private void Awake()
    {
        towerSlot = PlayerPrefs.GetInt(toweSlot_String);
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        buildManager = GameObject.Find("Panel_SwitchMode").GetComponent<BuildManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = GetComponent<RectTransform>().anchoredPosition;
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        point = cam.ScreenToWorldPoint(new Vector3(transform.position.x, transform.position.y, 10));
        point.y = 2.5f;
        buildManager.Build(point, towerSlot);
        rectTransform.anchoredPosition = startPosition;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

}
