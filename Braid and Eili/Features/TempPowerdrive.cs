using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;
internal sealed class TempPowerdriveManager : IStatusLogicHook
{
    public TempPowerdriveManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Card), nameof(Card.GetActualDamage)),
            postfix: new HarmonyMethod(GetType(), nameof(Card_GetActualDamage_Postfix))
        );
    }
    private static void Card_GetActualDamage_Postfix(
        State s,
        bool targetPlayer,
        ref int __result)
    {
        Ship? otherShip = s.route is Combat route ? route.otherShip : null;
        Ship? ship = targetPlayer ? otherShip : s.ship;
        if (ship != null && ship.Get(ModEntry.Instance.TempPowerdrive.Status) != 0)
            __result += ship.Get(ModEntry.Instance.TempPowerdrive.Status);
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != ModEntry.Instance.TempPowerdrive.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnEnd)
            return false;

        if (amount != 0)
            amount = 0;
        return false;
    }
}