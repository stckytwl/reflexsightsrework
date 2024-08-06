using System.Linq;
using System.Reflection;
using Comfort.Common;
using EFT;
using SPT.Reflection.Patching;
using UnityEngine;

namespace SamSWAT.ReflexSightsRework
{
    public class Patch : ModulePatch
    {
        public static float TotalErgonomics;
        public static float Overweight;
        private static Transform _currentWeapon;
        private static SightSwitch[] _instances;

        protected override MethodBase GetTargetMethod()
        {
            return typeof(Player.FirearmController).GetMethod("set_IsAiming", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostfix(Player.FirearmController __instance, bool value)
        {
            if (__instance == null) return;

            if (_currentWeapon != __instance.WeaponRoot || _instances.Any(x => x.gameObject.activeSelf == false))
            {
                _currentWeapon = __instance.WeaponRoot;
                _instances = _currentWeapon.GetComponentsInChildren<SightSwitch>();
            }

            if (_instances.Length == 0) return;

            var player = Singleton<GameWorld>.Instance.MainPlayer;

            TotalErgonomics = __instance.TotalErgonomics;
            Overweight = player.ProceduralWeaponAnimation.Overweight;

            foreach (var instance in _instances)
            {
                instance.enabled = value;
            }
        }
    }
}