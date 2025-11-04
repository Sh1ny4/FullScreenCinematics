using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace FullScreenCinematics.Patches.Execution
{
    [HarmonyPatch(typeof(HeroExecutionSceneNotificationData), nameof(HeroExecutionSceneNotificationData.GetSceneNotificationCharacters))]
    internal class ExecutionSceneNPCAmountPatch
    {

        [HarmonyPostfix]
        static void Postfix(ref HeroExecutionSceneNotificationData __instance, ref SceneNotificationData.SceneNotificationCharacter[] __result)
        {
            Equipment equipment = __instance.Victim.BattleEquipment.Clone(true);
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.NumAllWeaponSlots, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon2, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon3, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.ExtraWeaponSlot, default(EquipmentElement));
            CampaignSceneNotificationHelper.RemoveWeaponsFromEquipment(ref equipment, true);
            Equipment equipment2 = __instance.Executer.BattleEquipment.Clone(true);
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon2, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon3, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.ExtraWeaponSlot, default(EquipmentElement));
            __result = new SceneNotificationData.SceneNotificationCharacter[]
            {
                CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(__instance.Victim, equipment, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false),
                CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(__instance.Executer, equipment2, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false),
            };
            TroopRoster roster = __instance.Executer.PartyBelongedTo.MemberRoster;
            for (int i = 0; i < 10; i++) 
            {
                Random rnd = new Random();
                int rand = rnd.Next(roster.TotalManCount);
                __result.AddItem(new SceneNotificationData.SceneNotificationCharacter(roster.GetCharacterAtIndex(rand)));
            }
        }
    }
}
