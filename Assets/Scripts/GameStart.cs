using Assets.Scripts.Actions;
using Assets.Scripts.Runtime.UI;
using MyUISystem;
using System.Collections;
using System.Collections.Generic;
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
