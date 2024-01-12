using HarmonyLib;

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
                c.QueueImmediate(new AStatus()
                {
                    status = Status.evade,
                    statusAmount = shl,
                    targetPlayer = __instance.isPlayerShip
                });
                c.QueueImmediate(new AMove()
                {
                    dir = shl,
                    isRandom = true,
                    targetPlayer = __instance.isPlayerShip
                });
                __instance.PulseStatus(ModEntry.Instance.DisabledDampeners.Status);
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
        
        c.QueueImmediate(new AStatus()
        {
            status = Status.evade,
            statusAmount = amt,
            targetPlayer = __instance.isPlayerShip
        });
        c.QueueImmediate(new AMove()
        {
            dir = amt,
            isRandom = true,
            targetPlayer = __instance.isPlayerShip
        });
        __instance.PulseStatus(ModEntry.Instance.DisabledDampeners.Status);
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