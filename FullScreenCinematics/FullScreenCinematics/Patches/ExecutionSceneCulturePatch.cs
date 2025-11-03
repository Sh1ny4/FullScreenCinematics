using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace FullScreenCinematics.Patches
{
    [HarmonyPatch(typeof(HeroExecutionSceneNotificationData), nameof(HeroExecutionSceneNotificationData.SceneID), MethodType.Getter)]
    internal class ExecutionSceneCulturePatch
    {

        [HarmonyPostfix]
        static void Postfix(ref HeroExecutionSceneNotificationData __instance, ref string __result)
        {
            string text = string.Concat(new object[] { "scn_execution_notification_", __instance.Executer.Culture.StringId });
            var trySceneExist = new TrySceneExist();
            text = trySceneExist.TryGetSceneExist(text) ? text : "scn_execution_notification";
            __result = text;
        }
    }
}
