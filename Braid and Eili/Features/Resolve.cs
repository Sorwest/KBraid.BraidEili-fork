using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;
internal sealed class ResolveManager : IStatusLogicHook
{
    public ResolveManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);

        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.DirectHullDamage)),
            prefix: new HarmonyMethod(GetType(), nameof(Ship_DirectHullDamage_Prefix)),
            postfix: new HarmonyMethod(GetType(), nameof(Ship_DirectHullDamage_Postfix))
        );
    }

    private static void Ship_DirectHullDamage_Prefix(
        Ship __instance,
        ref int amt,
        ref int __state)
    {
        if (__instance.Get(ModEntry.Instance.Resolve.Status) <= 0)
            return;
        if (amt >= __instance.hull)
        {
            __state = amt - __instance.hull;
            if (__state == 0)
                __state = 1;
            amt = __instance.hull - 1;
        }
    }

    private static void Ship_DirectHullDamage_Postfix(
        Ship __instance,
        Combat c,
        ref int amt,
        ref int __state)
    {
        if (__instance.Get(ModEntry.Instance.Resolve.Status) <= 0)
            return;
        if (__state > 0)
        {
            __instance.PulseStatus(ModEntry.Instance.Resolve.Status);
            __instance.hullMax -= __state;
            c.QueueImmediate(new AStatus()
            {
                status = ModEntry.Instance.LostHull.Status,
                statusAmount = __state,
                targetPlayer = __instance.isPlayerShip
            });
        }
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != ModEntry.Instance.DisabledDampeners.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;

        if (amount > 0)
            amount--;
        return false;
    }
}