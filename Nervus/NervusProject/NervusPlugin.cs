using BepInEx;
using HarmonyLib;
using SpaceWarp;
using KSP.Modules;
using SpaceWarp.API.Mods;

namespace Nervus;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(SpaceWarpPlugin.ModGuid, SpaceWarpPlugin.ModVer)]
public class NervusPlugin : BaseSpaceWarpPlugin
{
    public const string ModGuid = MyPluginInfo.PLUGIN_GUID;
    public const string ModName = MyPluginInfo.PLUGIN_NAME;
    public const string ModVer = MyPluginInfo.PLUGIN_VERSION;

    public static NervusPlugin Instance { get; set; }


    public void Log(string text)
    {
        var logger = BepInEx.Logging.Logger.CreateLogSource("NervusLog");
        logger.LogInfo(text);
        return;
    }
    /*public bool FindPart(string partName, bool found)
    {
        var part = Data_Engine.Game.Parts._partData.Values
        .Where(item => item?.data?.partName == partName).FirstOrDefault();
        if (part != null)
        {
            part.data.oabEditorCategory = KSP.OAB.OABEditorPartCategory.VAB;
            Log("Found " + partName + "!");
            found = true;
        } else 
        {
            Log("Could not find " + partName + ".");
            found = false;   
        }
        return found;
    }*/
    public override void OnInitialized()
    {
        int i = 0;
        bool found = false;
        var stop = false;
        Instance = this;
        base.OnInitialized();
        string[] parts = { "engine_2v_hydrogen_nervus", "claw_1v_advanced", "strut_2v_heavy_extensible", "parachute_1v_eva", "factory_2v_methalox-monoprop" };
        Task.Run(() =>
        {
            while (!stop) {
                try
                {
                    {

                        for (i = 0; i < parts.Length; i++)
                        {
                            string partName = parts[i];
                            var part = Data_Engine.Game.Parts._partData.Values
                            .Where(item => item?.data?.partName == partName).FirstOrDefault();
                            if (part != null)
                            {
                                part.data.oabEditorCategory = KSP.OAB.OABEditorPartCategory.VAB;
                                Log("Found " + partName + "!");
                                found = true;
                            }
                            else
                            {
                                Log("Could not find " + partName + ".");
                            }
                            if (found == true && i == parts.Length)
                            {
                                stop = true;
                            }
                        }

                    }
                }
                catch { Log("Didn't work."); }
                Thread.Sleep(10000);
                if (found == true && i == parts.Length)
                {
                    stop = true;
                    break;
                }
            }
        });
        Harmony.CreateAndPatchAll(typeof(NervusPlugin).Assembly);
    }
}
