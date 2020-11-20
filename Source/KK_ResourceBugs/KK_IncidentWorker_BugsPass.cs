using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace KK_ResourceBugs
{
    public class KK_IncidentWorker_BugsPass : IncidentWorker
    {
        // Token: 0x06000D61 RID: 3425 RVA: 0x00062B88 File Offset: 0x00060F88
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            var map = (Map)parms.target;
            return !map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout);
        }

        readonly List<PawnKindDef> bugs = DefDatabase<PawnKindDef>.AllDefs.Where(kk => kk.defName.Contains("Bug_")).ToList();

        // Token: 0x06000D62 RID: 3426 RVA: 0x00062BB4 File Offset: 0x00060FB4
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = (Map)parms.target;
            if (!RCellFinder.TryFindRandomPawnEntryCell(out IntVec3 intVec, map, CellFinder.EdgeRoadChance_Animal + 0.2f, false, null))
            {
                return false;
            }
            var points = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, map).points;
            var num = Rand.RangeInclusive(5, 10);
            var num2 = Rand.RangeInclusive(90000, 150000);
            IntVec3 invalid = IntVec3.Invalid;
            if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(intVec, map, 10f, out invalid))
            {
                invalid = IntVec3.Invalid;
            }
            Pawn pawn = null;
            for (var i = 0; i < num; i++)
            {
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
                pawn = PawnGenerator.GeneratePawn(bugs.RandomElement(), null);
                GenSpawn.Spawn(pawn, loc, map, Rot4.Random, WipeMode.Vanish, false);
                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num2;
                if (invalid.IsValid)
                {
                    pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(invalid, map, 10, null);
                }
            }
            Find.LetterStack.ReceiveLetter("LetterLabelBugsPass".Translate(new object[]
            {
                this.def.label
            }).CapitalizeFirst(), "LetterBugsPass".Translate(new object[]
            {
                this.def.label
            }), LetterDefOf.PositiveEvent, pawn, null);
            return true;
        }
    }
}
