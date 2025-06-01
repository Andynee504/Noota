using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
   public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCameras;

    [SerializeField] private float _fallPanAmount = 0.2f;
    [SerializeField] private float _fallYPanTime = 0.3f;
    public float _fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping {  get; private set; }

    public bool LerpedFromPlayerFalling { get; set; }
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }
}