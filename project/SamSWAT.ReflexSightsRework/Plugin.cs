using System.Reflection;
using Aki.Common.Utils;
using BepInEx;

namespace SamSWAT.ReflexSightsRework
{
    [BepInPlugin("com.samswat.reflexsightsrework", "SamSWAT.ReflexSightsRework", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static string Directory { get; private set; }

        private void Awake()
        {
            Directory = Assembly.GetExecutingAssembly().Location.GetDirectory() + @"\";

            new BundleLoader().Enable();
            new Patch().Enable();
        }
    }
}