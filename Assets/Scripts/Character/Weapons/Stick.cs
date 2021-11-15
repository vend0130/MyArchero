using UnityEngine;

public class Stick : Weapon
{
    [SerializeField] private GameObject _prefabBall;
    [SerializeField] private Transform _point;
    private MagicBall[] _balls = new MagicBall[30];
    private int _currentIndex;
    private float _time;

    private void Start()
    {
        for (int i = 0; i < _balls.Length; i++)
        {
            _balls[i] = Instantiate(_prefabBall).GetComponent<MagicBall>();
            _balls[i].gameObject.SetActive(false);
        }
    }

    public override void Attack()
    {
        if (_time > Time.time || I.CharacterAttack.NearEnemy == null)
            return;

        _time = Time.time + _speedAttack;

        SpawnBall();
    }
    public void SpawnBall()
    {
        _balls[_currentIndex].transform.position = _point.position;
        Vector3 target = I.CharacterAttack.NearEnemy.transform.position - _balls[_currentIndex].transform.position;
        target.y = transform.position.y;
        _balls[_currentIndex].transform.rotation = Quaternion.LookRotation(target);
        _balls[_currentIndex].gameObject.SetActive(true);
        _currentIndex++;
        _currentIndex = _currentIndex >= _balls.Length ? 0 : _currentIndex;
    }
}
