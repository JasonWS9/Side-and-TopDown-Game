using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public CinemachineCamera topDownCamera;
    public CinemachineCamera sideCamera;
    private CinemachineCamera currentCamera;

    public static CameraManager Instance;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShiftCamera();
    }

    private void OnEnable()
    {
        GameManager.OnPerspectiveShift += ShiftCamera;
    }
    private void OnDisable()
    {
        GameManager.OnPerspectiveShift -= ShiftCamera;
    }
    void ShiftCamera()
    {
        if (GameManager.Instance.currentState == GameManager.CameraState.TopDown)
        {
            sideCamera.enabled = false;
            topDownCamera.enabled = true;
            currentCamera = topDownCamera;
        }
        else 
        {
            sideCamera.enabled = true;
            topDownCamera.enabled = false;
            currentCamera = sideCamera;
        }
    }
}
