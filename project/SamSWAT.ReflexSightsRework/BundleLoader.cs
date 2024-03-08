using System.Reflection;
using Aki.Common.Utils;
using Aki.Custom.Models;
using Aki.Custom.Utils;
using Aki.Reflection.Patching;
using Newtonsoft.Json.Linq;

namespace SamSWAT.ReflexSightsRework
{
    public class BundleLoader : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BundleManager).GetMethod(nameof(BundleManager.GetBundles));
        }

        [PatchPostfix]
        private static void PatchPostfix()
        {
            var json = VFS.ReadTextFile(Plugin.Directory + "bundles.json");
            var jArray = JArray.Parse(json);
            var bundles = BundleManager.Bundles;

            foreach (var jObj in jArray)
            {
                var key = jObj["key"].ToString();
                var path = jObj["path"].ToString();
                var bundle = new BundleInfo(key, path, jObj["dependencyKeys"].ToObject<string[]>());
                
                if (bundles.ContainsKey(key))
                    bundles.Remove(key);
                
                bundles.Add(key, bundle);
            }
        }
    }
}