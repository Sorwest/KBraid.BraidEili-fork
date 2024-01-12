using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidRetreat : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Retreat", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Retreat", "name"]).Localize
        });
    }
    public override string Name() => "Retreat";
    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = upgrade == Upgrade.A ? 0 : 3;
        data.exhaust = upgrade == Upgrade.B ? true : false;
        data.singleUse = upgrade == Upgrade.B ? false : true;
        data.art = new Spr?(StableSpr.cards_Scattershot);
        data.description = ModEntry.Instance.Localizations.Localize(["card", "Retreat", "description", upgrade.ToString()]);
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
