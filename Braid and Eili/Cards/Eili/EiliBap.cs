using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class EiliBap : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Bap", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Bap", "name"]).Localize
        });
    }
    public override string Name() => "Bap";

    public override CardData GetData(State state)
    {
        return new CardData()
        {
            cost = 0,
            art = ModEntry.Instance.BasicBackground.Sprite,
        };
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
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>();
                cardActionList2.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                });
                cardActionList2.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                });
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>();
                cardActionList3.Add((CardAction)new AAttack()
                {
                    damage = this.GetDmg(s, 1),
                    stunEnemy = true
                });
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}