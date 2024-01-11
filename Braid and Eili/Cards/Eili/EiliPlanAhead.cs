using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class EiliPlanAhead : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("PlanAhead", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "PlanAhead", "name"]).Localize
        });
    }
    public override string Name() => "Plan Ahead";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        Upgrade upgrade = this.upgrade;
        int num = 1;
        switch (upgrade)
        {
            case Upgrade.None:
                num = 1;
                break;
            case Upgrade.A:
                num = 1;
                break;
            case Upgrade.B:
                num = 2;
                break;
        }
        data.cost = num;
        data.art = new Spr?(StableSpr.cards_ShuffleShot);
        return data;
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
                    status = Status.drawNextTurn,
                    statusAmount = 2,
                    targetPlayer = true
                });
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>();
                cardActionList2.Add((CardAction)new AStatus()
                {
                    status = Status.drawNextTurn,
                    statusAmount = 3,
                    targetPlayer = true
                });
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>();
                cardActionList3.Add((CardAction)new AStatus()
                {
                    status = Status.drawNextTurn,
                    statusAmount = 2,
                    targetPlayer = true
                });
                cardActionList3.Add((CardAction)new AStatus()
                {
                    status = Status.energyNextTurn,
                    statusAmount = 1,
                    targetPlayer = true
                });
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}