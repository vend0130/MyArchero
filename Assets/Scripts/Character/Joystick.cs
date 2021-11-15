using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Image _joystickBG;
    [SerializeField] private Image _joystickZoneJob;
    [SerializeField] private Image _joystick;
    private Character _character;
    private Vector2 _position;
    private Vector2 _defaultPositionJoystick;

    private void Start()
    {
        _character = I.Character;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _defaultPositionJoystick = _joystickBG.transform.position;
        _joystickBG.transform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickBG.transform.position = _defaultPositionJoystick;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
        _position = Vector2.zero;
        SendDirection();
    }
    public void OnDrag(PointerEventData eventData)
    {
        JoystickCalculate(_joystickZoneJob, eventData);//for character direction
        JoystickCalculate(_joystickBG, eventData);//for ui
    }

    private void JoystickCalculate(Image image, PointerEventData eventData)
    {
        Vector2 pos;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(image.rectTransform,
            eventData.position, eventData.pressEventCamera, out pos))
        {
            Debug.LogWarning("RectTransformUtility - false");
            return;
        }

        pos.x = (pos.x / image.rectTransform.sizeDelta.x) * 2;
        pos.y = (pos.y / image.rectTransform.sizeDelta.x) * 2;
        pos = (pos.magnitude > 1.0f) ? pos.normalized : pos;

        if (image == _joystickZoneJob)
        {
            _position = (pos.magnitude >= .7f) ? pos : Vector2.zero;
            SendDirection();
        }
        else
            _joystick.rectTransform.anchoredPosition = new Vector2(pos.x * (image.rectTransform.sizeDelta.x / 2),
                pos.y * (image.rectTransform.sizeDelta.y / 2));
    }

    private void SendDirection()
    {
        if(_character.Action == ActionType.Death)
        {
            return;
        }
        Vector3 direction = Vector3.zero;
        direction.x = _position.x;
        direction.z = _position.y;
        _character.Direction = direction;
    }
}
