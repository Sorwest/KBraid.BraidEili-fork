using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;
internal sealed class ShockAbsorberManager : IStatusLogicHook
{
    public ShockAbsorberManager()
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
        if (__instance.Get(ModEntry.Instance.ShockAbsorber.Status) <= 0)
            return;
        if (!piercing)
        {
            var shl = __instance.Get(Status.shield) + __instance.Get(Status.tempShield);
            if (shl > 0)
            {
                var shockAbsorber = ModEntry.Instance.ShockAbsorber.Status;
                __instance.PulseStatus(shockAbsorber);
                c.QueueImmediate(new AStatus()
                {
                    status = ModEntry.Instance.TempShieldNextTurn.Status,
                    statusAmount = shl * __instance.Get(shockAbsorber),
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
        if (__instance.Get(ModEntry.Instance.ShockAbsorber.Status) <= 0)
            return;
        var shockAbsorber = ModEntry.Instance.ShockAbsorber.Status;
        __instance.PulseStatus(shockAbsorber);
        c.QueueImmediate(new AStatus()
        {
            status = ModEntry.Instance.TempShieldNextTurn.Status,
            statusAmount = amt * __instance.Get(shockAbsorber),
            targetPlayer = __instance.isPlayerShip
        });
        return;
    }
    public List<Tooltip> OverrideStatusTooltips(Status status, int amount, bool isForShipStatus, List<Tooltip> tooltips)
    {
        if (status != ModEntry.Instance.ShockAbsorber.Status)
            return tooltips;
        return tooltips.Concat(StatusMeta.GetTooltips(ModEntry.Instance.TempShieldNextTurn.Status, 1)).ToList();
    }
}