using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace KK_ResourceBugs
{
    public class KK_IncidentWorker_BugsPass : IncidentWorker
    {
        private readonly List<PawnKindDef> bugs = DefDatabase<PawnKindDef>.AllDefs
            .Where(kk => kk.defName.Contains("Bug_")).ToList();

        // Token: 0x06000D61 RID: 3425 RVA: 0x00062B88 File Offset: 0x00060F88
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            var map = (Map) parms.target;
            return !map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout);
        }

        // Token: 0x06000D62 RID: 3426 RVA: 0x00062BB4 File Offset: 0x00060FB4
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = (Map) parms.target;
            if (!RCellFinder.TryFindRandomPawnEntryCell(out var intVec, map, CellFinder.EdgeRoadChance_Animal + 0.2f))
            {
                return false;
            }

            var num = Rand.RangeInclusive(5, 10);
            var num2 = Rand.RangeInclusive(90000, 150000);
            if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(intVec, map, 10f, out var invalid))
            {
                invalid = IntVec3.Invalid;
            }

            Pawn pawn = null;
            for (var i = 0; i < num; i++)
            {
                var loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10);
                pawn = PawnGenerator.GeneratePawn(bugs.RandomElement());
                GenSpawn.Spawn(pawn, loc, map, Rot4.Random);
                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num2;
                if (invalid.IsValid)
                {
                    pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(invalid, map, 10);
                }
            }

            Find.LetterStack.ReceiveLetter("LetterLabelBugsPass".Translate(def.label).CapitalizeFirst(),
                "LetterBugsPass".Translate(def.label), LetterDefOf.PositiveEvent, pawn);
            return true;
        }
    }
}