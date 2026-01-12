using HarmonyLib;

namespace CustomPalettes
{
    [HarmonyPatch(typeof(MenuPaletteIndex), "OnAfterDeserialize")]
    class MenuPaletteIndexOnAfterDeserializePatch
    {
        static void Postfix(MenuPaletteIndex __instance)
        {
            __instance.AddPalettes(CustomPalettes.customPalettes);
        }
    }
}