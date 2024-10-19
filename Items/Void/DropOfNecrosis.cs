﻿using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System.Linq;
using UnityEngine;
using Hex3Mod.HelperClasses;
using Hex3Mod.Utils;
using static Hex3Mod.Main;
using System;

namespace Hex3Mod.Items
{
    /*
    The void item counterpart for Scattered Reflection. Meant to be paired with Drop Of Necrosis for a blight build, allowing damage scaling in a fully void build
    */
    public static class DropOfNecrosis
    {
        static string itemName = "DropOfNecrosis";
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/DropOfNecrosisPrefab.prefab");
            if (debugMode)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            return MainAssets.LoadAsset<Sprite>("Assets/Icons/DropOfNecrosis.png");
        }
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Damage };
            item._itemTierDef = helpers.GenerateItemDef(ItemTier.VoidTier1);
            item.canRemove = true;
            item.hidden = false;
            item.requiredExpansion = Hex3ModExpansion;

            item.pickupModelPrefab = LoadPrefab();
            item.pickupIconSprite = LoadSprite();

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules()
        {
            GameObject ItemDisplayPrefab = helpers.PrepareItemDisplayModel(PrefabAPI.InstantiateClone(LoadPrefab(), LoadPrefab().name + "Display", false));

            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
            rules.Add("mdlCommandoDualies", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Head",
                            localPos = new Vector3(0.19585F, 0.2992F, 0.20006F),
                            localAngles = new Vector3(0F, 0F, 0F),
                            localScale = new Vector3(0.10963F, 0.10963F, 0.10963F)
                        }
                    }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Chest",
                            localPos = new Vector3(0F, 0.14741F, 0.16468F),
                            localAngles = new Vector3(0F, 0F, 0F),
                            localScale = new Vector3(0.12898F, 0.12898F, 0.12898F)
                        }
                    }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "LowerArmL",
                            localPos = new Vector3(2.06932F, 2.66694F, 0F),
                            localAngles = new Vector3(0F, 0F, 90F),
                            localScale = new Vector3(1.14318F, 1.14318F, 1.14318F)
                        }
                    }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "LowerArmR",
                            localPos = new Vector3(0F, 0.22452F, 0F),
                            localAngles = new Vector3(0F, 0F, 0F),
                            localScale = new Vector3(0.18898F, 0.18898F, 0.18898F)
                        }
                    }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Chest",
                            localPos = new Vector3(0F, 0.00003F, 0.15016F),
                            localAngles = new Vector3(0F, 0F, 0F),
                            localScale = new Vector3(0.09006F, 0.09006F, 0.09006F)
                        }
                    }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Head",
                            localPos = new Vector3(0F, 0.1513F, 0.05275F),
                            localAngles = new Vector3(284.2869F, 180F, 180F),
                            localScale = new Vector3(0.37206F, 0.37206F, 0.37206F)
                        }
                    }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Chest",
                            localPos = new Vector3(-0.12459F, 0.3231F, 0.91488F),
                            localAngles = new Vector3(0F, 0F, 0F),
                            localScale = new Vector3(0.25185F, 0.25185F, 0.25185F)
                        }
                    }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "MechHandR",
                            localPos = new Vector3(0.02804F, 0F, 0.05959F),
                            localAngles = new Vector3(0F, 0F, 0F),
                            localScale = new Vector3(0.20992F, 0.20992F, 0.20992F)
                        }
                    }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "MouthMuzzle",
                            localPos = new Vector3(1.34505F, 2.83495F, 2.28644F),
                            localAngles = new Vector3(0F, 0F, 180F),
                            localScale = new Vector3(1.08472F, 1.08472F, 1.08897F)
                        }
                    }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "CalfL",
                            localPos = new Vector3(0F, 0.03235F, -0.05129F),
                            localAngles = new Vector3(15.34042F, 0F, 0F),
                            localScale = new Vector3(0.26102F, 0.26102F, 0.26102F)
                        }
                    }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Hat",
                            localPos = new Vector3(0.16872F, -0.02349F, 0.21604F),
                            localAngles = new Vector3(11.23946F, 172.4814F, 346.6523F),
                            localScale = new Vector3(0.10125F, 0.10125F, 0.10125F)
                        }
                    }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Head",
                            localPos = new Vector3(1.48743F, 0.47423F, -0.03004F),
                            localAngles = new Vector3(359.6133F, 179.1287F, 0.86641F),
                            localScale = new Vector3(0.51991F, 0.51991F, 0.51991F)
                        }
                    }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Head",
                            localPos = new Vector3(1.41124F, 1.02324F, -0.29499F),
                            localAngles = new Vector3(0F, 180.0867F, 0F),
                            localScale = new Vector3(0.48724F, 0.48724F, 0.48724F)
                        }
                    }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Chest",
                            localPos = new Vector3(5.53473F, 3.75452F, -4.22792F),
                            localAngles = new Vector3(43.78985F, 0F, 0F),
                            localScale = new Vector3(2.65545F, 2.65545F, 2.65545F)
                        }
                    }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "GunRoot",
                            localPos = new Vector3(0F, -0.51284F, -0.35865F),
                            localAngles = new Vector3(90F, 0F, 0F),
                            localScale = new Vector3(0.13449F, 0.13449F, 0.13449F)
                        }
                    }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Hand",
                            localPos = new Vector3(-0.00001F, -0.04555F, -0.00002F),
                            localAngles = new Vector3(0F, 0F, 0F),
                            localScale = new Vector3(0.22237F, 0.22237F, 0.22237F)
                        }
                    }
            );

            return rules;
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Drop Of Necrosis");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Your attacks have a chance to inflict <style=cIsDamage>Blight</style> with slightly increased damage. <style=cIsVoid>Corrupts all Shards of Glass.</style>");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"I think I was... exploring? I... my spyglass is...\"" +
            "\n\n<style=cStack>(Silence for 30 seconds)</style>" +
            "\n\n\"Right, it's gone. All that's left is... hm.\"" +
            "\n\n<style=cStack>(Silence for several minutes)</style>" +
            "\n\n\"There's... there was a big loud sound, and-- poison, poison poured from the walls... purple, stinging poison.\"" +
            "\n\n<style=cStack>(The clinking of what is assumed to be filled glass bottles and cups is heard.)</style>" +
            "\n\n\"Ahhh... yes, that's... mmm, my poisons. You will not find my poisons... my collection-- it's mine, mine forever. You wouldn't appreciate them.\"" +
            "\n\n<style=cStack>(Audio recording ends abruptly.)</style>");
        }
        public static void UpdateItemStatus(Run run)
        {
            if (!DropOfNecrosis_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Drop Of Necrosis" + " <style=cDeath>[DISABLED]</style>");
                if (run && run.availableItems.Contains(itemDef.itemIndex)) { run.availableItems.Remove(itemDef.itemIndex); }
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Drop Of Necrosis");
                if (run && !run.availableItems.Contains(itemDef.itemIndex) && run.IsExpansionEnabled(Hex3ModExpansion)) { run.availableItems.Add(itemDef.itemIndex); }
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", "Your attacks have a " + DropOfNecrosis_DotChance.Value + "% <style=cStack>(+" + DropOfNecrosis_DotChance.Value + "% per stack)</style> chance to inflict <style=cIsDamage>Blight</style>, which deals <style=cIsDamage>" + (DropOfNecrosis_Damage.Value * 100f) + "%</style> more damage for each additional stack of this item. <style=cIsVoid>Corrupts all Shards of Glass.</style>");
        }

        private static void AddHooks() // Insert hooks here
        {
            // Void transformation
            VoidTransformation.Add(itemDef, "ShardOfGlass");

            On.RoR2.DotController.AddDot += (orig, self, attackerObject, duration, dotIndex, damageMultiplier, maxStacksFromAttacker, totalDamage, preUpgradeDotIndex) =>
            {
                if (dotIndex == DotController.DotIndex.Blight && attackerObject && attackerObject.TryGetComponent(out CharacterBody body) && body.inventory && body.inventory.GetItemCount(itemDef) > 0)
                {
                    damageMultiplier += DropOfNecrosis_Damage.Value * body.inventory.GetItemCount(itemDef);
                }
                orig(self, attackerObject, duration, dotIndex, damageMultiplier, maxStacksFromAttacker, totalDamage, preUpgradeDotIndex);
            };

            On.RoR2.GlobalEventManager.OnHitEnemy += (orig, self, damageInfo, victim) =>
            {
                if (damageInfo.attacker && damageInfo.attacker.TryGetComponent(out CharacterBody body1) && body1.inventory && body1.inventory.GetItemCount(itemDef) > 0)
                {
                    if (body1.master && damageInfo.dotIndex != DotController.DotIndex.Blight && damageInfo.attacker != victim)
                    {
                        if (damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.FallDamage && damageInfo.damage > 0f)
                        {
                            if (Util.CheckRoll((DropOfNecrosis_DotChance.Value * body1.inventory.GetItemCount(itemDef)) * damageInfo.procCoefficient, body1.master.luck))
                            {
                                InflictDotInfo inflictDotInfo = new InflictDotInfo
                                {
                                    attackerObject = damageInfo.attacker,
                                    victimObject = victim,
                                    dotIndex = DotController.DotIndex.Blight,
                                    duration = 5f,
                                    damageMultiplier = 1f
                                };
                                DotController.InflictDot(ref inflictDotInfo);
                            }
                        }
                    }
                }
                orig(self, damageInfo, victim);
            };
        }

        public static void Initiate()
        {
            itemDef = CreateItem();
            ItemAPI.Add(new CustomItem(itemDef, CreateDisplayRules()));
            AddTokens();
            UpdateItemStatus(null);
            AddHooks();
        }
    }
}
