using System;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using TNRD.Zeepkist.LevelEditorTrails.Patches;
using UnityEngine;
using ZeepSDK.LevelEditor;
using ZeepSDK.Racing;
using ZeepSDK.Storage;
using ZeepSDK.UI;

namespace TNRD.Zeepkist.LevelEditorTrails
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("ZeepSDK", "1.44.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static IModStorage Storage { get; private set; }
        
        private readonly List<Action> _actions = [];

        private Harmony _harmony;
        private bool _isTesting;

        private void Awake()
        {
            _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            _harmony.PatchAll();

            PluginConfig pluginConfig = gameObject.AddComponent<PluginConfig>();
            pluginConfig.Initialize(this);
            TrailStorage.Initialize(this);
            Storage = StorageApi.CreateModStorage(this);
            UIApi.AddToolbarDrawer(new TrailToolbarDrawer());

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void Start()
        {
            LevelEditorApi.EnteredLevelEditor += OnEnteredLevelEditor;
            LevelEditorApi.ExitedLevelEditor += OnExitedLevelEditor;
            LevelEditorApi.EnteredTestMode += OnEnteredTestMode;
            LevelEditorApi.LevelLoaded += OnLevelLoaded;
            LevelEditorApi.LevelSaved += OnLevelSaved;
            RacingApi.PlayerSpawned += OnPlayerSpawned;
        }

        private void OnDestroy()
        {
            LevelEditorApi.EnteredLevelEditor -= OnEnteredLevelEditor;
            LevelEditorApi.ExitedLevelEditor -= OnExitedLevelEditor;
            LevelEditorApi.EnteredTestMode -= OnEnteredTestMode;
            LevelEditorApi.LevelLoaded -= OnLevelLoaded;
            LevelEditorApi.LevelSaved -= OnLevelSaved;
            RacingApi.PlayerSpawned -= OnPlayerSpawned;

            _harmony?.UnpatchSelf();
        }

        private void ExecuteNextFrame(Action action)
        {
            _actions.Add(action);
        }

        private void Update()
        {
            List<Action> clone = new List<Action>(_actions);
            _actions.Clear();
            
            foreach (Action action in clone)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private void OnLevelLoaded()
        {
            ExecuteNextFrame(() =>
            {
                TrailManager.LoadTrails(TrailStorage.LoadForCurrentLevel());
                TrailManager.CreateTrailRenderers();
            });
        }
        
        private void OnLevelSaved()
        {
            ExecuteNextFrame(() => { TrailStorage.SaveForCurrentLevel(TrailManager.LoadedTrails); });
        }
        
        private void OnEnteredLevelEditor()
        {
            _isTesting = false;
            TrailManager.CreateTrailRenderers();
        }
        
        private void OnExitedLevelEditor()
        {
            if (LEV_ReturnToMainMenu_ReturnToMainMenu.IsLeavingToMainMenu)
            {
                TrailManager.DestroyTrailRenderers();
            }
        }
        
        private void OnEnteredTestMode()
        {
            _isTesting = true;
            TrailManager.DestroyTrailRenderers();
        }
        
        private void OnPlayerSpawned()
        {
            if (!_isTesting)
                return;
        
            GameObject soapbox = GameObject.Find("Soapbox(Clone)");
            soapbox.AddComponent<TrailRecorder>();
        }
    }
}
