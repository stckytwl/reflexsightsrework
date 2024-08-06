using BepInEx;

namespace SamSWAT.ReflexSightsRework
{
    [BepInPlugin("com.samswat.reflexsightsrework", "SamSWAT.ReflexSightsRework", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            new Patch().Enable();
        }
    }
}