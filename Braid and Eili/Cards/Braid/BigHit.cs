using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BigHit : Card, IBraidCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("BigHit", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "BigHit", "name"]).Localize
        });
    }
    public override string Name() => "Big Hit";

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
        data.art = new Spr?(Spr.cards_colorless);
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
                cardActionList1.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 2),
                });
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>();
                cardActionList2.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 3),
                });
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>();
                cardActionList3.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 5),
                });
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
