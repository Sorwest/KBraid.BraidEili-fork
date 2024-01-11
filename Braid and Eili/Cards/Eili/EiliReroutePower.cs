using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class EiliReroutePower : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ReroutePower", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ReroutePower", "name"]).Localize
        });
    }
    public override string Name() => "ReroutePower";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 0;
        data.exhaust = true;
        data.art = ModEntry.Instance.BasicBackground.Sprite;
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
                    new AVariableHint()
                    {
                        status = Status.shield
                    },
                    new AEnergy()
                    {
                        changeAmount = s.ship.Get(Status.shield),
                        xHint = 1,
                    },
                    new AStatus()
                    {
                        status = Status.maxShield,
                        statusAmount = -1,
                        targetPlayer = true,
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AVariableHint()
                    {
                        status = Status.shield
                    },
                    new AEnergy()
                    {
                        changeAmount = s.ship.Get(Status.shield),
                        xHint = 1,
                    },
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AVariableHint()
                    {
                        status = Status.tempShield
                    },
                    new AEnergy()
                    {
                        changeAmount = s.ship.Get(Status.tempShield),
                        xHint = 1,
                    },
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}