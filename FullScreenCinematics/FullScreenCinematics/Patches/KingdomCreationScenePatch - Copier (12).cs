using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace FullScreenCinematics.Patches
{
    [HarmonyPatch(typeof(KingdomCreatedSceneNotificationItem), nameof(KingdomCreatedSceneNotificationItem.SceneID), MethodType.Getter)]
    internal class KingdomCreationScenePatch : KingdomCreatedSceneNotificationItem
    {
        public KingdomCreationScenePatch(Kingdom newKingdom) : base(newKingdom)
        {
        }

        [HarmonyPostfix]
        static void Postfix(ref KingdomCreatedSceneNotificationItem __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_kingdom_made_", __instance.NewKingdom.Culture.StringId });
            var trySceneExist = new TrySceneExist();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_kingdom_made";
            __result = text;
        }
    }
}
