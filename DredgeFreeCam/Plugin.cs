using BepInEx;

namespace DredgeFreeCam
{
	[BepInPlugin("com.xen-42.dredge.freecam", "Free Camera", "0.0.1")]
	[BepInProcess("DREDGE.exe")]
	public class Plugin : BaseUnityPlugin
	{
		private void Awake()
		{
			// Plugin startup logic
			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
		}
	}
}
