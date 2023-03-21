﻿using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System.Linq;
using UnityEngine;
using Hex3Mod.HelperClasses;
using VoidItemAPI;
using UnityEngine.Networking;
using System;
using Hex3Mod.Utils;
using static Hex3Mod.Main;
using System.ComponentModel;

namespace Hex3Mod.Items
{
    /*
    What if you want a pearl run, but don't want to find a pesky cleansing pool? That's where Do Not Eat comes in
    Opening chests and getting a bonus feels good, plus we needed more max health items to counteract late game
    */
    public class DoNotEat
    {
        static string itemName = "DoNotEat";
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/DoNotEatPrefab.prefab");
            if (Main.debugMode == true)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/DoNotEat.png");
            return pickupIconSprite;
        }
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.Healing };
            item.deprecatedTier = ItemTier.Tier3;
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
                        childName = "Pelvis",
                        localPos = new Vector3(0.15416F, -0.07144F, -0.00001F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.09441F, 0.09441F, 0.09441F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.00001F, 0.24273F, 0.05034F),
                        localAngles = new Vector3(25.39802F, 0F, 0F),
                        localScale = new Vector3(0.07587F, 0.07587F, 0.07587F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(1.05056F, 1.94221F, 1.64765F),
                        localAngles = new Vector3(45.8378F, 0F, 0F),
                        localScale = new Vector3(0.85108F, 0.85108F, 0.85108F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CannonHeadR",
                        localPos = new Vector3(0F, 0.39234F, 0.00003F),
                        localAngles = new Vector3(314.5863F, 0F, 0F),
                        localScale = new Vector3(0.11812F, 0.11812F, 0.11812F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.11241F, 0.28087F, -0.18459F),
                        localAngles = new Vector3(323.6529F, 0F, 0F),
                        localScale = new Vector3(0.09022F, 0.09022F, 0.09022F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandR",
                        localPos = new Vector3(-0.021F, 0.17313F, 0.01653F),
                        localAngles = new Vector3(36.11853F, 98.29133F, 8.29494F),
                        localScale = new Vector3(0.1098F, 0.1098F, 0.1098F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "WeaponPlatform",
                        localPos = new Vector3(-0.14601F, 0.07474F, 0.17599F),
                        localAngles = new Vector3(314.9279F, 0F, 0F),
                        localScale = new Vector3(0.19015F, 0.19015F, 0.19015F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.20309F, 0.53486F, 0.03296F),
                        localAngles = new Vector3(50.2245F, 0F, 0F),
                        localScale = new Vector3(0.10039F, 0.10039F, 0.10039F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MouthMuzzle",
                        localPos = new Vector3(-0.21135F, 2.19588F, 2.52151F),
                        localAngles = new Vector3(44.53611F, 86.83545F, 0.12085F),
                        localScale = new Vector3(1.12444F, 1.12444F, 1.12885F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "LowerArmL",
                        localPos = new Vector3(-0.00595F, 0.42614F, 0.04707F),
                        localAngles = new Vector3(315.9901F, 351.1433F, 12.76379F),
                        localScale = new Vector3(0.11838F, 0.11838F, 0.11838F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MainWeapon",
                        localPos = new Vector3(-0.14041F, 0.41272F, -0.00407F),
                        localAngles = new Vector3(315.4345F, 0F, 0F),
                        localScale = new Vector3(0.08203F, 0.08203F, 0.08203F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0F, 0.57577F, 0.58945F),
                        localAngles = new Vector3(45.66947F, 0F, 0F),
                        localScale = new Vector3(0.35445F, 0.35445F, 0.35445F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0F, 0.20624F, -0.20413F),
                        localAngles = new Vector3(45.29741F, 0F, 0F),
                        localScale = new Vector3(0.33057F, 0.33057F, 0.33057F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(-10.62732F, 13.82996F, 1.14351F),
                        localAngles = new Vector3(42.83438F, 0F, 0F),
                        localScale = new Vector3(1.34648F, 1.34648F, 1.34648F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "GunScope",
                        localPos = new Vector3(-0.07081F, -0.14643F, 0.17777F),
                        localAngles = new Vector3(44.5719F, 0F, 0F),
                        localScale = new Vector3(0.13531F, 0.13156F, 0.13156F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighL",
                        localPos = new Vector3(0F, 0F, -0.14146F),
                        localAngles = new Vector3(345.2758F, 49.11308F, 282.4109F),
                        localScale = new Vector3(0.10932F, 0.10932F, 0.10932F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Do Not Eat");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Chests may also contain a pearl.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "You know those little silica packets that say \"Do Not Eat\" on them? Or, should I say 'silica' at all?\n\n" +
                "You see, I'm a skeptic. When the government tells me that something is dangerous, I will do it. When the government tells me there's nothing wrong, I won't believe them. The government tells me to wear protective equipment to work, I shrug it off-- and the funny thing is, I'm usually right! When was the last time a mask or a seatbelt did anything but make you uncomfortable?\n\n" +
                "So when the government says, \"Do Not Eat\", my only logical choice is to eat. There must be something in there they don't *want* us to eat, for whatever reason... a chemical that counteracts flouridic mind control, perhaps? Something that decalcifies the pituitary gland? A cure for toxoplasmosis or even cancer? Nobody knows because nobody is willing to find out. As you know- my subscribers- that's a job for me.\n\n" +
                "I've ordered 100 individual packs of Mercurian seaweed, each of which should contain a silica packet. I'll cut them open, pour out those little mysterious orbs and season my meals with them. My workplace is sending me out to a remote system so I won't be able to post for a while, but give me just a few months and I will reward you with the truth. Watch this space...");
        }
        public static void UpdateItemStatus(Run run)
        {
            if (!DoNotEat_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Do Not Eat" + " <style=cDeath>[DISABLED]</style>");
                if (run && run.availableItems.Contains(itemDef.itemIndex)) { run.availableItems.Remove(itemDef.itemIndex); }
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Do Not Eat");
                if (run && !run.availableItems.Contains(itemDef.itemIndex) && run.IsExpansionEnabled(Hex3ModExpansion)) { run.availableItems.Add(itemDef.itemIndex); }
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", "Chests have a <style=cShrine>" + DoNotEat_PearlChancePerStack.Value + "%</style> <style=cStack>(+" + DoNotEat_PearlChancePerStack.Value + "% per stack)</style> chance to also contain a <style=cShrine>Pearl</style> or an <style=cShrine>Irradiant Pearl</style>.");
        }

        private static void AddHooks()
        {
            // Give item holders an ItemBehavior
            void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
            {
                orig(self);
                if (!self.inventory) { return; }
                int itemCount = self.inventory.GetItemCount(itemDef);
                if (itemCount > 0)
                {
                    DoNotEatBehavior component;
                    component = self.GetComponent<DoNotEatBehavior>();
                    if (!component)
                    {
                        component = self.AddItemBehavior<DoNotEatBehavior>(0);
                    }
                    component.stack = itemCount;
                }
                else
                {
                    if (self.GetComponent<DoNotEatBehavior>())
                    {
                        GameObject.Destroy(self.GetComponent<DoNotEatBehavior>());
                    }
                }
            }
            /*
            On.RoR2.ChestBehavior.ItemDrop += (orig, self) =>
            {
                orig(self);
                if (Util.GetItemCountGlobal(itemDef.itemIndex, true) > 0)
                {
                    int totalItems = Util.GetItemCountGlobal(itemDef.itemIndex, true);
                    float highestLuck = -99;
                    foreach (PlayerCharacterMasterController masterController in PlayerCharacterMasterController.instances)
                    {
                        if (masterController.master && masterController.master.GetBody() && masterController.master.GetBody().inventory)
                        {
                            if (masterController.master.luck >= highestLuck)
                            {
                                highestLuck = masterController.master.luck;
                            }
                        }
                    }
                    if (Util.CheckRoll(DoNotEat_PearlChancePerStack.Value * totalItems, highestLuck))
                    {
                        float angle = 360f / ((float)self.dropCount);
                        Vector3 vector = Vector3.up * (self.dropUpVelocityStrength / 2) + self.dropTransform.forward * 0f;
                        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
                        if (Util.CheckRoll(DoNotEat_IrradiantChance.Value))
                        {
                            PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(RoR2Content.Items.ShinyPearl.itemIndex), self.dropTransform.position + Vector3.up * 1.5f, vector);
                        }
                        else
                        {
                            PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(RoR2Content.Items.Pearl.itemIndex), self.dropTransform.position + Vector3.up * 1.5f, vector);
                        }
                    }
                }
            };
            */
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
        }

        public static void Initiate()
        {
            itemDef = CreateItem();
            ItemAPI.Add(new CustomItem(itemDef, CreateDisplayRules()));
            AddTokens();
            UpdateItemStatus(null);
            AddHooks();
        }

        public class DoNotEatBehavior : CharacterBody.ItemBehavior
        {

        }
    }
}
