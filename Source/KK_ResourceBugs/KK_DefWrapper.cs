using Verse;

namespace KK_ResourceBugs
{
    [StaticConstructorOnStartup]
    public static class KK_DefWrapper
    {
        static KK_DefWrapper()
        {
            SetupWrappedDefs();
            DefDatabase<PawnKindDef>.ResolveAllReferences();
            DefDatabase<ThingDef>.ResolveAllReferences();
            DefDatabase<RecipeDef>.ResolveAllReferences();
        }

        public static void SetupWrappedDefs()
        {
            DefDatabase<ThingDef>.Add(KK_ThingDefGenerator_Bugs.ImpliedThingDefs());
            DefDatabase<PawnKindDef>.Add(KK_PawnKindGenerator_Bugs.ImpliedPawnKindDefs());
        }
    }
}