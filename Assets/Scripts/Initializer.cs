using BattleSystem;
using ProgressSystem;
using UI;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    private UISystem uiSystem;
    private BattleManager battleManager;
    private ProgressManager progressManager;
    
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
            
        progressManager = new ProgressManager();

        uiSystem = new UISystem(progressManager);
        
        battleManager = new BattleManager();
    }

    void Update()
    {
        var deltaTime = Time.deltaTime;
        uiSystem.UpdateFrame(deltaTime);
        battleManager.UpdateFrame(deltaTime);
    }
}