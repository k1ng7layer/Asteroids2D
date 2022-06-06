using Assets.Scripts.Actions;
using Assets.Scripts.Configuration;
using Assets.Scripts.Runtime;
using Assets.Scripts.Runtime.UI;
using MyECS2;
using UnityEngine;

public class GameRunner : MonoBehaviour
{
    [SerializeField] private RootConfiguration _rootConfiguration;
    [SerializeField] private GameObject _asteroidsSpawnPoint;
    [SerializeField] private GameObject _ufosSpawnPoint;
    [SerializeField] private Camera _mainCamera;
    private UIPresenter uI;
    private SystemRunner _systemRunner;


    private void Awake()
    {
        EntityHandler entityHandler = new EntityHandler();
        uI = new UIPresenter(_rootConfiguration.UI);
        SystemConfiguration systemConfiguration = new SystemConfiguration(_rootConfiguration, uI, _asteroidsSpawnPoint, _ufosSpawnPoint, entityHandler);
        _systemRunner = new SystemRunner(_rootConfiguration, systemConfiguration);

    }
        

    void Start()
    {
        Time.timeScale = 1f;
        ActionConfig.ConfigureActions();
        uI.Initialize();
        _systemRunner.Init();
    }


    void Update()
    {
        _systemRunner.Update();
        uI.OnUpdate();
    }
    private void LateUpdate()
    {
        _systemRunner.LateUpdate();
    }
    private void OnDestroy()
    {
        _systemRunner.Destroy();
        uI.OnDestroy();
    }
}