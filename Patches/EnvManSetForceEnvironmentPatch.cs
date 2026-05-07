using HarmonyLib;

namespace DarkerDungeons.Patches
{
    [HarmonyPatch(typeof(EnvMan), nameof(EnvMan.SetForceEnvironment))]
    public static class EnvManSetForceEnvironmentPatch
    {
        private static string _lastEnv;

        [HarmonyPostfix]
        private static void Postfix(string env)
        {
            if (_lastEnv == env)
                return;

            _lastEnv = env;
            Plugin.LogDebug($"Forced environment changed to: {env}");
        }
    }
}