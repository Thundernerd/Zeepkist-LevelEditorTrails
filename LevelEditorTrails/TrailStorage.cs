using System.Collections.Generic;
using System.IO;
using TNRD.Zeepkist.LevelEditorTrails.Patches;
using ZeepSDK.Storage;

namespace TNRD.Zeepkist.LevelEditorTrails;

internal static class TrailStorage
{
    private static IModStorage _storage;
    private static string _currentLevelName;

    public static void Initialize(Plugin plugin)
    {
        _storage = StorageApi.CreateModStorage(plugin);
        
        LEV_SaveLoad_ExternalLoad.Postfixed += OnExternalLoad;
        LEV_SaveLoad_SaveFile.Postfixed += OnSaveFile;
    }

    private static void OnExternalLoad(string filename)
    {
        filename = Path.GetFileNameWithoutExtension(filename);
        if (filename == "TestLevel_Test")
            return;

        _currentLevelName = filename;
    }

    private static void OnSaveFile(string filename)
    {
        _currentLevelName = Path.GetFileNameWithoutExtension(filename);
    }

    public static void SaveForCurrentLevel(List<Trail> trails)
    {
        if (_currentLevelName == null)
            return;

        _storage.SaveToJson(_currentLevelName, trails);
    }

    public static List<Trail> LoadForCurrentLevel()
    {
        if (_currentLevelName == null)
            return [];

        return _storage.JsonFileExists(_currentLevelName)
            ? _storage.LoadFromJson<List<Trail>>(_currentLevelName)
            : [];
    }
}