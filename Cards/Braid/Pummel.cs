using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class Pummel : Card, IBraidCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Pummel", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Pummel", "name"]).Localize
        });
    }
    public override string Name() => "Pummel";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 2;
        data.art = new Spr?(Spr.cards_Scattershot);
        Upgrade upgrade = this.upgrade;
        bool flag = false;
        switch (upgrade)
        {
            case Upgrade.None:
                flag = false;
                break;
            case Upgrade.A:
                flag = false;
                break;
            case Upgrade.B:
                flag = true;
                break;
        }
        data.infinite = flag;
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        Upgrade upgrade = this.upgrade;
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>();
                cardActionList1.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                });
                cardActionList1.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                });
                cardActionList1.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                });
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>();
                cardActionList2.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                    piercing = true
                });
                cardActionList2.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                    piercing = true
                });
                cardActionList2.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                    piercing = true
                });
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>();
                cardActionList3.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 4),
                });
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
