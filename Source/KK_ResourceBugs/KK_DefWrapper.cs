using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;

namespace KK_ResourceBugs
{
    [StaticConstructorOnStartup]
    static public class KK_DefWrapper
    {
        static KK_DefWrapper()
        {
            SetupWrappedDefs();
            DefDatabase<PawnKindDef>.ResolveAllReferences();
            DefDatabase<ThingDef>.ResolveAllReferences();
            DefDatabase<RecipeDef>.ResolveAllReferences();
        }

        static public void SetupWrappedDefs()
        {
            DefDatabase<ThingDef>.Add(KK_ThingDefGenerator_Bugs.ImpliedThingDefs());
            DefDatabase<PawnKindDef>.Add(KK_PawnKindGenerator_Bugs.ImpliedPawnKindDefs());

        }
    }   
}
