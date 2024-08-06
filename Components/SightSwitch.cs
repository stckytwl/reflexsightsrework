using System.Collections;
using EFT;
using UnityEngine;

namespace SamSWAT.ReflexSightsRework
{
    public class SightSwitch : MonoBehaviour
    {
#pragma warning disable CS0649
        [SerializeField] private GameObject normalMesh;
#pragma warning disable CS0649
        [SerializeField] private GameObject adsMesh;

        private void OnEnable()
        {
            var delay = Patch.Overweight > 0 || Patch.TotalErgonomics < 30 ? 0.12f : 0f;
            StaticManager.BeginCoroutine(DelayCoroutine(delay));
        }

        private void OnDisable()
        {
            SwitchMeshes();
        }

        private void SwitchMeshes()
        {
            if (normalMesh == null) return;
            normalMesh.SetActive(!normalMesh.activeSelf);
            adsMesh.SetActive(!adsMesh.activeSelf);
        }

        private IEnumerator DelayCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            SwitchMeshes();
        }
    }
}