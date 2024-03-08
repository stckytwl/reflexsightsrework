using System.Linq;
using System.Reflection;
using Aki.Reflection.Patching;
using EFT;
using EFT.Animations;
using UnityEngine;
using HarmonyLib;
using System;

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
            return typeof(EFT.Player.FirearmController).GetMethod("set_IsAiming", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostfix(EFT.Player.FirearmController __instance, bool value)
        {
            if (__instance == null) return;

            if (_currentWeapon != __instance.WeaponRoot || _instances.Any(x => x.gameObject.activeSelf == false))
            {
                _currentWeapon = __instance.WeaponRoot;
                _instances = _currentWeapon.GetComponentsInChildren<SightSwitch>();
            }

            if (_instances.Length == 0) return;
            
            TotalErgonomics = __instance.TotalErgonomics;

            // Yoinked from Fontaine's Realism plugin
            Player player = (Player)AccessTools.Field(typeof(EFT.Player.ItemHandsController), "_player").GetValue(__instance);

            TotalErgonomics = __instance.TotalErgonomics;
            Overweight = player.ProceduralWeaponAnimation.Overweight;

            foreach (var instance in _instances) instance.enabled = value;
        }
    }
}