using HarmonyLib;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;

namespace FullScreenCinematics.Patches
{
    [HarmonyPatch(typeof(HeroExecutionSceneNotificationData), nameof(HeroExecutionSceneNotificationData.GetSceneNotificationCharacters))]
    internal class ExecutionSceneItemsPatch
    {

        [HarmonyPrefix]
        static bool Prefix(ref HeroExecutionSceneNotificationData __instance, ref SceneNotificationData.SceneNotificationCharacter[] __result)
        {
            Equipment equipment = __instance.Victim.BattleEquipment.Clone(true);
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.NumAllWeaponSlots, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon2, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon3, default(EquipmentElement));
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.ExtraWeaponSlot, default(EquipmentElement));
            string executionItem = string.Concat(new object[] { "execution_item_", __instance.Executer.Culture.StringId });
            ItemObject item = (Game.Current.ObjectManager.GetObject<ItemObject>(executionItem) ?? Game.Current.ObjectManager.GetObject<ItemObject>("execution_axe"));
            Equipment equipment2 = __instance.Executer.BattleEquipment.Clone(true);
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, new EquipmentElement(item, null, null, false));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon2, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon3, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.ExtraWeaponSlot, default(EquipmentElement));
            __result = new SceneNotificationData.SceneNotificationCharacter[]
            {
                CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(__instance.Victim, equipment, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false),
                CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(__instance.Executer, equipment2, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false)
            };
            return false;
        }
    }
}
