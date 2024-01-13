using HarmonyLib;
using KBraid.BraidEili.Actions;
namespace KBraid.BraidEili;
internal sealed class RetreatManager : IStatusLogicHook
{
    public RetreatManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
        /*ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Combat), nameof(Combat.PlayerWon)),
            prefix: new HarmonyMethod(GetType(), nameof(Combat_PlayerWon_Prefix))
        );*/
    }
    /*private static void Combat_PlayerWon_Prefix(
        Combat __instance,
        G g)
    {
        var state = g.state;
        if (state != null && state.ship.Get(ModEntry.Instance.Retreat.Status) > 0)
            __instance.noReward = true;
    }*/
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != ModEntry.Instance.Retreat.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;

        if (amount > 0)
            combat.QueueImmediate(new ARetreat());
        return false;
    }
}