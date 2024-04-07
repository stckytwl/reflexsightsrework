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
                var key = jObj["key"].ToString();;
                var path = jObj["path"].ToString();
                var data = VFS.ReadFile(path);
                var crc = Crc32.Compute(data);

                var bundle = new BundleItem(key, crc, jObj["dependencyKeys"].ToObject<string[]>(), Plugin.Directory.Replace("\\", "/"));
                
                if (bundles.ContainsKey(key))
                {
                    bundles.TryRemove(key, out _);
                }
                
                bundles.TryAdd(key, bundle);
            }
        }
    }
}