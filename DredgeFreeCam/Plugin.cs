using BepInEx;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace DredgeFreeCam
{
	[BepInPlugin("com.xen-42.dredge.freecam", "Free Camera", "0.0.1")]
	[BepInProcess("DREDGE.exe")]
	[HarmonyPatch]
	public class Plugin : BaseUnityPlugin
	{
		private void Awake()
		{
			// Plugin startup logic
			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
		}

		[HarmonyPostfix]
		[HarmonyPatch(typeof(Player), "Start")]
		public static void Player_Start(Player __instance)
		{
			var freecam = new GameObject("FreeCam");
			freecam.AddComponent<FreeCam>();
			freecam.transform.position = __instance.transform.position;
		}
	}
}
