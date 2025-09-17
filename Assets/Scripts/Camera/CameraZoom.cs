using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float defaultDistance = 4f;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float maxDistance = 7f;

    [SerializeField] private float smoothing = 4f;
    [SerializeField] [Range(0f,10f)] private float zoomSensitivity = 1f;
    
    private CinemachineFramingTransposer _framingTransposer;
    private CinemachineInputProvider _inputProvider;
    
    private float _currentTargetDistance;

    private void Awake()
    {
        _framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        _inputProvider = GetComponent<CinemachineInputProvider>();
        
        _currentTargetDistance = defaultDistance;
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float zoomValue = _inputProvider.GetAxisValue(2) * zoomSensitivity;
        _currentTargetDistance = Mathf.Clamp(_currentTargetDistance + zoomValue, minDistance, maxDistance);

        float currentDistance = _framingTransposer.m_CameraDistance;
        if (currentDistance == _currentTargetDistance) return;
        _framingTransposer.m_CameraDistance = Mathf.Lerp(currentDistance, _currentTargetDistance, smoothing * Time.deltaTime);
    }
}
