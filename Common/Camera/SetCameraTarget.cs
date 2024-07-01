using Cinemachine;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class SetCameraTarget : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _folowCamera;

    private Transform _playerTransform;

    [Inject]
    private void Construct(PlayerStateMachine player)
    {
        _playerTransform = player.transform;
    }

    private void Start()
    {
        _folowCamera.Follow = _playerTransform;
    }

    private void Update()
    {
       
    }
}
