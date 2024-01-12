using Nickel;
using System.Collections.Generic;
using System.Reflection;
using KBraid.BraidEili.Actions;

namespace KBraid.BraidEili.Cards;
public class EiliDumpCargo : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("DumpCargo", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "DumpCargo", "name"]).Localize
        });
    }
    public override string Name() => "Dump Cargo!";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 2;
        data.exhaust = upgrade == Upgrade.A ? false : true;
        data.retain = upgrade == Upgrade.B ? true : false;
        data.art = ModEntry.Instance.BasicBackground.Sprite;
        data.description = ModEntry.Instance.Localizations.Localize(["card", "DumpCargo", "description", upgrade.ToString()]);
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
                    new ALaunchMidrow()
                    {
                        MidrowType = 0
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new ALaunchMidrow()
                    {
                        MidrowType = 1
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new ALaunchMidrow()
                    {
                        MidrowType = 2
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}