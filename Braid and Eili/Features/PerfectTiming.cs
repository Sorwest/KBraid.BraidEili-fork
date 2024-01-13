using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;
internal sealed class PerfectTimingManager : IStatusLogicHook
{
    public PerfectTimingManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Card), nameof(Card.GetActualDamage)),
            postfix: new HarmonyMethod(GetType(), nameof(Card_GetActualDamage_Postfix))
        );
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.Begin)),
            postfix: new HarmonyMethod(GetType(), nameof(AAttack_Begin_Postfix))
        );
    }
    private static void Card_GetActualDamage_Postfix(
        State s,
        bool targetPlayer,
        ref int __result)
    {
        Combat? combat = s.route is Combat route ? route : null;
        if (combat != null)
        {
            Ship ship = targetPlayer ? combat.otherShip : s.ship;
            var perfectTiming = ModEntry.Instance.PerfectTiming.Status;
            if (ship != null && ship.Get(perfectTiming) != 0)
            {
                __result += ship.Get(perfectTiming);
            }
        }
    }
    private static void AAttack_Begin_Postfix(
        AAttack __instance,
        State s,
        Combat c)
    {
        Ship source = __instance.targetPlayer ? c.otherShip : s.ship;
        if (source.Get(ModEntry.Instance.PerfectTiming.Status) != 0)
        {
            var perfectTiming = ModEntry.Instance.PerfectTiming.Status;
            c.QueueImmediate(new AStatus()
            {
                status = perfectTiming,
                statusAmount = 0,
                mode = AStatusMode.Set,
                targetPlayer = source.isPlayerShip
            });
            var bide = ModEntry.Instance.Bide.Status;
            if (source.Get(bide) != 0)
                c.QueueImmediate(new AStatus()
                {
                    status = bide,
                    statusAmount = source.Get(bide) > 0 ? -1 : 1,
                    targetPlayer = source.isPlayerShip
                });
        }
    }
}