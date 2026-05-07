// File: Plugin.cs

using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace DarkerDungeons
{

    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string ModGuid = "petri.valheim.darkerdungeons";
        public const string ModName = "Darker Dungeons";
        public const string ModVersion = "1.0.0";

        internal static ManualLogSource Log;

        internal static ConfigEntry<bool> ModEnabled;
        internal static ConfigEntry<bool> DebugLogging;
        internal static ConfigEntry<float> CryptAmbientMultiplier;
        internal static ConfigEntry<float> CryptFogColorMultiplier;
        internal static ConfigEntry<float> CryptLightIntensityMultiplier;
        internal static ConfigEntry<float> CryptFogDensityMultiplier;

        private Harmony _harmony;

        private void Awake()
        {
            ModEnabled = Config.Bind(
                "General",
                "Enabled",
                true,
                "Enable or disable Darker Dungeons."
            );

            DebugLogging = Config.Bind(
                "General",
                "Debug Logging",
                false,
                "Enable debug logging for Darker Dungeons."
            );

            CryptAmbientMultiplier = Config.Bind(
                "Crypt",
                "Ambient Multiplier",
                0.15f,
                new ConfigDescription(
                    "Ambient brightness multiplier inside crypts.",
                    new AcceptableValueRange<float>(0.0f, 1.5f)
                )
            );

            CryptFogColorMultiplier = Config.Bind(
                "Crypt",
                "Fog Color Multiplier",
                0.35f,
                new ConfigDescription(
                    "Fog color brightness multiplier inside crypts.",
                    new AcceptableValueRange<float>(0.0f, 1.5f)
                )
            );

            CryptLightIntensityMultiplier = Config.Bind(
                "Crypt",
                "Light Intensity Multiplier",
                0.30f,
                new ConfigDescription(
                    "Environment light intensity multiplier inside crypts.",
                    new AcceptableValueRange<float>(0.0f, 1.5f)
                )
            );

            CryptFogDensityMultiplier = Config.Bind(
                "Crypt",
                "Fog Density Multiplier",
                1.50f,
                new ConfigDescription(
                    "Fog density multiplier inside crypts.",
                    new AcceptableValueRange<float>(0.1f, 3.0f)
                )
            );

            Log = Logger;

            _harmony = new Harmony(ModGuid);
            _harmony.PatchAll();

            Log.LogInfo($"{ModName} {ModVersion} loaded.");
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }

        internal static void LogDebug(string message)
        {
            if (DebugLogging.Value)
            {
                Log.LogInfo($"[Darker Dungeons]: {message}");
            }
        }
    }
}