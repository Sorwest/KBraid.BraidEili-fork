using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class EiliHotwire : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Hotwire", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Hotwire", "name"]).Localize
        });
    }
    public override string Name() => "Hotwire";
    
    public override CardData GetData(State state)
    {
        return new CardData()
        {
            cost = 0,
            art = new Spr?(StableSpr.cards_Heat),
        };
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        Upgrade upgrade = this.upgrade;
        List<CardAction> actions = new List<CardAction>();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>();
                cardActionList1.Add((CardAction)new AStatus()
                {
                    status = Status.shield,
                    statusAmount = 1,
                    targetPlayer = true
                });
                cardActionList1.Add((CardAction)new AStatus()
                {
                    status = Status.heat,
                    statusAmount = 1,
                    targetPlayer = true
                });
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>();
                cardActionList2.Add((CardAction)new AStatus()
                {
                    status = Status.shield,
                    statusAmount = 2,
                    targetPlayer = true
                });
                cardActionList2.Add((CardAction)new AStatus()
                {
                    status = Status.heat,
                    statusAmount = 1,
                    targetPlayer = true
                });
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>();
                cardActionList3.Add((CardAction)new AStatus()
                {
                    status = Status.tempShield,
                    statusAmount = 3,
                    targetPlayer = true
                });
                cardActionList3.Add((CardAction)new AStatus()
                {
                    status = Status.heat,
                    statusAmount = 1,
                    targetPlayer = true
                });
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}