using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using UnityEngine;
using System.Text;

namespace KK_ResourceBugs
{

    internal static class KK_ThingDefGenerator_Bugs
    {
        public static IEnumerable<ThingDef> ImpliedThingDefs()
        {
            var i = 0;
            Log.Message("[KK]Generating pawn");
            //generating Foreach
            foreach (ThingDef metal in from def in DefDatabase<ThingDef>.AllDefs.ToList()
                                       where def.stuffProps != null && def.stuffProps.categories.Contains(StuffCategoryDefOf.Metallic)
                                       select def)
            {
                //referencing Template
                ThingDef thingAnimal = KK_DefOf.Megascarab;
                ThingDef Chunk = ThingDefOf.ChunkSlagSteel;
                var bug = new ThingDef
                {
                    shortHash = (ushort)(thingAnimal.shortHash + i + 1)
                };

                //aux floats
                var maxHitPoints = metal.stuffProps.statFactors.GetStatFactorFromList(StatDefOf.MaxHitPoints);
                var marketValue = StatUtility.GetStatFactorFromList(metal.statBases, StatDefOf.MarketValue);
                var meleeWeapon_CooldownMultiplier = metal.stuffProps.statFactors.GetStatFactorFromList(StatDefOf.MeleeWeapon_CooldownMultiplier);
                var sharpDamageMultiplier = StatUtility.GetStatFactorFromList(metal.statBases, StatDefOf.SharpDamageMultiplier);
                var bluntDamageMultiplier = StatUtility.GetStatFactorFromList(metal.statBases, StatDefOf.BluntDamageMultiplier);

                //Defining ThingDef
                //BasePawn
                bug.thingClass = typeof(Pawn);
                bug.category = ThingCategory.Pawn;
                bug.selectable = true;
                bug.tickerType = TickerType.Normal;
                bug.altitudeLayer = AltitudeLayer.Pawn;
                bug.useHitPoints = false;
                bug.hasTooltip = true;
                bug.soundImpactDefault = DefDatabase<SoundDef>.GetNamed("BulletImpact_Flesh");
                StatUtility.SetStatValueInList(ref bug.statBases, StatDefOf.Mass, 70);
                bug.inspectorTabs = new List<Type>
                {
                    typeof(ITab_Pawn_Health),
                    typeof(ITab_Pawn_Needs),
                    typeof(ITab_Pawn_Character),
                    typeof(ITab_Pawn_Training),
                    typeof(ITab_Pawn_Gear),
                    typeof(ITab_Pawn_Guest),
                    typeof(ITab_Pawn_Prisoner),
                    typeof(ITab_Pawn_Social),
                    typeof(ITab_Pawn_Log)
                };
                bug.comps = new List<CompProperties>
                {
                    new CompProperties(typeof(CompAttachBase))
                };
                bug.drawGUIOverlay = true;

                //AnimalThingBase
                bug.race = thingAnimal.race;
                StatUtility.SetStatValueInList(ref bug.statBases, StatDefOf.Flammability, 1);
                StatUtility.SetStatValueInList(ref bug.statBases, StatDefOf.LeatherAmount, 0);
                bug.race.thinkTreeMain = KK_DefOf.Animal;
                bug.race.thinkTreeConstant = KK_DefOf.AnimalConstant;
                bug.race.hasGenders = false;
                bug.race.manhunterOnDamageChance = 0.2f;
                bug.race.manhunterOnTameFailChance = 0.018f;
                bug.race.nameOnNuzzleChance = 0;
                bug.race.hediffGiverSets = thingAnimal.race.hediffGiverSets;
                bug.recipes = thingAnimal.recipes;

                //AnimalThingBase
                StatUtility.SetStatValueInList(ref bug.statBases, StatDefOf.ToxicSensitivity, 0);
                bug.race.foodType = thingAnimal.race.foodType;

                //BugBase
                StatUtility.SetStatValueInList(ref bug.statBases, StatDefOf.ComfyTemperatureMin, -20);
                bug.race.body = KK_DefOf.BeetleLike;
                bug.race.useMeatFrom = KK_DefOf.Megaspider;
                bug.race.wildness = 0.95f;
                bug.race.soundMeleeHitPawn = SoundDefOf.Pawn_Melee_Punch_HitPawn;
                bug.race.soundMeleeHitBuilding = SoundDefOf.Pawn_Melee_Punch_HitBuilding;
                bug.race.soundMeleeMiss = SoundDefOf.Pawn_Melee_Punch_Miss;
                bug.tradeTags = new List<string>
                {
                    "StandardAnimal",
                    "AddedBug"
                };

                int amount()
                {
                    if (metal.smallVolume)
                    {
                        return 150;
                    }
                    else
                    {
                        return 15;
                    }
                }
                bug.defName = "Bug_" + metal.defName;
                bug.label = metal.label + " scarab";
                bug.description = "Scarab containing considerable amounts of " + metal.defName + ". Quite valuable and the metal can be extracted from it's corps";
                bug.butcherProducts = new List<ThingDefCountClass>
                {
                    new ThingDefCountClass(metal, amount())
                };

                StatUtility.SetStatValueInList(ref bug.statBases, StatDefOf.ArmorRating_Blunt, 0.3f);
                StatUtility.SetStatValueInList(ref bug.statBases, StatDefOf.ArmorRating_Sharp, 0.5f);
                StatUtility.SetStatValueInList(ref bug.statBases, StatDefOf.MarketValue, marketValue * 100f);
                StatUtility.SetStatValueInList(ref bug.statBases, StatDefOf.MoveSpeed, (float)Math.Round(4.7f / meleeWeapon_CooldownMultiplier, 3));
                bug.race.baseBodySize = 0.4f;
                bug.race.baseHungerRate = 0.2f;
                bug.race.baseHealthScale = 0.8f;
                bug.race.lifeExpectancy = 10;
                bug.tools = new List<Tool>
                {
                    new Tool
                    {
                        capacities = new List<ToolCapacityDef>
                        {
                            KK_DefOf.Bite
                        },
                        power = 7 * sharpDamageMultiplier,
                        cooldownTime = 2.5f * meleeWeapon_CooldownMultiplier,
                        linkedBodyPartsGroup = KK_DefOf.Mouth

                    },
                    new Tool
                    {
                        label = "head",
                        capacities = new List<ToolCapacityDef>
                        {
                            KK_DefOf.Blunt
                        },
                        power = 4 * bluntDamageMultiplier,
                        cooldownTime = 1.65f * meleeWeapon_CooldownMultiplier,
                        linkedBodyPartsGroup = KK_DefOf.HeadAttackTool,
                        chanceFactor = 0.2f                       
                    },
                };
                bug.race.lifeStageAges = new List<LifeStageAge>
                {
                    new LifeStageAge
                    {
                        def = KK_DefOf.EusocialInsectLarva,
                        minAge = 0
                    },
                    new LifeStageAge
                    {
                        def = KK_DefOf.EusocialInsectJuvenile,
                        minAge = 0.03f
                    },
                    new LifeStageAge
                    {
                        def = KK_DefOf.EusocialInsectAdult,
                        minAge = 0.4f,
                        soundWounded = KK_DefOf.Pawn_Megascarab_Wounded,
                        soundDeath = KK_DefOf.Pawn_Megascarab_Death,
                        soundCall = KK_DefOf.Pawn_Megascarab_Call,
                        soundAngry = KK_DefOf.Pawn_Megascarab_Angry
                    }
                };
                
                yield return bug;
                i++;
            }
            yield break;
        }
    }
}
