using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidLimiterOff : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("LimiterOff", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "LimiterOff", "name"]).Localize
        });
    }
    public override string Name() => "Limiter Off";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 1;
        data.art = new Spr?(StableSpr.cards_FumeCannon);
        data.artTint = "ff0000";
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
                cardActionList1.Add((CardAction)new AHurt()
                {
                    targetPlayer = true,
                    hurtAmount = 1
                });
                cardActionList1.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 5),
                });
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>();
                cardActionList2.Add((CardAction)new AHurt()
                {
                    targetPlayer = true,
                    hurtAmount = 1
                });
                cardActionList2.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 6),
                });
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>();
                cardActionList3.Add((CardAction)new AHurt()
                {
                    targetPlayer = true,
                    hurtAmount = 1
                });
                cardActionList3.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 5),
                    piercing = true
                });
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}