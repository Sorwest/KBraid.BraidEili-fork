using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidWindup : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Windup", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Windup", "name"]).Localize
        });
    }
    public override string Name() => "Windup";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = upgrade == Upgrade.B ? 2 : 1;
        data.infinite = upgrade == Upgrade.A ? true : false;
        data.exhaust = upgrade == Upgrade.B ? true : false;
        data.art = new Spr?(StableSpr.cards_Scattershot);
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = Status.overdrive,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.TempPowerdrive.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = Status.powerdrive,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
