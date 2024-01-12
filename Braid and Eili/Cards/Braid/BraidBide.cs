using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidBide : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Bide", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Bide", "name"]).Localize
        });
    }
    public override string Name() => "Bide";

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
                num = 1;
                break;
            case Upgrade.B:
                num = 3;
                break;
        }
        data.cost = num;
        data.exhaust = upgrade == Upgrade.B ? false : true;
        data.art = new Spr?(StableSpr.cards_Scattershot);
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        var bide_status = ModEntry.Instance.Bide.Status;
        List<CardAction> actions = new()
        {
            new AStatus()
            {
                status = bide_status,
                statusAmount = 1,
                targetPlayer = true
            }
        };
        return actions;
    }
}
