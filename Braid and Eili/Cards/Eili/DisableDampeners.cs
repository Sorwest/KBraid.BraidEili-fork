using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class DisableDampeners : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("DisableDampeners", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "DisableDampeners", "name"]).Localize
        });
    }
    public override string Name() => "Disable Dampeners";

    public override CardData GetData(State state)
    {
        return new CardData()
        {
            cost = upgrade == Upgrade.A ? 0 : 1,
            art = ModEntry.Instance.BasicBackground.Sprite,
        };
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        var disableDampeners_status = ModEntry.Instance.DisabledDampeners.Status;
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = disableDampeners_status,
                        statusAmount = 1,
                        targetPlayer = true,
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = disableDampeners_status,
                        statusAmount = 1,
                        targetPlayer = true,
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = disableDampeners_status,
                        statusAmount = 2,
                        targetPlayer = true,
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
