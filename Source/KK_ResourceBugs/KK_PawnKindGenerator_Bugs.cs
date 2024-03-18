using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace KK_ResourceBugs;

//Generates variants for each terrain type that is "Diggable" and "GrowSoil"
internal static class KK_PawnKindGenerator_Bugs
{
    public static IEnumerable<PawnKindDef> ImpliedPawnKindDefs()
    {
        Log.Message("[KK]Generating pawnKind");
        var i = 0;
        //generating Foreach
        foreach (var metal in from def in DefDatabase<ThingDef>.AllDefs.ToList()
                 where def.stuffProps != null && def.stuffProps.categories.Contains(StuffCategoryDefOf.Metallic)
                 select def)
        {
            //referencing Template
            var bug = ThingDef.Named("Bug_" + metal.defName);
            var bugKind = new PawnKindDef();


            //aux floats
            var maxHitPoints = metal.stuffProps.statFactors.GetStatFactorFromList(StatDefOf.MaxHitPoints);
            var meleeWeapon_CooldownMultiplier =
                metal.stuffProps.statFactors.GetStatFactorFromList(StatDefOf.MeleeWeapon_CooldownMultiplier);
            var sharpDamageMultiplier = metal.statBases.GetStatFactorFromList(StatDefOf.SharpDamageMultiplier);
            var bluntDamageMultiplier = metal.statBases.GetStatFactorFromList(StatDefOf.BluntDamageMultiplier);


            //Defining General Value/Rarity float of generated PawnKind
            var valueMultiplier =
                (float)Math.Round(
                    maxHitPoints * sharpDamageMultiplier * bluntDamageMultiplier / meleeWeapon_CooldownMultiplier,
                    2);

            //Defining PawnKindDef
            bugKind.defName = "Bug_" + metal.defName;
            bugKind.label = "KKSc.Scarab".Translate(metal.label);
            bugKind.description = "KKSc.ScarabInfo".Translate();
            bugKind.race = bug;
            bugKind.combatPower = 40 * valueMultiplier;
            bugKind.canArriveManhunter = false;


            bugKind.lifeStages =
            [
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
                    }
                }
            ];
            yield return bugKind;
            i++;
        }

        Log.Message("[KK]Pawns generated: " + i);
    }
}