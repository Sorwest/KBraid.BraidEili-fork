using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;
internal sealed class DisabledDampenersManager : IStatusLogicHook
{
    public DisabledDampenersManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.NormalDamage)),
            prefix: new HarmonyMethod(GetType(), nameof(Ship_NormalDamage_Prefix))
        );

        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.DirectHullDamage)),
            prefix: new HarmonyMethod(GetType(), nameof(Ship_DirectHullDamage_Prefix))
        );
    }

    private static void Ship_NormalDamage_Prefix(
        Ship __instance,
        State s,
        Combat c,
        int incomingDamage,
        int? maybeWorldGridX,
        bool piercing = false)
    {
        if (__instance.Get(ModEntry.Instance.DisabledDampeners.Status) <= 0)
            return;
        if (!piercing)
        {
            var shl = __instance.Get(Status.shield) + __instance.Get(Status.tempShield);
            if (shl > 0)
            {
                var disabledDampeners = ModEntry.Instance.DisabledDampeners.Status;
                __instance.PulseStatus(disabledDampeners);
                c.QueueImmediate(new AStatus()
                {
                    status = Status.evade,
                    statusAmount = shl * __instance.Get(disabledDampeners),
                    targetPlayer = __instance.isPlayerShip
                });
                c.QueueImmediate(new AMove()
                {
                    dir = shl * __instance.Get(disabledDampeners),
                    isRandom = true,
                    targetPlayer = __instance.isPlayerShip
                });
            }
        }
        return;
    }
    private static void Ship_DirectHullDamage_Prefix(
        Ship __instance,
        State s,
        Combat c,
        int amt)
    {
        if (__instance.hull <= 0 || amt <= 0 || __instance.Get(Status.perfectShield) > 0)
            return;
        if (__instance.Get(ModEntry.Instance.DisabledDampeners.Status) <= 0)
            return;
        var disabledDampeners = ModEntry.Instance.DisabledDampeners.Status;
        __instance.PulseStatus(disabledDampeners);
        c.QueueImmediate(new AStatus()
        {
            status = Status.evade,
            statusAmount = amt * __instance.Get(disabledDampeners),
            targetPlayer = __instance.isPlayerShip
        });
        c.QueueImmediate(new AMove()
        {
            dir = amt * __instance.Get(disabledDampeners),
            isRandom = true,
            targetPlayer = __instance.isPlayerShip
        });
        return;
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