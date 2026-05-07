using HarmonyLib;
using UnityEngine;

namespace DarkerDungeons.Patches
{
    [HarmonyPatch(typeof(EnvMan), "SetEnv")]
    public static class EnvManSetEnvPatch
    {
        private static EnvState _state;

        [HarmonyPrefix]
        private static void Prefix(EnvSetup env, ref bool __state)
        {
            __state = false;

            if (!Plugin.ModEnabled.Value)
                return;

            if (env == null)
                return;

            if (env.m_name != "Crypt")
                return;

            __state = true;

            SaveState(env);

            Plugin.LogDebug("Applying Crypt environment darkness.");

            env.m_ambColorDay *= Plugin.CryptAmbientMultiplier.Value;
            env.m_ambColorNight *= Plugin.CryptAmbientMultiplier.Value;

            env.m_fogColorDay *= Plugin.CryptFogColorMultiplier.Value;
            env.m_fogColorNight *= Plugin.CryptFogColorMultiplier.Value;
            env.m_fogColorMorning *= Plugin.CryptFogColorMultiplier.Value;
            env.m_fogColorEvening *= Plugin.CryptFogColorMultiplier.Value;

            env.m_lightIntensityDay *= Plugin.CryptLightIntensityMultiplier.Value;
            env.m_lightIntensityNight *= Plugin.CryptLightIntensityMultiplier.Value;

            env.m_fogDensityDay *= Plugin.CryptFogDensityMultiplier.Value;
            env.m_fogDensityNight *= Plugin.CryptFogDensityMultiplier.Value;
            env.m_fogDensityMorning *= Plugin.CryptFogDensityMultiplier.Value;
            env.m_fogDensityEvening *= Plugin.CryptFogDensityMultiplier.Value;
        }

        [HarmonyPostfix]
        private static void Postfix(EnvSetup env, bool __state)
        {
            if (!__state || env == null)
                return;

            RestoreState(env);
        }

        private static void SaveState(EnvSetup env)
        {
            _state.m_ambColorDay = env.m_ambColorDay;
            _state.m_ambColorNight = env.m_ambColorNight;

            _state.m_fogColorDay = env.m_fogColorDay;
            _state.m_fogColorNight = env.m_fogColorNight;
            _state.m_fogColorMorning = env.m_fogColorMorning;
            _state.m_fogColorEvening = env.m_fogColorEvening;

            _state.m_lightIntensityDay = env.m_lightIntensityDay;
            _state.m_lightIntensityNight = env.m_lightIntensityNight;

            _state.m_fogDensityDay = env.m_fogDensityDay;
            _state.m_fogDensityNight = env.m_fogDensityNight;
            _state.m_fogDensityMorning = env.m_fogDensityMorning;
            _state.m_fogDensityEvening = env.m_fogDensityEvening;
        }

        private static void RestoreState(EnvSetup env)
        {
            env.m_ambColorDay = _state.m_ambColorDay;
            env.m_ambColorNight = _state.m_ambColorNight;

            env.m_fogColorDay = _state.m_fogColorDay;
            env.m_fogColorNight = _state.m_fogColorNight;
            env.m_fogColorMorning = _state.m_fogColorMorning;
            env.m_fogColorEvening = _state.m_fogColorEvening;

            env.m_lightIntensityDay = _state.m_lightIntensityDay;
            env.m_lightIntensityNight = _state.m_lightIntensityNight;

            env.m_fogDensityDay = _state.m_fogDensityDay;
            env.m_fogDensityNight = _state.m_fogDensityNight;
            env.m_fogDensityMorning = _state.m_fogDensityMorning;
            env.m_fogDensityEvening = _state.m_fogDensityEvening;
        }

        private struct EnvState
        {
            public Color m_ambColorDay;
            public Color m_ambColorNight;

            public Color m_fogColorDay;
            public Color m_fogColorNight;
            public Color m_fogColorMorning;
            public Color m_fogColorEvening;

            public float m_lightIntensityDay;
            public float m_lightIntensityNight;

            public float m_fogDensityDay;
            public float m_fogDensityNight;
            public float m_fogDensityMorning;
            public float m_fogDensityEvening;
        }
    }
}