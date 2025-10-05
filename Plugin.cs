using System;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using BepInEx.Configuration;
using HarmonyLib;
using BokuMono;

namespace BetterHorseHandling;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;
    
    // Config values
    
    // General
    internal static ConfigEntry<bool> Debug;
    internal static ConfigEntry<bool> AlwaysMaxStats;
    
    // 0 Hearts
    internal static ConfigEntry<float> InitSpeed_0;
    internal static ConfigEntry<float> MaxSpeed_0;
    internal static ConfigEntry<float> RunMotionBorderSpeed_0;
    internal static ConfigEntry<float> RunMotionBorder_0;
    internal static ConfigEntry<float> TurnBorderSpeed_0;
    internal static ConfigEntry<float> TurnSpeed_0;
    internal static ConfigEntry<float> DecelerationTurnSpeed_0;
    internal static ConfigEntry<float> DecelerationSpeed_0;
    internal static ConfigEntry<float> AccelerationWalk_0;
    internal static ConfigEntry<float> AccelerationRun_0;
    internal static ConfigEntry<float> MinMotionSpeed_0;
    internal static ConfigEntry<float> MaxMotionSpeed_0;
    internal static ConfigEntry<float> JumpHight_0;
    internal static ConfigEntry<float> MaxJumpHight_0;
    internal static ConfigEntry<float> JumpForwardPower_0;

    // 10 Hearts
    internal static ConfigEntry<float> InitSpeed_10;
    internal static ConfigEntry<float> MaxSpeed_10;
    internal static ConfigEntry<float> RunMotionBorderSpeed_10;
    internal static ConfigEntry<float> RunMotionBorder_10;
    internal static ConfigEntry<float> TurnBorderSpeed_10;
    internal static ConfigEntry<float> TurnSpeed_10;
    internal static ConfigEntry<float> DecelerationTurnSpeed_10;
    internal static ConfigEntry<float> DecelerationSpeed_10;
    internal static ConfigEntry<float> AccelerationWalk_10;
    internal static ConfigEntry<float> AccelerationRun_10;
    internal static ConfigEntry<float> MinMotionSpeed_10;
    internal static ConfigEntry<float> MaxMotionSpeed_10;
    internal static ConfigEntry<float> JumpHight_10;
    internal static ConfigEntry<float> MaxJumpHight_10;
    internal static ConfigEntry<float> JumpForwardPower_10;
    
    // Interpolation
    internal static ConfigEntry<bool> UseExponentialInterpolation;
    internal static ConfigEntry<float> InterpolationExponent;
 
    public override void Load()
    {
        Debug = Config.Bind(
            "General",
            "Debug",
            false,
            "If true, enables debug logging and diagnostic output."
        );
        
        AlwaysMaxStats = Config.Bind(
            "General",
            "AlwaysMaxStats",
            false,
            "If true, horse stats will always use the 10-heart values regardless of friendship level. Friendship level will not change"
        );
        
        // 0 Hearts
        InitSpeed_0              = Config.Bind("Horse Speed - 0 Hearts", "InitSpeed",              3.0f,  "Vanilla: 3.0");
        MaxSpeed_0               = Config.Bind("Horse Speed - 0 Hearts", "MaxSpeed",               7.0f,  "Vanilla: 7.0");
        RunMotionBorderSpeed_0   = Config.Bind("Horse Speed - 0 Hearts", "RunMotionBorderSpeed",   7.0f,  "Vanilla: 7.0");
        RunMotionBorder_0        = Config.Bind("Horse Speed - 0 Hearts", "RunMotionBorder",        0.6f,  "Vanilla: 0.6");
        TurnBorderSpeed_0        = Config.Bind("Horse Speed - 0 Hearts", "TurnBorderSpeed",        12.0f, "Vanilla: 7.9");
        TurnSpeed_0              = Config.Bind("Horse Speed - 0 Hearts", "TurnSpeed",              200.0f,"Vanilla: 120.0");
        DecelerationTurnSpeed_0  = Config.Bind("Horse Speed - 0 Hearts", "DecelerationTurnSpeed",  10.0f, "Vanilla: 1.0");
        DecelerationSpeed_0      = Config.Bind("Horse Speed - 0 Hearts", "DecelerationSpeed",      10.0f, "Vanilla: 0.2");
        AccelerationWalk_0       = Config.Bind("Horse Speed - 0 Hearts", "AccelerationWalk",       20.0f, "Vanilla: 5.5");
        AccelerationRun_0        = Config.Bind("Horse Speed - 0 Hearts", "AccelerationRun",        20.0f, "Vanilla: 5.0");
        MinMotionSpeed_0         = Config.Bind("Horse Speed - 0 Hearts", "MinMotionSpeed",         1.0f,  "Vanilla: 1.0");
        MaxMotionSpeed_0         = Config.Bind("Horse Speed - 0 Hearts", "MaxMotionSpeed",         1.0f,  "Vanilla: 1.0");
        JumpHight_0              = Config.Bind("Horse Speed - 0 Hearts", "JumpHight",              5.5f,  "Vanilla: 5.5");
        MaxJumpHight_0           = Config.Bind("Horse Speed - 0 Hearts", "MaxJumpHight",           1.3f,  "Vanilla: 1.3");
        JumpForwardPower_0       = Config.Bind("Horse Speed - 0 Hearts", "JumpForwardPower",       2.0f,  "Vanilla: 2.0");

        // 10 Hearts
        InitSpeed_10             = Config.Bind("Horse Speed - 10 Hearts", "InitSpeed",             4.0f,  "Vanilla: 4.0");
        MaxSpeed_10              = Config.Bind("Horse Speed - 10 Hearts", "MaxSpeed",              10.2f, "Vanilla: 10.2");
        RunMotionBorderSpeed_10  = Config.Bind("Horse Speed - 10 Hearts", "RunMotionBorderSpeed",  8.0f,  "Vanilla: 7.0");
        RunMotionBorder_10       = Config.Bind("Horse Speed - 10 Hearts", "RunMotionBorder",       1.20f, "Vanilla: 0.6");
        TurnBorderSpeed_10       = Config.Bind("Horse Speed - 10 Hearts", "TurnBorderSpeed",       12.0f, "Vanilla: 7.9");
        TurnSpeed_10             = Config.Bind("Horse Speed - 10 Hearts", "TurnSpeed",             500.0f,"Vanilla: 120.0");
        DecelerationTurnSpeed_10 = Config.Bind("Horse Speed - 10 Hearts", "DecelerationTurnSpeed", 10.0f, "Vanilla: 1.0");
        DecelerationSpeed_10     = Config.Bind("Horse Speed - 10 Hearts", "DecelerationSpeed",     10.0f, "Vanilla: 0.2");
        AccelerationWalk_10      = Config.Bind("Horse Speed - 10 Hearts", "AccelerationWalk",      20.0f, "Vanilla: 5.5");
        AccelerationRun_10       = Config.Bind("Horse Speed - 10 Hearts", "AccelerationRun",       20.0f, "Vanilla: 5.0");
        MinMotionSpeed_10        = Config.Bind("Horse Speed - 10 Hearts", "MinMotionSpeed",        1.0f,  "Vanilla: 1.0");
        MaxMotionSpeed_10        = Config.Bind("Horse Speed - 10 Hearts", "MaxMotionSpeed",        1.0f,  "Vanilla: 1.0");
        JumpHight_10             = Config.Bind("Horse Speed - 10 Hearts", "JumpHight",             5.5f,  "Vanilla: 5.5");
        MaxJumpHight_10          = Config.Bind("Horse Speed - 10 Hearts", "MaxJumpHight",          1.3f,  "Vanilla: 1.3");
        JumpForwardPower_10      = Config.Bind("Horse Speed - 10 Hearts", "JumpForwardPower",      0.5f,  "Vanilla: 0.5");
        
        UseExponentialInterpolation = Config.Bind(
            "Interpolation",
            "UseExponentialInterpolation",
            false,
            "If true, uses exponential scaling. For example, MaxSpeed ~= ( FriendshipLevel / 10 )^InterpolationExponent."
        );

        InterpolationExponent = Config.Bind(
            "Interpolation",
            "InterpolationExponent",
            1.5f,
            "Exponent used for exponential interpolation. (0.0, 1.0] for easy gains at the start, [1.0, inf) for slower scaling at the start."
        );
        
        // Plugin startup logic
        Log = base.Log;
        Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        Harmony.CreateAndPatchAll(typeof(HorseSpeedSettingsPatch));
    }
    
    private class HorseSpeedSettingsPatch
    {
        // PREFIX (overrides the input of PetAnimalCommonSetting.GetHorseSpeedSetting)
        [HarmonyPatch(typeof(PetAnimalCommonSetting))]
        [HarmonyPatch("GetHorseSpeedSetting")]
        [HarmonyPrefix]
        public static bool GetHorseSpeedSettingPrefix(ref int friendLevel)
        {
            if (Debug.Value)
            {
                Log.LogInfo("[DEBUG] GetHorseSpeedSettingPatch PreFix");
                Log.LogInfo($"[DEBUG] friendLevel: {friendLevel}");
            }
            
            // Override the parameter
            if (!AlwaysMaxStats.Value) return true;
            friendLevel = 10;
                
            // Log the change
            if (Debug.Value) Log.LogInfo($"[DEBUG] Overridden friendLevel to: {friendLevel}");


            // Return true to continue with the original method
            return true;
        }
        
        
        // POSTFIX (overrides the output of PetAnimalCommonSetting.GetHorseSpeedSetting)
        [HarmonyPatch(typeof(PetAnimalCommonSetting))]
        [HarmonyPatch("GetHorseSpeedSetting")] 
        [HarmonyPostfix]
        public static void GetHorseSpeedSettingPatch(int friendLevel, PetAnimalHorseSpeedSetting __result)
        {
            if (Debug.Value)
            {
                Log.LogInfo("[DEBUG] GetHorseSpeedSettingPatch PostFix");
                Log.LogInfo($"[DEBUG] friendLevel: {friendLevel}");
            }
            
            if (__result != null)
            {
                // Log original values
                if (Debug.Value) LogHorseSpeedSettings(__result);

                // Modify values
                if (UseExponentialInterpolation.Value)
                {
                    ApplyScaledHorseSettings(friendLevel, ref __result, (a, b, t) => ExpLerp(a, b, t, InterpolationExponent.Value));
                }
                else
                {
                    ApplyScaledHorseSettings(friendLevel, ref __result, Lerp);
                }

                // Log modified values
                if (Debug.Value) Log.LogInfo($"[DEBUG] Overridden Horse Stats");
                if (Debug.Value) LogHorseSpeedSettings(__result);
                
            }
            else
            {
                Log.LogWarning("GetHorseSpeedSetting returned null.");
            }
        }
    }
    
    // Modifies the properties of PetAnimalHorseSpeedSetting with given a friendLevel
    // Uses either linear or exponential interpolation
    private static void ApplyScaledHorseSettings(
        int friendLevel,
        ref PetAnimalHorseSpeedSetting __result,
        Func<float, float, float, float> interpolate)
    {
        friendLevel = Math.Clamp(friendLevel, 0, 10);
        float t = friendLevel / 10f;

        __result.InitSpeed            = interpolate(InitSpeed_0.Value,             InitSpeed_10.Value,             t);
        __result.MaxSpeed             = interpolate(MaxSpeed_0.Value,              MaxSpeed_10.Value,              t);
        __result.RunMotionBorderSpeed = interpolate(RunMotionBorderSpeed_0.Value,  RunMotionBorderSpeed_10.Value,  t);
        __result.RunMotionBorder      = interpolate(RunMotionBorder_0.Value,       RunMotionBorder_10.Value,       t);
        __result.TurnBorderSpeed      = interpolate(TurnBorderSpeed_0.Value,       TurnBorderSpeed_10.Value,       t);
        __result.TurnSpeed            = interpolate(TurnSpeed_0.Value,             TurnSpeed_10.Value,             t);
        __result.DecelerationTurnSpeed= interpolate(DecelerationTurnSpeed_0.Value, DecelerationTurnSpeed_10.Value, t);
        __result.DecelerationSpeed    = interpolate(DecelerationSpeed_0.Value,     DecelerationSpeed_10.Value,     t);
        __result.AccelerationWalk     = interpolate(AccelerationWalk_0.Value,      AccelerationWalk_10.Value,      t);
        __result.AccelerationRun      = interpolate(AccelerationRun_0.Value,       AccelerationRun_10.Value,       t);
        __result.MinMotionSpeed       = interpolate(MinMotionSpeed_0.Value,        MinMotionSpeed_10.Value,        t);
        __result.MaxMotionSpeed       = interpolate(MaxMotionSpeed_0.Value,        MaxMotionSpeed_10.Value,        t);
        __result.JumpHight            = interpolate(JumpHight_0.Value,             JumpHight_10.Value,             t);
        __result.MaxJumpHight         = interpolate(MaxJumpHight_0.Value,          MaxJumpHight_10.Value,          t);
        __result.JumpForwardPower     = interpolate(JumpForwardPower_0.Value,      JumpForwardPower_10.Value,      t);
    }

    // Simple linear interpolation helper
    private static float Lerp(float a, float b, float t)
    {
        return (float)Math.Round((a + (b - a) * t), 2);
    }
    
    // Exponential interpolation helper
    private static float ExpLerp(float a, float b, float t, float exponent = 2f)
    {
        // Clamp t between 0 and 1
        t = Math.Clamp(t, 0f, 1f);

        // Apply exponential curve
        float curvedT = MathF.Pow(t, exponent);

        return (float)Math.Round(a + (b - a) * curvedT, 2);
    }
    
    // Prints PetAnimalHorseSpeedSetting properties
    private static void LogHorseSpeedSettings(PetAnimalHorseSpeedSetting __result)
    {
        Log.LogInfo($"--- Horse Speed Settings ---");
        Log.LogInfo($"MinFriendLevel:          {__result.MinFriendLevel}");
        Log.LogInfo($"MaxFriendLevel:          {__result.MaxFriendLevel}");
        Log.LogInfo($"InitSpeed:               {__result.InitSpeed}");
        Log.LogInfo($"MaxSpeed:                {__result.MaxSpeed}");
        Log.LogInfo($"RunMotionBorderSpeed:    {__result.RunMotionBorderSpeed}");
        Log.LogInfo($"RunMotionBorder:         {__result.RunMotionBorder}");
        Log.LogInfo($"TurnBorderSpeed:         {__result.TurnBorderSpeed}");
        Log.LogInfo($"TurnSpeed:               {__result.TurnSpeed}");
        Log.LogInfo($"DecelerationTurnSpeed:   {__result.DecelerationTurnSpeed}");
        Log.LogInfo($"DecelerationSpeed:       {__result.DecelerationSpeed}");
        Log.LogInfo($"AccelerationWalk:        {__result.AccelerationWalk}");
        Log.LogInfo($"AccelerationRun:         {__result.AccelerationRun}");
        Log.LogInfo($"MinMotionSpeed:          {__result.MinMotionSpeed}");
        Log.LogInfo($"MaxMotionSpeed:          {__result.MaxMotionSpeed}");
        Log.LogInfo($"JumpHight:               {__result.JumpHight}");
        Log.LogInfo($"MaxJumpHight:            {__result.MaxJumpHight}");
        Log.LogInfo($"JumpForwardPower:        {__result.JumpForwardPower}");
        Log.LogInfo($"----------------------------");
    }
}