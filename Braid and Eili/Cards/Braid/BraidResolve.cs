using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidResolve : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Resolve", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Resolve", "name"]).Localize
        });
    }
    public override string Name() => "Resolve";
    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        int num = 2;
        switch (upgrade)
        {
            case Upgrade.None:
                num = 2;
                break;
            case Upgrade.A:
                num = 3;
                break;
            case Upgrade.B:
                num = 0;
                break;
        }
        data.cost = num;
        data.exhaust = upgrade == Upgrade.A ? true : false;
        data.art = new Spr?(StableSpr.cards_Scattershot);
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new()
        {

        };
        return actions;
    }
}
