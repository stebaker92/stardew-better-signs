using StardewModdingAPI;
using StardewValley;

namespace BetterSigns;

/// <summary>
/// Harmony patches for overwriting existing Stardew methods
/// </summary>
public class SignPatcher
{
	private static IMonitor Monitor;

	public static void Initialize(IMonitor monitor)
	{
		Monitor = monitor;
	}

	public static bool checkForAction_Prefix(StardewValley.Object __instance, Farmer who, bool justCheckingForActivity, ref bool __result)
	{
		try
		{
			if (__instance is StardewValley.Objects.Sign sign && sign.displayItem.Value != null)
			{
				// don't run original logic, aka overwriting the displayItem on a Sign
				return false; 
			}

			// run original logic
			return true; 
		}
		catch (Exception ex)
		{
			Monitor.Log($"Failed in {nameof(checkForAction_Prefix)}:\n{ex}", LogLevel.Error);

			// run original logic instead of crashing the game / mod
			return true; 
		}
	}
}
