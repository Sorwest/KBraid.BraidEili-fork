using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using static HarmonyLib.Code;

namespace KBraid.BraidEili;
internal sealed class BideManager : IStatusLogicHook
{
    public BideManager()
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
        if (__instance.Get(ModEntry.Instance.PerfectTiming.Status) <= 0)
            return;
        if (!piercing)
        {
            var shl = __instance.Get(Status.shield) + __instance.Get(Status.tempShield);
            if (shl > 0)
            {
                var bide = ModEntry.Instance.Bide.Status;
                __instance.PulseStatus(bide);
                c.QueueImmediate(new AStatus()
                {
                    status = ModEntry.Instance.PerfectTiming.Status,
                    statusAmount = shl,
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
        if (__instance.Get(ModEntry.Instance.PerfectTiming.Status) <= 0)
            return;
        var bide = ModEntry.Instance.Bide.Status;
        __instance.PulseStatus(bide);
        c.QueueImmediate(new AStatus()
        {
            status = ModEntry.Instance.PerfectTiming.Status,
            statusAmount = amt,
            targetPlayer = __instance.isPlayerShip
        });
        return;
    }
    public List<Tooltip> OverrideStatusTooltips(Status status, int amount, bool isForShipStatus, List<Tooltip> tooltips)
    {
        if (status != ModEntry.Instance.Bide.Status)
            return tooltips;
        return tooltips.Concat(StatusMeta.GetTooltips(ModEntry.Instance.PerfectTiming.Status, 1)).ToList();
    }
}