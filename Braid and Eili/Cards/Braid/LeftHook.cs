using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class LeftHook : Card, IBraidCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("LeftHook", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "LeftHook", "name"]).Localize
        });
    }
    public override string Name() => "Left Hook";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 1;
        data.art = new Spr?(Spr.cards_Scattershot);
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
                cardActionList1.Add((CardAction)new AMove()
                {
                    targetPlayer = true,
                    dir = -1
                });
                cardActionList1.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                    moveEnemy = 1
                });
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>();
                cardActionList2.Add((CardAction)new AMove()
                {
                    targetPlayer = true,
                    dir = -1
                });
                cardActionList2.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 2),
                    moveEnemy = 2
                });
                cardActionList2.Add((CardAction)new AMove()
                {
                    targetPlayer = true,
                    dir = 1
                });
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>();
                cardActionList3.Add((CardAction)new AMove()
                {
                    targetPlayer = true,
                    dir = -1
                });
                cardActionList3.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                    piercing = true,
                    moveEnemy = 1
                });
                cardActionList3.Add((CardAction)new AMove()
                {
                    targetPlayer = true,
                    dir = 1
                });
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
