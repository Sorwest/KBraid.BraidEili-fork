using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidInductionCoils : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("InductionCoils", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "InductionCoils", "name"]).Localize
        });
    }
    public override string Name() => "Induction Coils";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 1;
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
                    new AVariableHint()
                    {
                        status = Status.heat
                    },
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = s.ship.Get(Status.heat),
                        xHint = 1,
                        targetPlayer = true
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AVariableHint()
                    {
                        status = Status.heat
                    },
                    new AAttack()
                    {
                        damage = s.ship.Get(Status.heat),
                        xHint = 1,
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AVariableHint()
                    {
                        status = Status.heat
                    },
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = s.ship.Get(Status.heat),
                        xHint = 1,
                        targetPlayer = true
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
