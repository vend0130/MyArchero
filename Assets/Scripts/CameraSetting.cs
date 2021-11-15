using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private const float DEFAULT_WIDTH = 720;
    private const float DEFAULT_HEIGHT = 1280;
    private const float DEFAULT_SIZE_CAMERA = 7;

    private void Awake()
    {
        float number = (DEFAULT_WIDTH / DEFAULT_HEIGHT) * DEFAULT_SIZE_CAMERA;
        float number2 = (float)Screen.width / (float)Screen.height;
        number /= number2;
        _camera.orthographicSize = number;
    }
}
