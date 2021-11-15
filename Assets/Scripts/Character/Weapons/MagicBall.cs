using UnityEngine;

public class MagicBall : MonoBehaviour
{
    private int _speed = 10;

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
            gameObject.SetActive(false);
    }
}
