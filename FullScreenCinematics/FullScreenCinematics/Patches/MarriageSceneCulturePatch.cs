using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

namespace FullScreenCinematics.Patches
{
    [HarmonyPatch(typeof(MarriageSceneNotificationItem), nameof(MarriageSceneNotificationItem.SceneID), MethodType.Getter)]
    internal class MarriageSceneCulturePatch
    {
        //linked it to the culture of the main hero, since we don't know it's gender.
        //can be changed to the groom by changing Hero.MainHero to __instance.GroomHero
        [HarmonyPostfix]
        static void Postfix(ref MarriageSceneNotificationItem __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_cutscene_wedding_", Hero.MainHero.Culture.StringId });
            var trySceneExist = new FallbackForSceneMissing();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_cutscene_wedding";
            __result = text;
        }
    }
}
