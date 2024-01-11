using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class EiliShockAbsorption : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ShockAbsorption", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ShockAbsorption", "name"]).Localize
        });
    }
    public override string Name() => "Shock Absorption";

    public override CardData GetData(State state)
    {
        return new CardData()
        {
            cost = upgrade == Upgrade.A ? 2 : 3,
            art = ModEntry.Instance.BasicBackground.Sprite,
            exhaust = upgrade == Upgrade.B ? false : true,
        };
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        var shockAbsorption_status = ModEntry.Instance.ShockAbsorber.Status;
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = shockAbsorption_status,
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
                        status = shockAbsorption_status,
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
                        status = shockAbsorption_status,
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
