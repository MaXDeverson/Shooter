using UnityEngine;
using Zenject;

public class SceneLoader : MonoInstaller
{
    [Header("Input Sytem")]
    [SerializeField] private PCInputSystem _pcInputSystem;
    [SerializeField] private MobileInputSystem _mobileInputSystem;
    [SerializeField] private bool _isPCInput;
    [Header("Game Play Stuff")]
    [SerializeField] private CharacterManager _playerCharacterManager;
    [SerializeField] private CameraController _camera;
    [SerializeField] private UIController _ui;
    public override void InstallBindings()
    {
        Container.Bind<InputSystem>().FromInstance((_isPCInput?(InputSystem) _pcInputSystem:(InputSystem) _mobileInputSystem)).AsSingle().NonLazy();
        Container.Bind<CameraController>().FromInstance(_camera).AsSingle().NonLazy();
        Container.Bind<UIController>().FromInstance(_ui).AsSingle().NonLazy();
        Container.Bind<CharacterManager>().FromInstance(_playerCharacterManager).AsSingle().NonLazy();
    }
}
