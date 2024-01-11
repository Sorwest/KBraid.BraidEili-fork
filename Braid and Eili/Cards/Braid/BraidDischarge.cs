using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidDischarge : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Discharge", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Discharge", "name"]).Localize
        });
    }
    public override string Name() => "Discharge";

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
                        status = Status.shield
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, s.ship.Get(Status.shield)),
                        xHint = 1,
                    },
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 0,
                        mode = AStatusMode.Set,
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
                        status = Status.shield
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 2 * s.ship.Get(Status.shield)),
                        xHint = 2,
                    },
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 0,
                        mode = AStatusMode.Set,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.maxShield,
                        statusAmount = -1,
                        targetPlayer = true
                    }
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
                    new AAttack()
                    {
                        damage = GetDmg(s, s.ship.Get(Status.tempShield)),
                        xHint = 1,
                    },
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 0,
                        mode = AStatusMode.Set,
                        targetPlayer = true
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
