using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace FullScreenCinematics
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            new Harmony("FullScreenCinematics.patches").PatchAll();
        }
    }
}
