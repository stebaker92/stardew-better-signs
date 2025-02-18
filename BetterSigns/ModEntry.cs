using HarmonyLib;
using StardewModdingAPI;

namespace BetterSigns
{
	public class ModEntry : Mod
	{
		internal Config config;
		internal ITranslationHelper i18n => Helper.Translation;

		public override void Entry(IModHelper helper)
		{
			string startingMessage = i18n.Get("template.start", new { mod = helper.ModRegistry.ModID, folder = helper.DirectoryPath });
			Monitor.Log(startingMessage, LogLevel.Trace);

			config = helper.ReadConfig<Config>();

			var harmony = new Harmony(this.ModManifest.UniqueID);

			harmony.Patch(
			   original: AccessTools.Method(typeof(StardewValley.Objects.Sign), nameof(StardewValley.Objects.Sign.checkForAction)),
			   prefix: new HarmonyMethod(typeof(SignPatcher), nameof(SignPatcher.checkForAction_Prefix))
			);

			harmony.Patch(
			   original: AccessTools.Method(typeof(StardewValley.Object), nameof(StardewValley.Object.checkForAction)),
			   prefix: new HarmonyMethod(typeof(ObjectPatcher), nameof(ObjectPatcher.checkForAction_Prefix))
			);
		}
	}
}
