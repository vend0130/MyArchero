using UnityEngine;
using UnityEngine.UI;

public class UIhp : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private GameObject _hpObject;
    [SerializeField] private RectTransform[] _hpBar;
    [SerializeField] private Text _textHp;
    Canvas canvas;

    private Who _who;
    private Camera _camera;

    public void Init(Who who, Vector3 position)
    {
        _camera = Camera.main;
        SetWho(who);
        SetPosition(position);
    }

    public void SetWho(Who value)
    {
        _who = value;
        if (_who == Who.Characte)
            _hpBar[0].gameObject.SetActive(false);
        else
        {
            _textHp.gameObject.SetActive(false);
            _hpBar[1].gameObject.SetActive(false);
            _hpObject.SetActive(false);
        }
    }

    public void SetPosition(Vector3 position)
    {
        Vector2 screenPoints = _camera.WorldToScreenPoint(position + Vector3.forward * 1.5f);
        screenPoints.x = -(Screen.width / 2) + screenPoints.x;
        screenPoints.y = -(Screen.height / 2) + screenPoints.y;
        screenPoints /= I.Ui.CanvasScale;
        _rectTransform.anchoredPosition = screenPoints;
    }

    public void UpdateHpBar(int hpmax, int hpcurrent)
    {
        if(hpcurrent == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        if (!_hpObject.activeSelf)
            _hpObject.SetActive(true);

        Vector3 scale = _hpBar[(int)_who].localScale;
        scale.x = (float)hpcurrent / (float)hpmax;
        _hpBar[(int)_who].localScale = scale;

        if (_who == Who.Characte)
            _textHp.text = hpcurrent.ToString();
    }
}
