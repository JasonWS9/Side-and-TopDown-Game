using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public CinemachineCamera topDownCamera;
    public CinemachineCamera sideCamera;
   
    private CinemachineCamera currentCamera;

    public static CameraManager Instance;

    private Camera mainCam;

    [SerializeField] private CinemachineBrain cinemachineBrain;

    private bool canChangeCamera = true;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (topDownCamera.enabled == false)
        {
            ShiftCamera();
        }
  
    }

    private void OnEnable()
    {
        PerspectiveManager.OnPerspectiveShift += ShiftCamera;
    }
    private void OnDisable()
    {
        PerspectiveManager.OnPerspectiveShift -= ShiftCamera;
    }
    void ShiftCamera()
    {
        if (PerspectiveManager.Instance.currentState == PerspectiveManager.PerspectiveState.TopDown)
        {
            topDownCamera.Lens.ModeOverride = LensSettings.OverrideModes.Orthographic;
            topDownCamera.enabled = true;
            sideCamera.enabled = false;
            currentCamera = topDownCamera;
        }
        else
        {
            sideCamera.Lens.ModeOverride = LensSettings.OverrideModes.Perspective;

            sideCamera.enabled = true;
            topDownCamera.enabled = false;
            currentCamera = sideCamera;
        }
        StartCoroutine(CheckCameraTransition());
    }
    private IEnumerator CheckCameraTransition()
    {
        PlayerMovement.Instance.movementEnabled = false;
        PerspectiveManager.Instance.canChangePerspective = false;
        yield return null;
        while (cinemachineBrain.IsBlending)
        {
            yield return null;
        }
        PlayerMovement.Instance.movementEnabled = true;
        PerspectiveManager.Instance.canChangePerspective = true;

        sideCamera.Lens.ModeOverride = LensSettings.OverrideModes.Orthographic;
        topDownCamera.Lens.ModeOverride = LensSettings.OverrideModes.Perspective;
    }
}
