using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private float handleRange = 100f;

    private Vector2 input = Vector2.zero;

    public Vector2 Direction => input;
    public float Horizontal => input.x;
    public float Vertical => input.y;
    public bool IsPressed { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out pos))
            return;

        pos = Vector2.ClampMagnitude(pos, handleRange);
        handle.anchoredPosition = pos;
        input = pos / handleRange;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}
