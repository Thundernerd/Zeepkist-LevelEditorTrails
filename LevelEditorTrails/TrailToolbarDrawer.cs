using Imui.Controls;
using Imui.Core;
using ZeepSDK.UI;

namespace TNRD.Zeepkist.LevelEditorTrails;

internal class TrailToolbarDrawer : IZeepToolbarDrawer
{
    public string MenuTitle => "Trails";

    public void DrawMenuItems(ImGui gui)
    {
        PluginConfig.LinesVisible.Value = gui.Checkbox(PluginConfig.LinesVisible.Value, "Lines visible");
        PluginConfig.TimeMarkersVisible.Value =
            gui.Checkbox(PluginConfig.TimeMarkersVisible.Value, "Time markers visible");

        gui.Separator();

        if (gui.Menu("Clear trails"))
        {
            TrailStorage.SaveForCurrentLevel([]);
            TrailManager.LoadTrails([]);
        }
    }
}