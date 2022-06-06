using AsteroidsECS;
using MyActionContainer;
using MyUISystem;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] UIConfiguration _uIConfiguration;
    private UIPresenter uIPresenter;
    private void Awake()
    {
       uIPresenter = new UIPresenter(_uIConfiguration);
    }
    void Start()
    {
        ActionConfig.ConfigureActions();
        uIPresenter.Initialize();
    }

    
    void Update()
    {
        uIPresenter.OnUpdate();
    }
    private void OnDestroy()
    {
        uIPresenter.OnDestroy();
    }
}
