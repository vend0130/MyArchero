using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _hpBarPrefab;
    [SerializeField] private Transform _UIhpPanel;
    [SerializeField] private GameObject _panelSelectWeapon;
    [SerializeField] private GameObject _panelGameplay;
    [SerializeField] private RectTransform _rectTransformProgress;
    [SerializeField] private GameObject _panelEnd;
    [SerializeField] private Text _textEnd;
    [SerializeField] private Canvas _canvas;

    public GameObject HpBarPrafab => _hpBarPrefab;
    public Transform UIhpPanel => _UIhpPanel;
    public float CanvasScale => _canvas.scaleFactor;

    private void Awake()
    {
        I.Ui = this;
        _panelEnd.SetActive(false);
        _panelGameplay.SetActive(true);
        _panelSelectWeapon.SetActive(true);
        Time.timeScale = 0;
    }
    public void EndGame()
    {
        _textEnd.text = "END GAME";
        _panelGameplay.SetActive(false);
        _panelEnd.SetActive(true);
    }

    public void BtnLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BtnSelectWeapon(int value)
    {
        I.CharacterAttack.SetActiveWeapon(value);
        _panelSelectWeapon.SetActive(false);
        _panelGameplay.SetActive(true);
        Time.timeScale = 1;
    }

    public void UpdateProgress(int maxEnemy, int currentEnemy)
    {
        Vector3 rectScale = _rectTransformProgress.localScale;
        rectScale.x = (float)currentEnemy / (float)maxEnemy;
        _rectTransformProgress.localScale = rectScale;
    }
}
