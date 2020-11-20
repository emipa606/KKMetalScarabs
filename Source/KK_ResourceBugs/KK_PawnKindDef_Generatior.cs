using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace KK_ResourceBugs
{

    //Generates variants for each terrain type that is "Diggable" and "GrowSoil"
    internal static class KK_PawnKindGenerator_Bugs
    {
        public static IEnumerable<PawnKindDef> ImpliedPawnKindDefs()
        {
            Log.Message("[KK]Generating pawnKind");
            var i = 0;
            //generating Foreach
            foreach (ThingDef metal in from def in DefDatabase<ThingDef>.AllDefs.ToList()
                                        where def.stuffProps != null && def.stuffProps.categories.Contains(StuffCategoryDefOf.Metallic)
                                        select def)
            {
                //referencing Template
                ThingDef thingAnimal = KK_DefOf.Megascarab;
                var bug = ThingDef.Named("Bug_" + metal.defName);
                var bugKind = new PawnKindDef();


                //aux floats
                var maxHitPoints = metal.stuffProps.statFactors.GetStatFactorFromList(StatDefOf.MaxHitPoints);
                var marketValue = StatUtility.GetStatFactorFromList(metal.statBases, StatDefOf.MarketValue);
                var meleeWeapon_CooldownMultiplier = metal.stuffProps.statFactors.GetStatFactorFromList(StatDefOf.MeleeWeapon_CooldownMultiplier);
                var sharpDamageMultiplier = StatUtility.GetStatFactorFromList(metal.statBases, StatDefOf.SharpDamageMultiplier);
                var bluntDamageMultiplier = StatUtility.GetStatFactorFromList(metal.statBases, StatDefOf.BluntDamageMultiplier);


                //Defining General Value/Rarity float of generated PawnKind
                var valueMultiplier = (float)Math.Round(maxHitPoints * sharpDamageMultiplier * bluntDamageMultiplier / meleeWeapon_CooldownMultiplier, 2);
                var drawSizeFactor = (float)Math.Round(Math.Pow(Math.Pow(0.4, 3) * 2, 1/3), 3);
                
                //Defining PawnKindDef
                bugKind.defName = "Bug_" + metal.defName;
                bugKind.label = metal.label + " scarab";
                bugKind.description = "Metal scarab. Quite valuable and it's carapace can be smelted";
                bugKind.race = bug;
                bugKind.combatPower = 40 * valueMultiplier;
                bugKind.canArriveManhunter = false;


                bugKind.lifeStages = new List<PawnKindLifeStage>
                {
                    new PawnKindLifeStage
                    {
                        bodyGraphicData = new GraphicData
                        {
                            texPath = "Things/Pawn/Animal/Megascarab/Megascarab",
                            drawSize = new Vector2(1.26f, 1.26f),
                            color = metal.stuffProps.color
                        },
                        dessicatedBodyGraphicData = new GraphicData
                        {
                            texPath = "Things/Pawn/Animal/Megascarab/Dessicated_Megascarab",
                            drawSize = new Vector2(1, 1)
                        }
                    },
                    new PawnKindLifeStage
                    {
                        bodyGraphicData = new GraphicData
                        {
                            texPath = "Things/Pawn/Animal/Megascarab/Megascarab",
                            drawSize = new Vector2(1.57f, 1.57f),
                            color = metal.stuffProps.color
                        },
                        dessicatedBodyGraphicData = new GraphicData
                        {
                            texPath = "Things/Pawn/Animal/Megascarab/Dessicated_Megascarab",
                            drawSize = new Vector2(1.13f, 1.13f)
                        }
                    },
                    new PawnKindLifeStage
                    {
                        bodyGraphicData = new GraphicData
                        {
                            texPath = "Things/Pawn/Animal/Megascarab/Megascarab",
                            drawSize = new Vector2(1.89f, 1.89f),
                            color = metal.stuffProps.color
                        },
                        dessicatedBodyGraphicData = new GraphicData
                        {
                            texPath = "Things/Pawn/Animal/Megascarab/Dessicated_Megascarab",
                            drawSize = new Vector2(1.26f, 1.26f)
                        },
                    }
                };
                yield return bugKind;
                i++;
            }
            Log.Message("[KK]Pawns generated: " + i);
            yield break;
        }
    }
}
