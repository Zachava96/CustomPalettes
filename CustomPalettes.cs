using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System;

namespace CustomPalettes
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInProcess("UNBEATABLE.exe")]
    public class CustomPalettes : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "net.zachava.custompalettes";
        public const string PLUGIN_NAME = "Custom Palettes";
        public const string PLUGIN_VERSION = "1.0.0";
        internal static new ManualLogSource Logger;
        public static List<MenuPaletteIndex.Palette> customPalettes = new List<MenuPaletteIndex.Palette>();

        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");

            string customPalettesFolderPath = Path.Combine(Paths.PluginPath, "CustomPalettes");
            string[] jsonFiles = Directory.GetFiles(customPalettesFolderPath, "*.json", SearchOption.AllDirectories);
            foreach (var jsonFile in jsonFiles)
            {
                try
                {
                    string jsonContent = File.ReadAllText(jsonFile);
                    CustomPaletteInfo customPalette = JsonConvert.DeserializeObject<CustomPaletteInfo>(jsonContent);
                    if (customPalette != null)
                    {
                        Array.Resize<SerializableColor>(ref customPalette.colors, 10);
                        MenuPaletteIndex.Palette menuPalette = new MenuPaletteIndex.Palette
                        {
                            name = customPalette.name,
                            palette = new Arcade.UI.UIColorPalette{colors = customPalette.colors.Select(c => c.ToColor()).ToArray()}
                        };
                        customPalettes.Add(menuPalette);
                        Logger.LogInfo($"Loaded custom palette {customPalette.name} from {jsonFile}");
                    }
                    else
                    {
                        Logger.LogWarning($"Failed to deserialize custom palette from {jsonFile}");
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.LogError($"Error reading custom palette file {jsonFile}: {ex}");
                }
            }
            
            var harmony = new Harmony(PLUGIN_GUID);
			harmony.PatchAll();
        }
    }
}