using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidDriveby : Card , IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Driveby", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Driveby", "name"]).Localize
        });
    }
    public override string Name() => "Drive-by";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 2;
        data.art = new Spr?(StableSpr.cards_Scattershot);
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
        data.flippable = flag;
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
                    dir = 2
                });
                cardActionList1.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                });
                cardActionList1.Add((CardAction)new AMove()
                {
                    targetPlayer = true,
                    dir = 2
                });
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>();
                cardActionList2.Add((CardAction)new AMove()
                {
                    targetPlayer = true,
                    dir = 1
                });
                cardActionList2.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                });
                cardActionList2.Add((CardAction)new AMove()
                {
                    targetPlayer = true,
                    dir = 2
                });
                cardActionList2.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
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
                    dir = -2
                });
                cardActionList3.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 2),
                });
                cardActionList3.Add((CardAction)new AMove()
                {
                    targetPlayer = true,
                    dir = -2
                });
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
