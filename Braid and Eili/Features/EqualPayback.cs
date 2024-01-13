using HarmonyLib;
using Nanoray.Shrike.Harmony;
using Nanoray.Shrike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using static HarmonyLib.Code;
using static System.Net.Mime.MediaTypeNames;

namespace KBraid.BraidEili;
internal sealed class EqualPaybackManager : IStatusLogicHook
{
    public EqualPaybackManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);

        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.Begin)),
            transpiler: new HarmonyMethod(GetType(), nameof(AAttack_Payback_Delegate_Transpiler))
        );
    }
    private static int GetEqualPaybackDmg(int amt, int incomingDamage, Ship ship)
    {
        return amt + ship.Get(ModEntry.Instance.EqualPayback.Status) * incomingDamage;
    }
    private static IEnumerable<CodeInstruction> AAttack_Payback_Delegate_Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase originalMethod)
    {
        try
        {
            return new SequenceBlockMatcher<CodeInstruction>(instructions)
                //Find  : target.Get(Status.payback) > 0 ||
                //In    : if (!this.isBeam && (target.Get(Status.payback) > 0 || target.Get(Status.tempPayback) > 0) && this.paybackCounter < 100)
                .Find(
                    ILMatches.Ldloc<Ship>(originalMethod).CreateLdlocInstruction(out var ldLocInstruction),
                    ILMatches.LdcI4((int)Status.payback),
                    ILMatches.Call("Get"),
                    ILMatches.LdcI4(0),
                    ILMatches.Bgt.GetBranchTarget(out var bgtLabel)
                )
                //Add   : target.Get(ModEntry.Instance.EqualPayback.Status) > 0 ||
                .Insert(
                    SequenceMatcherPastBoundsDirection.Before, SequenceMatcherInsertionResultingBounds.IncludingInsertion,
                    ldLocInstruction.Value,
                    new CodeInstruction(OpCodes.Ldc_I4, (int)ModEntry.Instance.EqualPayback.Status),
                    new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.Get))),
                    new CodeInstruction(OpCodes.Ldc_I4, 0),
                    new CodeInstruction(OpCodes.Bgt_S, bgtLabel.Value)
                )
                //After the stuff behind
                //Find  : + target.Get(Status.tempPayback)
                //In    : damage = Card.GetActualDamage(s, target.Get(Status.payback) + target.Get(Status.tempPayback), !this.targetPlayer),
                .Find(
                    ILMatches.Ldloc<Ship>(originalMethod).CreateLdlocInstruction(out var ldLocInstruction2),
                    ILMatches.LdcI4((int)Status.tempPayback),
                    ILMatches.Call("Get"),
                    ILMatches.Instruction(OpCodes.Add)
                )
                //Using the int result as first parameter for GetEqualPaybackDmg
                //Add this.damage and Ship to the stack as second and third parameters
                //Call GetEqualPaybackDmg and the returning int is the whole math to the stack
                // num = target.Get(Status.payback) + target.Get(Status.tempPayback) + target.Get(ModEntry.Instance.EqualPayback.Status)
                .Insert(
                    SequenceMatcherPastBoundsDirection.After, SequenceMatcherInsertionResultingBounds.IncludingInsertion,
                    new CodeInstruction(OpCodes.Ldarg, 0),
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.DeclaredField(typeof(AAttack), "damage")),
                    ldLocInstruction2.Value,
                    new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(EqualPaybackManager), nameof(EqualPaybackManager.GetEqualPaybackDmg)))
                )
                .AllElements();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Braid and Eili mod failed to patch AAttack.Begin");
            Console.WriteLine(ex);
            return instructions;
        }
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != ModEntry.Instance.EqualPayback.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;

        if (amount > 0)
            amount--;
        return false;
    }
}