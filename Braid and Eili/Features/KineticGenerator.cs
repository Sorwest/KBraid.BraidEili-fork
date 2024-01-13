using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBraid.BraidEili;
internal sealed class KineticGeneratorManager : IStatusLogicHook
{
    public KineticGeneratorManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AMove), nameof(AMove.Begin)),
            postfix: new HarmonyMethod(GetType(), nameof(AMove_Begin_Postfix))
        );
    }
    private static void AMove_Begin_Postfix(
        AMove __instance,
        State s,
        Combat c)
    {
        Ship ship = __instance.targetPlayer ? s.ship : c.otherShip;
        if (ship.Get(ModEntry.Instance.KineticGenerator.Status) <= 0)
            return;
        if (__instance.dir == 0)
            return;
        var move = __instance.dir > 0 ? __instance.dir : -1 * __instance.dir;
        var kineticGenerator = ModEntry.Instance.KineticGenerator.Status;
        ship.PulseStatus(kineticGenerator);
        c.QueueImmediate(new AStatus()
        {
            status = Status.tempShield,
            statusAmount = move * ship.Get(kineticGenerator),
            targetPlayer = ship.isPlayerShip
        });
        return;
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != ModEntry.Instance.KineticGenerator.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;

        if (amount > 0)
            amount--;
        return false;
    }
}