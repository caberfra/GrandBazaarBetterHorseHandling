using System;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using BokuMono;
using HarmonyLib;

namespace TutorialPlugin;

/**
 * The BepInPlugin attribute is used to specify the plugin's GUID, name, and version and is necessary for the plugin to be recognized by BepInEx
 * MyPluginInfo.PLUGIN_GUID is a unique identifier for the plugin, it needs to be unique. Linked to the assembly name in the .csproj file. 
 * MyPluginInfo.PLUGIN_NAME is the name of the plugin, it can be anything you want. Linked to the product name in the .csproj file
 * MyPluginInfo.PLUGIN_VERSION is the version of the plugin, it can be anything you want. Linked to the version in the .csproj file
 */
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    // This is a static reference to the logger so we can use it in other classes
    internal static new ManualLogSource Log;

    /**
     * The Load function is called when the plugin is loaded and is where you should put your initialization code
     * This function is called exactly once and is guaranteed to be called before any other functions in the plugin
     * Load is the IL2CPP equivalent of Awake
     */
    public override void Load()
    {
        // Plugin startup logic
        // Assigning the logger to the base logger(useful so you aren't calling Plugin.Log every time)
        Log = base.Log;
        
        // Logs on startup to the BepInEx console and log file
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        
        // Without this line, the patches won't be applied
        Harmony.CreateAndPatchAll(typeof(TutorialPatch));
    }
    
    private class TutorialPatch
    {
        
        /**
         * The [HarmonyPatch] attribute is used to specify the class and function to patch and is necessary for the patch to work
         * typeof(PlayerCharacter) specifies the class we want to patch
         * "DamageHP" specifies the function we want to patch(you can also do nameof(function) if you prefer but private functions won't work with that)
         * new Type[] {typeof(float), typeof(bool)} specifies the parameters of the function we want to patch, this is necessary if there are multiple functions with the same name
         * The [HarmonyPostfix] attribute is used to specify that this function should be called after the original function and is also necessary for the patch to work
         */
        [HarmonyPatch(typeof(PlayerCharacter))]
        // If there is only one function with the function name(in the class), you can leave out the new Type[] part
        [HarmonyPatch("DamageHP", new Type[] {typeof(float), typeof(bool)})] 
        [HarmonyPostfix]
        
        // You can name this whatever you want, but it must be static. the parameters must match the original function, with an optional __instance parameter at the start if you want to access instance variable
        // NOTE: not all parameters need to be included, only the ones you need
        public static void DamageHpPatch(float damage)
        {
            var oldDamageValue = damage; // Store the original damage value for logging purposes
            damage = 0; // This will make the damage taken from an action to 0
            Log.LogInfo($"Player took {oldDamageValue} damage, but it was set to {damage} by the plugin!"); // Log to show it working
        }
    }
}