using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class EiliImprovising : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Improvising", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Improvising", "name"]).Localize
        });
    }
    public override string Name() => "Improvising";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 1;
        data.exhaust = upgrade == Upgrade.B ? false : true;
        data.retain = upgrade == Upgrade.B ? true : false;
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
                    new ADrawCard()
                    {
                        count = 2
                    },
                    new AStatus()
                    {
                        status = Status.drawNextTurn,
                        statusAmount = 2
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new ADrawCard()
                    {
                        count = 2
                    },
                    new AStatus()
                    {
                        status = Status.drawNextTurn,
                        statusAmount = 2
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new ADrawCard()
                    {
                        count = 3
                    },
                    new AStatus()
                    {
                        status = Status.drawNextTurn,
                        statusAmount = 4
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}