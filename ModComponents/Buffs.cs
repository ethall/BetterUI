﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using UnityEngine;
using RoR2;
using BetterUI.ModCompatibility;

namespace BetterUI
{
    public static class Buffs
    {
        static IEnumerable<CharacterBody.TimedBuff> timedBuffs;
        static CharacterBody.TimedBuff thisBuff;
        static Dictionary<BuffDef, BuffInfo> buffInfos = new Dictionary<BuffDef, BuffInfo>();

        static Buffs()
        {
            ItemCatalog.availability.CallWhenAvailable(() =>
            {
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixRed, "Blazing", "Gain power of a Blazing Elite : Leave a fire trail, and apply a percent burn on hit.");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixBlue, "Overloading", "Gain power of an Overloading Elite : Attacks explode after a delay and 50% of health replaced by shield.");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixWhite, "Glacial", "Gain power of a Glacial Elite : Leave an ice explosion on death, and apply an 80% slow on hit.");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixPoison, "Malachite", "Gain power of a Malachite Elite : Shoot occasional urchins and apply Healing disabled on hit.");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixHaunted, "Celestine", "Gain power of a Celestine Elite : Cloak nearby allies, and apply a 80% slow on hit.");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixLunar, "Perfected", "Gain power of a Perfected Elite : Cripple on hit, occasionally fire five bomb projectiles at enemies, gain 30% increased movement speed, and gain 25% max hp as well as having all health replaced with shields.");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.ArmorBoost, "Armor Boost", "'+200 Armor");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AttackSpeedOnCrit, "Attack Speed On Crit", "'+12% Attack Speed (stackable)");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.BugWings, "Bug Wings", "Sprout wings and fly, +20% Movement Speed");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Cloak, "Cloak", "Invisible");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.CloakSpeed, "Cloak Speed", "'+40% Movement Speed");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixHauntedRecipient, "Celestine Cloak", "Invisible");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.ElephantArmorBoost, "Elephant Armor Boost", "'+500 Armor");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Energized, "Energized", "'+70% Attack Speed");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.FullCrit, "Full Crit", "'+100% Critical Strike chance");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.HiddenInvincibility, "Hidden Invincibility", "Invulnerable");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Immune, "Immune", "Invulnerable");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.CrocoRegen, "Regenerative", "Gain Health Regeneration equal to 10% of your maximum health for  0.5s (stackable)");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.MedkitHeal, "Medkit Heal", "Medkit activation");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.NoCooldowns, "No Cooldowns", "Ability cooldowns reduced to 0.5 seconds");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.TeslaField, "Tesla Field", "Shooting lightning");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.TonicBuff, "Tonic Buff", "*150% Max Health , *150% Max Shield , *170% Attack Speed , *130% Movement Speed , +20 Armor , *200% Base Damage , *400% Health Regeneration");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Warbanner, "Warbanner", "'+30% Movement Speed , +30% Attack Speed");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.WarCryBuff, "War Cry Buff", "'+50% Movement Speed , +100% Attack Speed");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.TeamWarCry, "Team War Cry", "'+50% Movement Speed , +100% Attack Speed");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.WhipBoost, "Whip Boost", "'+30% Movement Speed");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.LifeSteal, "Life Steal", "Heal for 20% of the damage you deal");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.PowerBuff, "Power Buff", "'+50% Damage");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.ElementalRingsReady, "Elemental Rings Ready", "Runald's Band and/or Kjaro's Band effect can be activated");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.BanditSkull, "Bandit Skull", "'+10% Desperado damage per stack");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.LaserTurbineKillCharge, "Resonance Disc Kill Charge", "Gain a stack per kill that lasts for 7 seconds.  At 4 stacks the Resonance Disc fires, resetting all stacks.");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.OnFire, "Ignite", "Ignite: Damage over Time , Health Regen set to 0. Note: This debuff appears to stack with Burn, but actually deals damage and counts towards Death Mark separately.| (stackable)");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.OnFire, "Burn", "Burn: Damage over Time , and Health Regen set to 0. Note: This debuff appears to stack with Ignite, but actually deals damage and counts towards Death Mark separately.| (stackable)");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Bleeding, "Bleed", "Bleed: Damage over Time (stackable)");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.SuperBleed, "Hemorrhage", "Hemorrhage: Damage over Time (stackable)");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Poisoned, "Poison", "Poison: Damage over Time");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Blight, "Blight", "Blight: Damage over Time (stackable)");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.MercExpose, "Expose", "Expose: Hitting Exposed enemies reduces all skill cooldowns by 1.0s and deals an additional +350% damage");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.DeathMark, "Death Mark", "Increases damage taken by 50% from all sources for 7 (+7 per stack) seconds");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.PulverizeBuildup, "Pulverize Buildup", "Pulverize Buildup: upon getting 5 stacks, apply Pulverized (stackable)");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Pulverized, "Pulverized", "Pulverized: -60 Armor");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Cripple, "Cripple", "Cripple: -20 Armor , 50% reduced Movement Speed");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Weak, "Weak", "Weakened: -30 Armor , 40% reduced Movement Speed , 40% reduced Damage ( Natural Toxins )");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.BeetleJuice, "Beetle Juice", "-5% final Movement Speed -5% Character Damage | -5% final Attack Speed | (stackable)");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.ClayGoo, "Tar", "50% reduced Movement Speed");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Slow50, "50% Slow", "50% reduced Movement Speed");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Slow60, "60% Slow", "60% reduced Movement Speed");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Slow80, "80% Slow", "80% reduced Movement Speed");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.NullifyStack, "Nullify Stack", "Nullify Stack. Upon getting 3 stacks, apply Nullified (stackable)");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Nullified, "Nullified", "Nullified, movement speed reduced to 0");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Entangle, "Entangle", "Root: Reduce movement speed to 0");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Fruiting, "Fruiting", "Spawn 2 - 8 fruits on death");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.HealingDisabled, "Healing disabled", "Healing disabled");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.ElementalRingsCooldown, "Elemental Rings Cooldown", "Gain 10 stacks when Runald's Band or Kjaro's Band activates, lose 1 stack every second. Prevents Runald's Band or Kjaro's Band from activating. (stackable)");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.NullSafeZone, "Null Safe Zone", "Safe from Damage in the Void Fields Note: Despite the description, this is a debuff");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.PermanentCurse, "Permanent Curse", "Maximum health reduced by a factor of 1 + 0.01 * n , where n is the number of stacks. When taking damage on Eclipse 8 , allies gain a number of stacks of Permanent Curse equal to 40 * Damage / MaxHealth , rounded down. All stacks are removed at the end of each stage and when resurrecting with Dio's Best Friend , but not when using Blast Shower .| (stackable)");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Overheat, "Overheat", "Overheat: Increases fire damage received");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.LunarSecondaryRoot, "Lunar Root", "Root: Reduce movement speed to 0 for 3 (+3 per stack) seconds");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.LunarDetonationCharge, "Ruin", "Consumes Ruin stacks to deal 300% damage plus 120% damage per Ruin stack (stackable)");
            });
        }
        public static void RegisterName(BuffDef buffDef, string nameToken)
        {
            RegisterBuffInfo(buffDef, nameToken: nameToken);
        }

        public static void RegisterDescription(BuffDef buffDef, string descriptionToken)
        {
            RegisterBuffInfo(buffDef, descriptionToken: descriptionToken);
        }

        public static void RegisterBuffInfo(BuffDef buffDef, string nameToken = null, string descriptionToken = null)
        {
            buffInfos.TryGetValue(buffDef, out BuffInfo buffInfo);
            buffInfo.nameToken = buffInfo.nameToken ?? nameToken;
            buffInfo.descriptionToken = buffInfo.descriptionToken ?? descriptionToken;
            RegisterBuffInfo(buffDef, buffInfo);
        }
        public static void RegisterBuffInfo(BuffDef buffDef, BuffInfo buffInfo)
        {
            if (BetterUIPlugin.BetterAPIModIntegration)
            {
                ModCompatibility.BetterAPICompatibility.Buffs.AddInfo(buffDef, buffInfo);
                return;
            }
            buffInfos[buffDef] = buffInfo;
        }
        public static string GetName(BuffDef buffDef)
        {
            if (BetterUIPlugin.BetterAPIModIntegration)
            {
                return ModCompatibility.BetterAPICompatibility.Buffs.GetName(buffDef);
            }
            buffInfos.TryGetValue(buffDef, out BuffInfo buffInfo);
            return string.IsNullOrEmpty(buffInfo.nameToken) ? buffDef.name : RoR2.Language.GetString(buffInfo.nameToken);
        }
        public static string GetDescription(BuffDef buffDef)
        {
            if (BetterUIPlugin.BetterAPIModIntegration)
            {
                return ModCompatibility.BetterAPICompatibility.Buffs.GetDescription(buffDef);
            }
            buffInfos.TryGetValue(buffDef, out BuffInfo buffInfo);
            return string.IsNullOrEmpty(buffInfo.descriptionToken) ? String.Empty : RoR2.Language.GetString(buffInfo.descriptionToken);
        }

        internal static void Hook()
        {
            if (ConfigManager.BuffTimers.Value || ConfigManager.BuffTooltips.Value)
            {
                BetterUIPlugin.Hooks.Add<RoR2.UI.BuffIcon>("Awake", BuffIcon_Awake);
                BetterUIPlugin.Hooks.Add<RoR2.UI.BuffIcon>("UpdateIcon", BuffIcon_UpdateIcon);
            }
        }
        internal static void BuffIcon_Awake(Action<RoR2.UI.BuffIcon> orig, RoR2.UI.BuffIcon self)
        {
            orig(self);
            if (self.transform.parent.name == "BuffDisplayRoot")
            {
                if (ConfigManager.BuffTooltips.Value)
                {
                    UnityEngine.UI.GraphicRaycaster raycaster = self.transform.parent.GetComponent<UnityEngine.UI.GraphicRaycaster>();
                    if (raycaster == null)
                    {
                        self.transform.parent.gameObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();
                    }
                    self.gameObject.AddComponent<RoR2.UI.TooltipProvider>();
                }
                if (ConfigManager.BuffTimers.Value)
                {
                    GameObject TimerText = new GameObject("TimerText");
                    RectTransform timerRect = TimerText.AddComponent<RectTransform>();
                    RoR2.UI.HGTextMeshProUGUI timerTextMesh = TimerText.AddComponent<RoR2.UI.HGTextMeshProUGUI>();
                    TimerText.transform.SetParent(self.transform);

                    timerTextMesh.enableWordWrapping = false;
                    timerTextMesh.alignment = ConfigManager.BuffTimersTextAlignmentOption;
                    timerTextMesh.fontSize = ConfigManager.BuffTimersFontSize.Value;
                    timerTextMesh.faceColor = Color.white;
                    timerTextMesh.text = "";

                    timerRect.localPosition = Vector3.zero;
                    timerRect.anchorMin = new Vector2(1, 0);
                    timerRect.anchorMax = new Vector2(1, 0);
                    timerRect.localScale = Vector3.one;
                    timerRect.sizeDelta = new Vector2(48, 48);
                    timerRect.anchoredPosition = new Vector2(-24, 24);
                }
            }
        }

        internal static void BuffIcon_UpdateIcon(Action<RoR2.UI.BuffIcon> orig, RoR2.UI.BuffIcon self)
        {
            orig(self);
            if (self.buffDef && self.transform.parent.name == "BuffDisplayRoot")
            {
                if (ConfigManager.BuffTooltips.Value)
                {
                    RoR2.UI.TooltipProvider tooltipProvider = self.GetComponent<RoR2.UI.TooltipProvider>();
                    tooltipProvider.titleToken = GetName(self.buffDef);
                    tooltipProvider.bodyToken = GetDescription(self.buffDef);
                    tooltipProvider.titleColor = self.buffDef.buffColor;
                }
                if (ConfigManager.BuffTimers.Value)
                {
                    Transform timerText = self.transform.Find("TimerText");
                    if (timerText != null)
                    {
                        if (BetterUIPlugin.HUD != null)
                        {
                            CharacterBody characterBody = BetterUIPlugin.HUD.targetBodyObject ? BetterUIPlugin.HUD.targetBodyObject.GetComponent<CharacterBody>() : null;
                            if (characterBody != null && characterBody.timedBuffs.Count > 0)
                            {
                                timedBuffs = characterBody.timedBuffs.Where(b => b.buffIndex == self.buffDef.buffIndex);
                                if(timedBuffs.Any())
                                {
                                    thisBuff = timedBuffs.OrderByDescending(b => b.timer).First();
                                    timerText.GetComponent<RoR2.UI.HGTextMeshProUGUI>().text = thisBuff.timer < 10 && ConfigManager.BuffTimersDecimal.Value ? thisBuff.timer.ToString("N1") : thisBuff.timer.ToString("N0");
                                    return;
                                }
                            }
                        }
                        timerText.GetComponent<RoR2.UI.HGTextMeshProUGUI>().text = "";
                    }
                }
            }
        }
        public struct BuffInfo
        {
            public string nameToken;
            public string descriptionToken;
        }
    }
}
