using BattleTech;
using BattleTech.UI;
using Harmony;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace RandomNewCommanderGender
{
    public class RandomNewCommanderGender
    {
        public static void Init()
        {
            var harmony = HarmonyInstance.Create("sqparadox.TagRemover");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(SGCharacterCreationNamePanel), "LoadOptions")]
        public static class SGCharacterCreationNamePanel_LoadOptions_Patch
        {
            public static void Postfix(SGCharacterCreationNamePanel __instance)
            {
                __instance.pronounSelector.Select(new Random().Next(0, 3));
                __instance.OnPronounChanged.Invoke();
            }
        }

        public class Logger
        {
            static readonly string FilePath = "./Log.txt";
            public static void LogError(Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(FilePath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
            }

            public static void LogLine(String line)
            {
                using (StreamWriter streamWriter = new StreamWriter(FilePath, true))
                {
                    streamWriter.WriteLine(DateTime.Now.ToString() + Environment.NewLine + line + Environment.NewLine);
                }
            }
        }
    }
}
