﻿using R2API;
using RoR2;
using RoR2.Achievements;
using RoR2.ExpansionManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Hex3Mod.HelperClasses;

namespace Hex3Mod.Items
{
    /*
    The Corrupting Parasite is an item idea thought up by conq and kking. I liked it and was given permission to make it into a full item, so here we are
    It should be a good way to form void builds and pave the way for more void items in the future
    */
    public class CorruptingParasite
    {
        static string itemName = "CorruptingParasite";
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/CorruptingParasitePrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/CorruptingParasite.png");
            return pickupIconSprite;
        }
        public static Sprite LoadAchievementSprite()
        {
            Sprite achievementIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/corruptingParasiteAchievement.png");
            return achievementIconSprite;
        }
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();
            UnlockableDef corruptingParasiteUnlock = ScriptableObject.CreateInstance<UnlockableDef>();

            corruptingParasiteUnlock.cachedName = "CorruptingParasiteUnlock";
            corruptingParasiteUnlock.nameToken = upperName + "_UNLOCK_NAME";
            corruptingParasiteUnlock.achievementIcon = LoadAchievementSprite();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist }; // It would be useless on enemies
            item.deprecatedTier = ItemTier.VoidTier1;
            item.canRemove = true;
            item.hidden = false;
            item.requiredExpansion = ExpansionCatalog.expansionDefs.FirstOrDefault(x => x.nameToken == "DLC1_NAME");

            item.pickupModelPrefab = LoadPrefab();
            item.pickupIconSprite = LoadSprite();

            ContentAddition.AddUnlockableDef(corruptingParasiteUnlock);
            item.unlockableDef = corruptingParasiteUnlock;
            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules()
        {
            GameObject ItemDisplayPrefab = helpers.PrepareItemDisplayModel(PrefabAPI.InstantiateClone(LoadPrefab(), LoadPrefab().name + "Display", false));

            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
            rules.Add("mdlCommandoDualies", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighR",
                        localPos = new Vector3(-0.08455F, -0.01497F, 0.0869F),
                        localAngles = new Vector3(330.4019F, 148.6668F, 168.6579F),
                        localScale = new Vector3(0.31789F, 0.29327F, 0.29788F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.09894F, 0.0666F, 0.14715F),
                        localAngles = new Vector3(286.2381F, 34.64287F, 176.1205F),
                        localScale = new Vector3(0.19941F, 0.19941F, 0.19941F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighR",
                        localPos = new Vector3(-0.32609F, 1.50743F, 0.58917F),
                        localAngles = new Vector3(287.1428F, 344.6615F, 146.7554F),
                        localScale = new Vector3(3.07578F, 3.07578F, 3.07578F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CannonHeadL",
                        localPos = new Vector3(0.04581F, 0.02192F, 0.00216F),
                        localAngles = new Vector3(25.12274F, 137.6865F, 142.4892F),
                        localScale = new Vector3(0.38887F, 0.38887F, 0.38887F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CalfL",
                        localPos = new Vector3(-0.0374F, 0.24028F, 0.07721F),
                        localAngles = new Vector3(319.5006F, 60.6742F, 83.30266F),
                        localScale = new Vector3(0.31656F, 0.31656F, 0.31656F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.113F, 0.21553F, 0.08614F),
                        localAngles = new Vector3(280.9289F, 23.77603F, 136.4629F),
                        localScale = new Vector3(0.36894F, 0.36894F, 0.36894F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(-0.33058F, 0.22093F, -0.88125F),
                        localAngles = new Vector3(49.63772F, 221.0377F, 37.76935F),
                        localScale = new Vector3(0.83672F, 0.83672F, 0.83672F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.13442F, 0.36149F, 0.23423F),
                        localAngles = new Vector3(331.5598F, 180.9432F, 359.8354F),
                        localScale = new Vector3(0.26171F, 0.26171F, 0.26171F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmL",
                        localPos = new Vector3(-0.57796F, 3.79938F, 0.0451F),
                        localAngles = new Vector3(355.382F, 354.386F, 83.46042F),
                        localScale = new Vector3(3.48469F, 3.48469F, 3.48469F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighL",
                        localPos = new Vector3(0.09961F, 0.39041F, -0.07873F),
                        localAngles = new Vector3(29.06793F, 171.0644F, 91.19601F),
                        localScale = new Vector3(0.39495F, 0.39935F, 0.39495F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Stomach",
                        localPos = new Vector3(0.17586F, 0.14657F, -0.04721F),
                        localAngles = new Vector3(31.56213F, 188.1448F, 85.46602F),
                        localScale = new Vector3(0.25929F, 0.25929F, 0.25929F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.60931F, 0.757F, -1.06356F),
                        localAngles = new Vector3(17.09353F, 229.7854F, 359.3452F),
                        localScale = new Vector3(1.04019F, 1.04019F, 1.04019F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.69553F, 1.1678F, -1.17419F),
                        localAngles = new Vector3(30.41575F, 236.0284F, 15.85494F),
                        localScale = new Vector3(0.84911F, 0.84911F, 0.84911F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(-7.20899F, 11.39804F, 1.42232F),
                        localAngles = new Vector3(334.2978F, 178.9926F, 359.245F),
                        localScale = new Vector3(3.81427F, 3.81427F, 3.81427F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmR",
                        localPos = new Vector3(-0.01926F, 0.13436F, 0.02021F),
                        localAngles = new Vector3(9.62257F, 233.6922F, 250.2488F),
                        localScale = new Vector3(0.23791F, 0.23791F, 0.23791F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.00862F, 0.18012F, -0.15892F),
                        localAngles = new Vector3(84.5425F, 187.6659F, 4.56151F),
                        localScale = new Vector3(0.42825F, 0.42825F, 0.42825F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens(bool CorruptingParasite_CorruptBossItems, int CorruptingParasite_ItemsPerStage)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Corrupting Parasite");
            if (CorruptingParasite_CorruptBossItems == false)
            {
                LanguageAPI.Add("H3_" + upperName + "_PICKUP", string.Format("Corrupts {0} of your items into their <style=cIsVoid>void equivalents</style> each stage.", CorruptingParasite_ItemsPerStage));
                LanguageAPI.Add("H3_" + upperName + "_DESC", string.Format("At the start of a stage, <style=cIsVoid>{0} random item(s) will be corrupted into their void equivalent</style> <style=cStack>(+{0} per stack)</style>.", CorruptingParasite_ItemsPerStage));
            }
            else
            {
                LanguageAPI.Add("H3_" + upperName + "_PICKUP", string.Format("Corrupts {0} of your items into their <style=cIsVoid>void equivalents</style> each stage.", CorruptingParasite_ItemsPerStage));
                LanguageAPI.Add("H3_" + upperName + "_DESC", string.Format("At the start of a stage, <style=cIsVoid>{0} random item(s) will be corrupted into their void equivalent</style> <style=cStack>(+{0} per stack)</style>. <style=cShrine>Affects boss items.</style>", CorruptingParasite_ItemsPerStage));
            }
            
            LanguageAPI.Add("H3_" + upperName + "_LORE", "Order: Bugs" +
            "\nTracking Number: 977******" +
            "\nEstimated Delivery: Not approved" +
            "\nShipping Method: Priority" +
            "\nShipping Address: Earth, Unspecified address" +
            "\n\nBugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs?" +
            "\n\nBugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs?");

            LanguageAPI.Add("ACHIEVEMENT_" + upperName + "_NAME", "From The Depths");
            LanguageAPI.Add("ACHIEVEMENT_" + upperName + "_DESCRIPTION", "Enter the Deep Void Portal.");
            LanguageAPI.Add(upperName + "_UNLOCK_NAME", "From The Depths");
        }

        private static void AddHooks(ItemDef itemDef, bool CorruptingParasite_CorruptBossItems, int CorruptingParasite_ItemsPerStage)
        {
            On.RoR2.CharacterMaster.OnServerStageBegin += (orig, self, stage) =>
            {
                orig(self, stage);
                if (self.inventory && self.inventory.GetItemCount(itemDef) > 0)
                {
                    Xoroshiro128Plus rng = new Xoroshiro128Plus(Run.instance.stageRng.nextUlong);
                    int itemsLeft = self.inventory.GetItemCount(itemDef) * CorruptingParasite_ItemsPerStage;
                    List<ItemIndex> itemList = new List<ItemIndex>(self.inventory.itemAcquisitionOrder);
                    Util.ShuffleList(itemList, rng);
                    rng.Next();

                    foreach (ItemIndex item in itemList)
                    {
                        if (itemsLeft <= 0)
                        {
                            break;
                        }
                        if (!CorruptingParasite_CorruptBossItems && ItemCatalog.GetItemDef(item).tier == ItemTier.Boss)
                        {
                            continue;
                        }
                        foreach (ItemDef.Pair pair in ItemCatalog.GetItemPairsForRelationship(DLC1Content.ItemRelationshipTypes.ContagiousItem))
                        {
                            if (pair.itemDef1 == ItemCatalog.GetItemDef(item))
                            {
                                self.inventory.RemoveItem(item);
                                self.inventory.GiveItem(pair.itemDef2);
                                CharacterMasterNotificationQueue.PushItemTransformNotification(self, item, pair.itemDef2.itemIndex, CharacterMasterNotificationQueue.TransformationType.ContagiousVoid);
                                itemsLeft--;
                                break;
                            }
                        }
                    }
                }
            };
        }

        [RegisterAchievement("CorruptingParasite", "CorruptingParasiteUnlock", null, typeof(CorruptingParasiteAchievement))]
        public class CorruptingParasiteAchievement : BaseAchievement
        {
            public override void OnInstall()
            {
                base.OnInstall();
                On.RoR2.CharacterMaster.OnServerStageBegin += CharacterMaster_OnServerStageBegin;
            }

            public override void OnUninstall()
            {
                On.RoR2.CharacterMaster.OnServerStageBegin -= CharacterMaster_OnServerStageBegin;
                base.OnUninstall();
            }

            private void CharacterMaster_OnServerStageBegin(On.RoR2.CharacterMaster.orig_OnServerStageBegin orig, CharacterMaster self, Stage stage)
            {
                orig(self, stage);
                if (localUser != null && stage.sceneDef.baseSceneName == "voidraid")
                {
                    Grant();
                }
            }
        }

        public static void Initiate(bool CorruptingParasite_CorruptBossItems, int CorruptingParasite_ItemsPerStage)
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(CorruptingParasite_CorruptBossItems, CorruptingParasite_ItemsPerStage);
            AddHooks(itemDefinition, CorruptingParasite_CorruptBossItems, CorruptingParasite_ItemsPerStage);
        }
    }
}
