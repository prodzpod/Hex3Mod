using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using RoR2;
using R2API;
using R2API.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Hex3Mod.Items;
using Hex3Mod.Artifacts;
using Hex3Mod.Logging;
using Hex3Mod.HelperClasses;
using RiskOfOptions;
using RiskOfOptions.Options;
using RiskOfOptions.OptionConfigs;
using UnityEngine.AddressableAssets;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Hex3Mod
{
    [BepInPlugin(ModGuid, ModName, ModVer)]
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.content_management", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.items", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.language", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.prefab", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.recalculatestats", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.networking", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.unlockable", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("Hayaku.VanillaRebalance", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("HIFU.UltimateCustomRun", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(VoidItemAPI.VoidItemAPI.MODGUID)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string ModGuid = "com.Hex3.Hex3Mod";
        public const string ModName = "Hex3Mod";
        public const string ModVer = "2.0.7";

        public static RoR2.ExpansionManagement.ExpansionDef Hex3ModExpansion;

        // Common
        public static ConfigEntry<bool> ShardOfGlass_Load;
        public static ConfigEntry<bool> ShardOfGlass_Enable;
        public static ConfigEntry<float> ShardOfGlass_DamageIncrease;

        public static ConfigEntry<bool> BucketList_Load;
        public static ConfigEntry<bool> BucketList_Enable;
        public static ConfigEntry<float> BucketList_FullBuff;
        public static ConfigEntry<float> BucketList_BuffReduce;

        public static ConfigEntry<bool> HopooEgg_Load;
        public static ConfigEntry<bool> HopooEgg_Enable;
        public static ConfigEntry<float> HopooEgg_JumpModifier;

        public static ConfigEntry<bool> AtgPrototype_Load;
        public static ConfigEntry<bool> AtgPrototype_Enable;
        public static ConfigEntry<float> AtgPrototype_Damage;
        public static ConfigEntry<int> AtgPrototype_HitRequirement;

        public static ConfigEntry<bool> Tickets_Load;
        public static ConfigEntry<bool> Tickets_Enable;

        public static ConfigEntry<bool> Balance_Load;
        public static ConfigEntry<bool> Balance_Enable;
        public static ConfigEntry<float> Balance_MaxDodge;

        public static ConfigEntry<bool> MinersHelmet_Load;
        public static ConfigEntry<bool> MinersHelmet_Enable;
        public static ConfigEntry<float> MinersHelmet_CooldownReduction;
        public static ConfigEntry<int> MinersHelmet_GoldPerProc;

        // Uncommon
        public static ConfigEntry<bool> ScatteredReflection_Load;
        public static ConfigEntry<bool> ScatteredReflection_Enable;
        public static ConfigEntry<float> ScatteredReflection_DamageReflectPercent;
        public static ConfigEntry<float> ScatteredReflection_DamageReflectShardStack;
        public static ConfigEntry<float> ScatteredReflection_DamageReflectBonus;

        public static ConfigEntry<bool> Empathy_Load;
        public static ConfigEntry<bool> Empathy_Enable;
        public static ConfigEntry<float> Empathy_HealthPerHit;
        public static ConfigEntry<float> Empathy_Radius;

        public static ConfigEntry<bool> ScavengersPack_Load;
        public static ConfigEntry<bool> ScavengersPack_Enable;
        public static ConfigEntry<int> ScavengersPack_Uses;
        public static ConfigEntry<bool> ScavengersPack_RegenScrap;
        public static ConfigEntry<bool> ScavengersPack_PowerElixir;
        public static ConfigEntry<bool> ScavengersPack_DelicateWatch;
        public static ConfigEntry<bool> ScavengersPack_Dios;
        public static ConfigEntry<bool> ScavengersPack_VoidDios;
        public static ConfigEntry<bool> ScavengersPack_RustedKey;
        public static ConfigEntry<bool> ScavengersPack_EncrustedKey;
        public static ConfigEntry<bool> ScavengersPack_FourHundredTickets;
        public static ConfigEntry<bool> ScavengersPack_OneTicket;
        public static ConfigEntry<bool> ScavengersPack_ShopCard;
        public static ConfigEntry<bool> ScavengersPack_CuteBow;
        public static ConfigEntry<bool> ScavengersPack_ClockworkMechanism;
        public static ConfigEntry<bool> ScavengersPack_Vials;
        public static ConfigEntry<bool> ScavengersPack_BrokenChopsticks;
        public static ConfigEntry<bool> ScavengersPack_AbyssalCartridge;
        public static ConfigEntry<bool> ScavengersPack_Singularity;

        public static ConfigEntry<bool> TheUnforgivable_Load;
        public static ConfigEntry<bool> TheUnforgivable_Enable;
        public static ConfigEntry<float> TheUnforgivable_Interval;

        public static ConfigEntry<bool> OverkillOverdrive_Load;
        public static ConfigEntry<bool> OverkillOverdrive_Enable;
        public static ConfigEntry<bool> OverkillOverdrive_TurretBlacklist;
        public static ConfigEntry<float> OverkillOverdrive_ZoneIncrease;
        public static ConfigEntry<bool> Overkilloverdrive_EnableHoldouts;
        public static ConfigEntry<bool> Overkilloverdrive_EnableShrineWoods;
        public static ConfigEntry<bool> Overkilloverdrive_EnableFocusCrystal;
        public static ConfigEntry<bool> Overkilloverdrive_EnableBuffWards;
        public static ConfigEntry<bool> Overkilloverdrive_EnableDeskPlant;
        public static ConfigEntry<bool> Overkilloverdrive_EnableBungus;

        // Legendary
        public static ConfigEntry<bool> Apathy_Load;
        public static ConfigEntry<bool> Apathy_Enable;
        public static ConfigEntry<float> Apathy_Radius;
        public static ConfigEntry<float> Apathy_MoveSpeedAdd;
        public static ConfigEntry<float> Apathy_AttackSpeedAdd;
        public static ConfigEntry<float> Apathy_RegenAdd;
        public static ConfigEntry<float> Apathy_Duration;
        public static ConfigEntry<int> Apathy_RequiredKills;

        public static ConfigEntry<bool> MintCondition_Load;
        public static ConfigEntry<bool> MintCondition_Enable;
        public static ConfigEntry<float> MintCondition_MoveSpeed;
        public static ConfigEntry<float> MintCondition_MoveSpeedStack;
        public static ConfigEntry<int> MintCondition_AddJumps;
        public static ConfigEntry<int> MintCondition_AddJumpsStack;

        public static ConfigEntry<bool> ElderMutagen_Load;
        public static ConfigEntry<bool> ElderMutagen_Enable;
        public static ConfigEntry<int> ElderMutagen_MaxHealthFlatAdd;
        public static ConfigEntry<float> ElderMutagen_RegenAdd;

        public static ConfigEntry<bool> DoNotEat_Load;
        public static ConfigEntry<bool> DoNotEat_Enable;
        public static ConfigEntry<float> DoNotEat_PearlChancePerStack;
        public static ConfigEntry<float> DoNotEat_IrradiantChance;

        // Void
        public static ConfigEntry<bool> CorruptingParasite_Load;
        public static ConfigEntry<bool> CorruptingParasite_Enable;
        public static ConfigEntry<bool> CorruptingParasite_CorruptBossItems;
        public static ConfigEntry<int> CorruptingParasite_ItemsPerStage;

        public static ConfigEntry<bool> NoticeOfAbsence_Load;
        public static ConfigEntry<bool> NoticeOfAbsence_Enable;
        public static ConfigEntry<float> NoticeOfAbsence_InvisibilityBuff;
        public static ConfigEntry<float> NoticeOfAbsence_InvisibilityBuffStack;

        public static ConfigEntry<bool> DropOfNecrosis_Load;
        public static ConfigEntry<bool> DropOfNecrosis_Enable;
        public static ConfigEntry<float> DropOfNecrosis_Damage;
        public static ConfigEntry<float> DropOfNecrosis_DotChance;

        public static ConfigEntry<bool> CaptainsFavor_Load;
        public static ConfigEntry<bool> CaptainsFavor_Enable;
        public static ConfigEntry<float> CaptainsFavor_InteractableIncrease;

        public static ConfigEntry<bool> Discovery_Load;
        public static ConfigEntry<bool> Discovery_Enable;
        public static ConfigEntry<float> Discovery_ShieldAdd;
        public static ConfigEntry<int> Discovery_MaxStacks;

        public static ConfigEntry<bool> SpatteredCollection_Load;
        public static ConfigEntry<bool> SpatteredCollection_Enable;
        public static ConfigEntry<float> SpatteredCollection_ArmorReduction;
        public static ConfigEntry<float> SpatteredCollection_DotChance;

        public static ConfigEntry<bool> TheHermit_Load;
        public static ConfigEntry<bool> TheHermit_Enable;
        public static ConfigEntry<float> TheHermit_BuffDuration;
        public static ConfigEntry<float> TheHermit_DamageReduction;

        // Lunar
        public static ConfigEntry<bool> OneTicket_Load;
        public static ConfigEntry<bool> OneTicket_Enable;

        // Lunar Equipment
        public static ConfigEntry<bool> BloodOfTheLamb_Load;
        public static ConfigEntry<bool> BloodOfTheLamb_Enable;
        public static ConfigEntry<int> BloodOfTheLamb_ItemsTaken;

        public static bool debugMode = false; // DISABLE BEFORE BUILD

        public static AssetBundle MainAssets;

        public static Dictionary<string, string> ShaderLookup = new Dictionary<string, string>() // Strings of stubbed vs. real shaders
        {
            {"stubbed hopoo games/deferred/standard", "shaders/deferred/hgstandard"},
            {"stubbed hopoo games/fx/cloud intersection remap", "shaders/fx/hgintersectioncloudremap"},
            {"stubbed hopoo games/fx/cloud remap", "shaders/fx/hgcloudremap"},
            {"stubbed hopoo games/fx/opaque cloud remap", "shaders/fx/hgopaquecloudremap"}
        };

        public static ManualLogSource logger;

        public void Awake()
        {
            Log.Init(Logger);
            Log.LogInfo("Creating assets...");
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Hex3Mod.vfxpass2"))
            {
                MainAssets = AssetBundle.LoadFromStream(stream); // Load mainassets into stream
            }

            var materialAssets = MainAssets.LoadAllAssets<Material>();
            foreach (Material material in materialAssets)
            {
                if (!material.shader.name.StartsWith("Stubbed Hopoo Games"))
                {
                    continue;
                }
                var replacementShader = Resources.Load<Shader>(ShaderLookup[material.shader.name.ToLower()]);
                if (replacementShader)
                {
                    material.shader = replacementShader;
                }
            }

            Log.LogInfo("Creating config...");

            // Risk Of Options
            ModSettingsManager.SetModDescription("Adds 25 new items. Fully configurable, now with Risk Of Options!");
            Sprite icon = MainAssets.LoadAsset<Sprite>("Assets/VFXPASS3/Icons/icon.png");
            ModSettingsManager.SetModIcon(icon);

            // Common
            ShardOfGlass_Load = Config.Bind(new ConfigDefinition("Common - Shard Of Glass", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ShardOfGlass_Load));
            ShardOfGlass_Enable = Config.Bind(new ConfigDefinition("Common - Shard Of Glass", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ShardOfGlass_Enable));
            ShardOfGlass_DamageIncrease = Config.Bind(new ConfigDefinition("Common - Shard Of Glass", "Damage multiplier"), 0.07f, new ConfigDescription("Percentage of base damage this item adds", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(ShardOfGlass_DamageIncrease, new StepSliderConfig() { min = 0f, max = 1f, increment = 0.01f }));

            BucketList_Load = Config.Bind(new ConfigDefinition("Common - Bucket List", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(BucketList_Load));
            BucketList_Enable = Config.Bind(new ConfigDefinition("Common - Bucket List", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(BucketList_Enable));
            BucketList_FullBuff = Config.Bind(new ConfigDefinition("Common - Bucket List", "Speed multiplier"), 0.2f, new ConfigDescription("Percent speed increase", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(BucketList_FullBuff, new StepSliderConfig() { min = 0f, max = 1f, increment = 0.01f }));
            BucketList_BuffReduce = Config.Bind(new ConfigDefinition("Common - Bucket List", "Reduced buff multiplier"), 0.75f, new ConfigDescription("Multiplier subtracted from the speed buff while fighting a boss", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(BucketList_BuffReduce, new StepSliderConfig() { min = 0f, max = 1f, increment = 0.01f }));

            HopooEgg_Load = Config.Bind(new ConfigDefinition("Common - Hopoo Egg", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(HopooEgg_Load));
            HopooEgg_Enable = Config.Bind(new ConfigDefinition("Common - Hopoo Egg", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(HopooEgg_Enable));
            HopooEgg_JumpModifier = Config.Bind(new ConfigDefinition("Common - Hopoo Egg", "Jump height multiplier"), 0.2f, new ConfigDescription("Percent jump height increase", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(HopooEgg_JumpModifier, new StepSliderConfig() { min = 0f, max = 2f, increment = 0.01f }));

            AtgPrototype_Load = Config.Bind(new ConfigDefinition("Common - ATG Prototype", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(AtgPrototype_Load));
            AtgPrototype_Enable = Config.Bind(new ConfigDefinition("Common - ATG Prototype", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(AtgPrototype_Enable));
            AtgPrototype_Damage = Config.Bind(new ConfigDefinition("Common - ATG Prototype", "Damage per stack"), 0.8f, new ConfigDescription("Multiplier of base damage the missile deals per stack", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(AtgPrototype_Damage, new StepSliderConfig() { min = 0f, max = 10f, increment = 0.01f }));
            AtgPrototype_HitRequirement = Config.Bind(new ConfigDefinition("Common - ATG Prototype", "Hits required per missile"), 10, new ConfigDescription("How many hits it should take to fire each missile", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new IntSliderOption(AtgPrototype_HitRequirement, new IntSliderConfig() { min = 1, max = 50}));

            Tickets_Load = Config.Bind(new ConfigDefinition("Common - 400 Tickets", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Tickets_Load));
            Tickets_Enable = Config.Bind(new ConfigDefinition("Common - 400 Tickets", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Tickets_Enable));

            Balance_Load = Config.Bind(new ConfigDefinition("Common - Balance", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Balance_Load));
            Balance_Enable = Config.Bind(new ConfigDefinition("Common - Balance", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Balance_Enable));
            Balance_MaxDodge = Config.Bind(new ConfigDefinition("Common - Balance", "Max added dodge chance"), 20f, new ConfigDescription("Maximum (standing still) chance to dodge, stacking hyperbolically", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(Balance_MaxDodge, new StepSliderConfig() { min = 0f, max = 100f, increment = 1f }));

            MinersHelmet_Load = Config.Bind(new ConfigDefinition("Common - Miners Helmet", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(MinersHelmet_Load));
            MinersHelmet_Enable = Config.Bind(new ConfigDefinition("Common - Miners Helmet", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(MinersHelmet_Enable));
            MinersHelmet_CooldownReduction = Config.Bind(new ConfigDefinition("Common - Miners Helmet", "Cooldown reduction"), 2f, new ConfigDescription("Reduce cooldowns by this many seconds every time a small chest's worth is earned", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(MinersHelmet_CooldownReduction, new StepSliderConfig() { min = 0.1f, max = 10f, increment = 0.1f }));
            MinersHelmet_GoldPerProc = Config.Bind(new ConfigDefinition("Common - Miners Helmet", "Gold requirement"), 25, new ConfigDescription("Gold required to proc cooldown reduction (Scaling with time)", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new IntSliderOption(MinersHelmet_GoldPerProc, new IntSliderConfig() { min = 1, max = 100 }));

            // Uncommon
            ScatteredReflection_Load = Config.Bind(new ConfigDefinition("Uncommon - Scattered Reflection", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScatteredReflection_Load));
            ScatteredReflection_Enable = Config.Bind(new ConfigDefinition("Uncommon - Scattered Reflection", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScatteredReflection_Enable));
            ScatteredReflection_DamageReflectPercent = Config.Bind(new ConfigDefinition("Uncommon - Scattered Reflection", "Damage reflect value"), 0.07f, new ConfigDescription("The percent of all total received damage to be reflected", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(ScatteredReflection_DamageReflectPercent, new StepSliderConfig() { min = 0.01f, max = 1f, increment = 0.01f }));
            ScatteredReflection_DamageReflectShardStack = Config.Bind(new ConfigDefinition("Uncommon - Scattered Reflection", "Damage reflect multiplier per shard"), 0.007f, new ConfigDescription("How much of a reflection bonus each Shard Of Glass adds in percentage of total damage (Caps at 90%)", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(ScatteredReflection_DamageReflectShardStack, new StepSliderConfig() { min = 0f, max = 0.1f, increment = 0.001f }));
            ScatteredReflection_DamageReflectBonus = Config.Bind(new ConfigDefinition("Uncommon - Scattered Reflection", "Reflected damage bonus"), 7f, new ConfigDescription("Multiplier of how much bonus damage is added to the reflection", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(ScatteredReflection_DamageReflectBonus, new StepSliderConfig() { min = 0f, max = 100f, increment = 0.1f }));

            Empathy_Load = Config.Bind(new ConfigDefinition("Uncommon - Empathy", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Empathy_Load));
            Empathy_Enable = Config.Bind(new ConfigDefinition("Uncommon - Empathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Empathy_Enable));
            Empathy_HealthPerHit = Config.Bind(new ConfigDefinition("Uncommon - Empathy", "Health per hit"), 4f, new ConfigDescription("Health points restored when an enemy is hit within radius", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(Empathy_HealthPerHit, new StepSliderConfig() { min = 0f, max = 100f, increment = 0.1f }));
            Empathy_Radius = Config.Bind(new ConfigDefinition("Uncommon - Empathy", "Zone radius"), 20f, new ConfigDescription("Radius of activation zone in meters", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(Empathy_Radius, new StepSliderConfig() { min = 1f, max = 200f, increment = 1f }));

            ScavengersPack_Load = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_Load));
            ScavengersPack_Enable = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_Enable));
            ScavengersPack_Uses = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "Maximum uses"), 2, new ConfigDescription("Times the Scavenger's Pouch can be used before being emptied.", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new IntSliderOption(ScavengersPack_Uses, new IntSliderConfig() { min = 1, max = 20 }));
            ScavengersPack_RegenScrap = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLA: Regenerating Scrap"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_RegenScrap));
            ScavengersPack_PowerElixir = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLA: Power Elixir"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_PowerElixir));
            ScavengersPack_DelicateWatch = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLA: Delicate Watch"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_DelicateWatch));
            ScavengersPack_Dios = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLA: Dios Best Friend"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_Dios));
            ScavengersPack_VoidDios = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLA: Pluripotent Larva"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_VoidDios));
            ScavengersPack_RustedKey = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "WOLFO QOL: Rusted Key"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_RustedKey));
            ScavengersPack_EncrustedKey = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "WOLFO QOL: Encrusted Key"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_EncrustedKey));
            ScavengersPack_FourHundredTickets = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "HEX3MOD: 400 Tickets"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_FourHundredTickets));
            ScavengersPack_OneTicket = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "HEX3MOD: One Ticket"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_OneTicket));
            ScavengersPack_ShopCard = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "MYSTICS ITEMS: Platinum Card"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_ShopCard));
            ScavengersPack_CuteBow = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "MYSTICS ITEMS: Cutesy Bow"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_CuteBow));
            ScavengersPack_ClockworkMechanism = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLAVOID: Clockwork Mechanism"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_ClockworkMechanism));
            ScavengersPack_Vials = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLAVOID: Enhancement Vials"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_Vials));
            ScavengersPack_BrokenChopsticks = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "HOLYCRAPFORKISBACK: Sharp Chopsticks"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_BrokenChopsticks));
            ScavengersPack_AbyssalCartridge = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "SPIKESTRIP: Abyssal Cartridge"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_AbyssalCartridge));
            ScavengersPack_Singularity = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "SPIKESTRIP: Singularity"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ScavengersPack_Singularity));

            TheUnforgivable_Load = Config.Bind(new ConfigDefinition("Uncommon - The Unforgivable", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(TheUnforgivable_Load));
            TheUnforgivable_Enable = Config.Bind(new ConfigDefinition("Uncommon - The Unforgivable", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(TheUnforgivable_Enable));
            TheUnforgivable_Interval = Config.Bind(new ConfigDefinition("Uncommon - The Unforgivable", "Activation interval"), 8f, new ConfigDescription("Activate your on-kill effects every time this amount of seconds passes", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(TheUnforgivable_Interval, new StepSliderConfig() { min = 0.1f, max = 60f, increment = 0.1f }));

            OverkillOverdrive_Load = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(OverkillOverdrive_Load));
            OverkillOverdrive_Enable = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(OverkillOverdrive_Enable));
            OverkillOverdrive_TurretBlacklist = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Turret blacklist"), false, new ConfigDescription("This item has no effect if used by Engineer turrets. Disabled by default", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(OverkillOverdrive_TurretBlacklist));
            OverkillOverdrive_ZoneIncrease = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Zone size percentage increase"), 20f, new ConfigDescription("How much larger affected zones should be in percentage", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(OverkillOverdrive_ZoneIncrease, new StepSliderConfig() { min = 0f, max = 200f, increment = 1f }));
            Overkilloverdrive_EnableHoldouts = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Amplify holdout zones"), true, new ConfigDescription("e.g Teleporter, Pillars", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Overkilloverdrive_EnableHoldouts));
            Overkilloverdrive_EnableShrineWoods = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Amplify Shrine of the Woods"), true, new ConfigDescription("", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Overkilloverdrive_EnableShrineWoods));
            Overkilloverdrive_EnableFocusCrystal = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Amplify Focus Crystal"), true, new ConfigDescription("", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Overkilloverdrive_EnableFocusCrystal));
            Overkilloverdrive_EnableBuffWards = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Amplify buff wards"), true, new ConfigDescription("e.g Warbanner, Spinel Tonic", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Overkilloverdrive_EnableBuffWards));
            Overkilloverdrive_EnableDeskPlant = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Amplify Interstellar Desk Plant"), true, new ConfigDescription("", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Overkilloverdrive_EnableDeskPlant));
            Overkilloverdrive_EnableBungus = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Amplify Bustling Fungus"), true, new ConfigDescription("", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Overkilloverdrive_EnableBungus));

            // Legendary
            Apathy_Load = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Apathy_Load));
            Apathy_Enable = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Apathy_Enable));
            Apathy_Radius = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Radius"), 20f, new ConfigDescription("Radius within which kills contribute to Apathy stacks", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(Apathy_Radius, new StepSliderConfig() { min = 1f, max = 200f, increment = 1f }));
            Apathy_MoveSpeedAdd = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Buff move speed multiplier"), 1f, new ConfigDescription("Move speed multiplier added while buffed", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(Apathy_MoveSpeedAdd, new StepSliderConfig() { min = 0f, max = 100f, increment = 0.1f }));
            Apathy_AttackSpeedAdd = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Buff attack speed multiplier"), 1f, new ConfigDescription("Attack speed multiplier added while buffed", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(Apathy_AttackSpeedAdd, new StepSliderConfig() { min = 0f, max = 100f, increment = 0.1f }));
            Apathy_RegenAdd = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Buff regen added"), 0f, new ConfigDescription("Regen hp/second added while buffed", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(Apathy_RegenAdd, new StepSliderConfig() { min = 0f, max = 200f, increment = 1f }));
            Apathy_Duration = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Buff duration"), 5f, new ConfigDescription("Duration of buff", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(Apathy_Duration, new StepSliderConfig() { min = 0f, max = 60f, increment = 0.1f }));
            Apathy_RequiredKills = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Kills required for buff"), 15, new ConfigDescription("Required stacks of Apathy to trigger buff", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new IntSliderOption(Apathy_RequiredKills, new IntSliderConfig() { min = 1, max = 100 }));

            MintCondition_Load = Config.Bind(new ConfigDefinition("Legendary - Mint Condition", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(MintCondition_Load));
            MintCondition_Enable = Config.Bind(new ConfigDefinition("Legendary - Mint Condition", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(MintCondition_Enable));
            MintCondition_MoveSpeed = Config.Bind(new ConfigDefinition("Legendary - Mint Condition", "Move speed increase"), 0.2f, new ConfigDescription("Base movement speed increase", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(MintCondition_MoveSpeed, new StepSliderConfig() { min = 0f, max = 5f, increment = 0.01f }));
            MintCondition_MoveSpeedStack = Config.Bind(new ConfigDefinition("Legendary - Mint Condition", "Move speed increase per stack"), 0.6f, new ConfigDescription("Base movement speed increase per additional stack", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(MintCondition_MoveSpeedStack, new StepSliderConfig() { min = 0f, max = 5f, increment = 0.01f }));
            MintCondition_AddJumps = Config.Bind(new ConfigDefinition("Legendary - Mint Condition", "Additional jumps"), 0, new ConfigDescription("Jump count increase", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new IntSliderOption(MintCondition_AddJumps, new IntSliderConfig() { min = 0, max = 10 }));
            MintCondition_AddJumpsStack = Config.Bind(new ConfigDefinition("Legendary - Mint Condition", "Additional jumps per stack"), 2, new ConfigDescription("Jump count increase per additional stack", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new IntSliderOption(MintCondition_AddJumpsStack, new IntSliderConfig() { min = 0, max = 10 }));

            ElderMutagen_Load = Config.Bind(new ConfigDefinition("Legendary - Elder Mutagen", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ElderMutagen_Load));
            ElderMutagen_Enable = Config.Bind(new ConfigDefinition("Legendary - Elder Mutagen", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(ElderMutagen_Enable));
            ElderMutagen_MaxHealthFlatAdd = Config.Bind(new ConfigDefinition("Legendary - Elder Mutagen", "Max health per species"), 15, new ConfigDescription("Amount of max health added per killed species", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new IntSliderOption(ElderMutagen_MaxHealthFlatAdd, new IntSliderConfig() { min = 0, max = 100 }));
            ElderMutagen_RegenAdd = Config.Bind(new ConfigDefinition("Legendary - Elder Mutagen", "Regen per species"), 1f, new ConfigDescription("How much hp per second regen should be added for each killed species", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(ElderMutagen_RegenAdd, new StepSliderConfig() { min = 0.1f, max = 100f, increment = 0.1f }));

            DoNotEat_Load = Config.Bind(new ConfigDefinition("Legendary - Do Not Eat", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(DoNotEat_Load));
            DoNotEat_Enable = Config.Bind(new ConfigDefinition("Legendary - Do Not Eat", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(DoNotEat_Enable));
            DoNotEat_PearlChancePerStack = Config.Bind(new ConfigDefinition("Legendary - Do Not Eat", "Pearl Chance"), 10f, new ConfigDescription("Percent chance that a Pearl or Irradiant Pearl will drop from a chest.", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(DoNotEat_PearlChancePerStack, new StepSliderConfig() { min = 0f, max = 100f, increment = 1f }));
            DoNotEat_IrradiantChance = Config.Bind(new ConfigDefinition("Legendary - Do Not Eat", "Irradiant Pearl Chance"), 20f, new ConfigDescription("Percent chance that an Irradiant Pearl will drop instead of a Pearl.", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(DoNotEat_IrradiantChance, new StepSliderConfig() { min = 0f, max = 100f, increment = 1f }));

            /*
            TaxManStatement_Enable() { return Config.Bind<bool>(new ConfigDefinition("Legendary - The Tax Mans Statement", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>())); }
            TaxManStatement_ChanceToInflict() { return Config.Bind<float>(new ConfigDefinition("Legendary - The Tax Mans Statement", "Chance to inflict on hit"), 5f, new ConfigDescription("Percent chance that hit enemies will be taxed per stack", null, Array.Empty<object>())); }
            TaxManStatement_DamagePerTax() { return Config.Bind<float>(new ConfigDefinition("Legendary - The Tax Mans Statement", "Damage percentage per tax"), 5f, new ConfigDescription("Percent damage the enemy takes whenever they use an ability. Halved against bosses", null, Array.Empty<object>())); }
            TaxManStatement_BaseGoldPerTax() { return Config.Bind<float>(new ConfigDefinition("Legendary - The Tax Mans Statement", "Gold per tax"), 2f, new ConfigDescription("Base gold gained for each taxation, scaling with time", null, Array.Empty<object>())); }
            */

            // Void
            CorruptingParasite_Load = Config.Bind(new ConfigDefinition("Void - Corrupting Parasite", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(CorruptingParasite_Load));
            CorruptingParasite_Enable = Config.Bind(new ConfigDefinition("Void - Corrupting Parasite", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(CorruptingParasite_Enable));
            CorruptingParasite_CorruptBossItems = Config.Bind(new ConfigDefinition("Void - Corrupting Parasite", "Corrupt boss items"), false, new ConfigDescription("Allows the parasite to corrupt boss items", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(CorruptingParasite_CorruptBossItems));
            CorruptingParasite_ItemsPerStage = Config.Bind(new ConfigDefinition("Void - Corrupting Parasite", "Items per stage"), 1, new ConfigDescription("Number of items corrupted each stage", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new IntSliderOption(CorruptingParasite_ItemsPerStage, new IntSliderConfig() { min = 0, max = 20 }));

            NoticeOfAbsence_Load = Config.Bind(new ConfigDefinition("Void - Notice Of Absence", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(NoticeOfAbsence_Load));
            NoticeOfAbsence_Enable = Config.Bind(new ConfigDefinition("Void - Notice Of Absence", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(NoticeOfAbsence_Enable));
            NoticeOfAbsence_InvisibilityBuff = Config.Bind(new ConfigDefinition("Void - Notice Of Absence", "Base invisibility duration"), 10f, new ConfigDescription("How long you'll turn invisible at the start of a boss fight in seconds", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(NoticeOfAbsence_InvisibilityBuff, new StepSliderConfig() { min = 0f, max = 50f, increment = 0.1f }));
            NoticeOfAbsence_InvisibilityBuffStack = Config.Bind(new ConfigDefinition("Void - Notice Of Absence", "Invisibility duration per stack"), 5f, new ConfigDescription("How much longer you'll turn invisible at the start of a boss fight in seconds, per stack", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(NoticeOfAbsence_InvisibilityBuffStack, new StepSliderConfig() { min = 0f, max = 50f, increment = 0.1f }));

            DropOfNecrosis_Load = Config.Bind(new ConfigDefinition("Void - Drop Of Necrosis", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(DropOfNecrosis_Load));
            DropOfNecrosis_Enable = Config.Bind(new ConfigDefinition("Void - Drop Of Necrosis", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(DropOfNecrosis_Enable));
            DropOfNecrosis_Damage = Config.Bind(new ConfigDefinition("Void - Drop Of Necrosis", "Added damage per stack"), 0.05f, new ConfigDescription("What fraction of base Blight damage is added to Blight per stack.", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(DropOfNecrosis_Damage, new StepSliderConfig() { min = 0f, max = 1f, increment = 0.01f }));
            DropOfNecrosis_DotChance = Config.Bind(new ConfigDefinition("Void - Drop Of Necrosis", "Chance to inflict Blight"), 5f, new ConfigDescription("Added percent chance of inflicting Blight on hit.", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(DropOfNecrosis_DotChance, new StepSliderConfig() { min = 0f, max = 100f, increment = 1f }));

            CaptainsFavor_Load = Config.Bind(new ConfigDefinition("Void - Captains Favor", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(CaptainsFavor_Load));
            CaptainsFavor_Enable = Config.Bind(new ConfigDefinition("Void - Captains Favor", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(CaptainsFavor_Enable));
            CaptainsFavor_InteractableIncrease = Config.Bind(new ConfigDefinition("Void - Captains Favor", "Interactables increase"), 7.5f, new ConfigDescription("Percentage value that interactable credits should be increased by.", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(CaptainsFavor_InteractableIncrease, new StepSliderConfig() { min = 0f, max = 100f, increment = 0.1f }));

            Discovery_Load = Config.Bind(new ConfigDefinition("Void - Discovery", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Discovery_Load));
            Discovery_Enable = Config.Bind(new ConfigDefinition("Void - Discovery", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(Discovery_Enable));
            Discovery_ShieldAdd = Config.Bind(new ConfigDefinition("Void - Discovery", "Shield value"), 3f, new ConfigDescription("Shield added per world interactable used", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(Discovery_ShieldAdd, new StepSliderConfig() { min = 0f, max = 100f, increment = 0.1f }));
            Discovery_MaxStacks = Config.Bind(new ConfigDefinition("Void - Discovery", "Maximum uses"), 100, new ConfigDescription("Maximum interactable uses per stack before shield is no longer granted", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new IntSliderOption(Discovery_MaxStacks, new IntSliderConfig() { min = 0, max = 500 }));

            SpatteredCollection_Load = Config.Bind(new ConfigDefinition("Void - Spattered Collection", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(SpatteredCollection_Load));
            SpatteredCollection_Enable = Config.Bind(new ConfigDefinition("Void - Spattered Collection", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(SpatteredCollection_Enable));
            SpatteredCollection_ArmorReduction = Config.Bind(new ConfigDefinition("Void - Spattered Collection", "Armor reduction per stack"), 2f, new ConfigDescription("For each stack of Spattered Collection, reduce enemies' armor by this much per stack of Blight.", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(SpatteredCollection_ArmorReduction, new StepSliderConfig() { min = 0f, max = 100f, increment = 1f }));
            SpatteredCollection_DotChance = Config.Bind(new ConfigDefinition("Void - Spattered Collection", "Chance to inflict Blight"), 10f, new ConfigDescription("Added percent chance of inflicting Blight on hit.", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(SpatteredCollection_DotChance, new StepSliderConfig() { min = 0f, max = 100f, increment = 1f }));

            TheHermit_Load = Config.Bind(new ConfigDefinition("Void - The Hermit", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(TheHermit_Load));
            TheHermit_Enable = Config.Bind(new ConfigDefinition("Void - The Hermit", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(TheHermit_Enable));
            TheHermit_BuffDuration = Config.Bind(new ConfigDefinition("Void - The Hermit", "Debuff duration"), 5f, new ConfigDescription("How long in seconds the on-hit debuff should last", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(TheHermit_BuffDuration, new StepSliderConfig() { min = 0f, max = 50f, increment = 0.1f }));
            TheHermit_DamageReduction = Config.Bind(new ConfigDefinition("Void - The Hermit", "Debuff damage reduction"), 5f, new ConfigDescription("Enemy damage reduced by each debuff in percent", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new StepSliderOption(TheHermit_DamageReduction, new StepSliderConfig() { min = 0f, max = 100f, increment = 1f }));

            // Lunar
            OneTicket_Load = Config.Bind(new ConfigDefinition("Lunar - One Ticket", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(OneTicket_Load));
            OneTicket_Enable = Config.Bind(new ConfigDefinition("Lunar - One Ticket", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(OneTicket_Enable));

            // Lunar Equipment
            BloodOfTheLamb_Load = Config.Bind(new ConfigDefinition("Lunar Equipment - Blood Of The Lamb", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(BloodOfTheLamb_Load));
            BloodOfTheLamb_Enable = Config.Bind(new ConfigDefinition("Lunar Equipment - Blood Of The Lamb", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new CheckBoxOption(BloodOfTheLamb_Enable));
            BloodOfTheLamb_ItemsTaken = Config.Bind(new ConfigDefinition("Lunar Equipment - Blood Of The Lamb", "Items taken"), 6, new ConfigDescription("Amount of regular items you must sacrifice to gain a boss item", null, Array.Empty<object>()));
            ModSettingsManager.AddOption(new IntSliderOption(BloodOfTheLamb_ItemsTaken, new IntSliderConfig() { min = 0, max = 50 }));

            if (UltimateCustomRunCompatibility.enabled == true) { Log.LogInfo("Detected Ultimate Custom Run soft dependency"); }
            else { Log.LogInfo("Did not detect Ultimate Custom Run soft dependency"); }
            if (VanillaRebalanceCompatibility.enabled == true) { Log.LogInfo("Detected VanillaRebalance soft dependency"); }
            else { Log.LogInfo("Did not detect VanillaRebalance soft dependency"); }

            Log.LogInfo("Creating expansion...");
            Hex3ModExpansion = ScriptableObject.CreateInstance<RoR2.ExpansionManagement.ExpansionDef>();
            Hex3ModExpansion.name = "Hex3Mod";
            Hex3ModExpansion.nameToken = "Hex3Mod";
            Hex3ModExpansion.descriptionToken = "Adds 25 new items.";
            Hex3ModExpansion.iconSprite = MainAssets.LoadAsset<Sprite>("Assets/VFXPASS3/Icons/expansion.png");
            Hex3ModExpansion.disabledIconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texUnlockIcon.png").WaitForCompletion();
            Hex3ModExpansion.requiredEntitlement = null;
            ContentAddition.AddExpansionDef(Hex3ModExpansion);

            Log.LogInfo("Initializing items...");
            // Common
            Log.LogInfo("Common");
            if (ShardOfGlass_Load.Value == true){ ShardOfGlass.Initiate();}
            if (BucketList_Load.Value == true){ BucketList.Initiate();}
            if (HopooEgg_Load.Value == true){ HopooEgg.Initiate();}
            if (AtgPrototype_Load.Value == true){ AtgPrototype.Initiate();}
            if (Tickets_Load.Value == true){ Tickets.Initiate();}
            if (Balance_Load.Value == true) { Balance.Initiate();}
            if (MinersHelmet_Load.Value == true) { MinersHelmet.Initiate();}
            // Uncommon
            Log.LogInfo("Uncommon");
            if (ScatteredReflection_Load.Value == true){ ScatteredReflection.Initiate();}
            if (Empathy_Load.Value == true){ Empathy.Initiate();}
            if (ScavengersPack_Load.Value == true) { ScavengersPack.Initiate();}
            if (TheUnforgivable_Load.Value == true) { TheUnforgivable.Initiate();}
            if (OverkillOverdrive_Load.Value == true) { OverkillOverdrive.Initiate();}
            // Legendary
            Log.LogInfo("Legendary");
            if (Apathy_Load.Value == true){ Apathy.Initiate();}
            if (MintCondition_Load.Value == true){ MintCondition.Initiate();}
            if (ElderMutagen_Load.Value == true){ ElderMutagen.Initiate();}
            if (DoNotEat_Load.Value == true) { DoNotEat.Initiate();}
            // if (TaxManStatement_Enable().Value == true) { TaxManStatement.Initiate(TaxManStatement_ChanceToInflict().Value, TaxManStatement_DamagePerTax().Value, TaxManStatement_BaseGoldPerTax().Value); }
            // Void
            Log.LogInfo("Void");
            if (CorruptingParasite_Load.Value == true){ CorruptingParasite.Initiate(); ArtifactOfCorruption.Initiate(); }
            if (NoticeOfAbsence_Load.Value == true){ NoticeOfAbsence.Initiate(); }
            if (DropOfNecrosis_Load.Value == true) { DropOfNecrosis.Initiate(); }
            if (CaptainsFavor_Load.Value == true) { CaptainsFavor.Initiate(); }
            if (Discovery_Load.Value == true){ Discovery.Initiate(); }
            if (SpatteredCollection_Load.Value == true) { SpatteredCollection.Initiate(); }
            if (TheHermit_Load.Value == true){ TheHermit.Initiate(); }
            // Lunar
            Log.LogInfo("Lunar");
            if (OneTicket_Load.Value == true) { OneTicket.Initiate(); }
            // Lunar Equipment
            Log.LogInfo("Lunar Equipment");
            if (BloodOfTheLamb_Load.Value == true) { BloodOfTheLamb.Initiate(); }

            // Make sure disabled items are actually disabled
            bool Run_IsItemAvailable(On.RoR2.Run.orig_IsItemAvailable orig, Run self, ItemIndex itemIndex)
            {
                UpdateItemStatuses(self);
                return orig(self, itemIndex);
            }

            void Run_BuildDropTable(On.RoR2.Run.orig_BuildDropTable orig, Run self)
            {
                UpdateItemStatuses(self);
                orig(self);
            }
            On.RoR2.Run.BuildDropTable += Run_BuildDropTable;
            On.RoR2.Run.IsItemAvailable += Run_IsItemAvailable;

            void UpdateItemStatuses(Run self)
            {
                // Common
                if (ShardOfGlass_Load.Value) { ShardOfGlass.UpdateItemStatus(self); }
                if (BucketList_Load.Value) { BucketList.UpdateItemStatus(self); }
                if (HopooEgg_Load.Value) { HopooEgg.UpdateItemStatus(self); }
                if (AtgPrototype_Load.Value) { AtgPrototype.UpdateItemStatus(self); }
                if (Tickets_Load.Value) { Tickets.UpdateItemStatus(self); }
                if (Balance_Load.Value) { Balance.UpdateItemStatus(self); }
                if (MinersHelmet_Load.Value) { MinersHelmet.UpdateItemStatus(self); }
                // Uncommon
                if (ScatteredReflection_Load.Value) { ScatteredReflection.UpdateItemStatus(self); }
                if (Empathy_Load.Value) { Empathy.UpdateItemStatus(self); }
                if (ScavengersPack_Load.Value) { ScavengersPack.UpdateItemStatus(self); }
                if (TheUnforgivable_Load.Value) { TheUnforgivable.UpdateItemStatus(self); }
                if (OverkillOverdrive_Load.Value) { OverkillOverdrive.UpdateItemStatus(self); }
                // Legendary
                if (Apathy_Load.Value) { Apathy.UpdateItemStatus(self); }
                if (MintCondition_Load.Value) { MintCondition.UpdateItemStatus(self); }
                if (ElderMutagen_Load.Value) { ElderMutagen.UpdateItemStatus(self); }
                if (DoNotEat_Load.Value) { DoNotEat.UpdateItemStatus(self); }
                // Void
                if (CorruptingParasite_Load.Value) { CorruptingParasite.UpdateItemStatus(self); }
                if (NoticeOfAbsence_Load.Value) { NoticeOfAbsence.UpdateItemStatus(self); }
                if (DropOfNecrosis_Load.Value) { DropOfNecrosis.UpdateItemStatus(self); }
                if (CaptainsFavor_Load.Value) { CaptainsFavor.UpdateItemStatus(self); }
                if (SpatteredCollection_Load.Value) { SpatteredCollection.UpdateItemStatus(self); }
                if (TheHermit_Load.Value) { TheHermit.UpdateItemStatus(self); }
                // Lunar
                if (OneTicket_Load.Value) { OneTicket.UpdateItemStatus(self); }
                // Lunar Equipment
                if (BloodOfTheLamb_Load.Value) { BloodOfTheLamb.UpdateItemStatus(self); }
            }

            Log.LogInfo("Done!");
        }
    }
}
