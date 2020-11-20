using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;

namespace KK_ResourceBugs
{
    [DefOf]
    public static class KK_DefOf
    {
        public static ThingDef Megascarab;
        public static ThingDef Megaspider;

        public static BodyDef BeetleLike;
        public static BodyPartGroupDef Mouth;
        public static BodyPartGroupDef HeadAttackTool;

        public static LifeStageDef EusocialInsectLarva;
        public static LifeStageDef EusocialInsectJuvenile;
        public static LifeStageDef EusocialInsectAdult;

        public static SoundDef Pawn_Megascarab_Wounded;
        public static SoundDef Pawn_Megascarab_Death;
        public static SoundDef Pawn_Megascarab_Call;
        public static SoundDef Pawn_Megascarab_Angry;
        public static SoundDef Recipe_Smelt;

        public static ThinkTreeDef Animal;
        public static ThinkTreeDef AnimalConstant;

        public static ToolCapacityDef Bite;
        public static ToolCapacityDef Blunt;

        public static StatDef SmeltingSpeed;

        public static EffecterDef Smelt;
        
    }
}
