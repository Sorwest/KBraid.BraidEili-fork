using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidChargeBlast : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ChargeBlast", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ChargeBlast", "name"]).Localize
        });
    }
    public override string Name() => "Charge Blast";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 2;
        data.art = new Spr?(StableSpr.cards_Scattershot);
        //data.description = ModEntry.Instance.Localizations.Localize(["card", "ChargeBlast", "description", upgrade.ToString()]);
        return data;
    }
    public int GetCardsInHand(State s) => s.route is Combat route ? route.hand.Count - 1 : 0;
    public override List<CardAction> GetActions(State s, Combat c)
    {
        var maxBlastCard = new BraidMaxBlast()
        {
            myDamage = GetCardsInHand(s),
            temporaryOverride = true,
            upgrade = this.upgrade,
        };
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AVariableHint()
                    {
                        hand = true,
                        handAmount = GetCardsInHand(s)
                    },
                    new ADiscard()
                    {
                        count = GetCardsInHand(s),
                        xHint = 1
                    },
                    new AAddCard()
                    {
                        card = maxBlastCard,
                        amount = 1,
                        destination = CardDestination.Hand
                    },
                    new AStatus()
                    {
                        status = Status.energyLessNextTurn,
                        statusAmount = 1,
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
                        hand = true,
                        handAmount = GetCardsInHand(s)
                    },
                    new AExhaustEntireHand(),
                    new AAddCard()
                    {
                        card = maxBlastCard,
                        amount = 1,
                        destination = CardDestination.Hand
                    },
                    new AStatus()
                    {
                        status = Status.energyLessNextTurn,
                        statusAmount = 1,
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
                        hand = true,
                        handAmount = GetCardsInHand(s)
                    },
                    new AExhaustEntireHand(),
                    new AAddCard()
                    {
                        card = maxBlastCard,
                        amount = 1,
                        destination = CardDestination.Hand
                    },
                    new AStatus()
                    {
                        status = Status.energyLessNextTurn,
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
