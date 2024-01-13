using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidMaxBlast : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("MaxBlast", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "MaxBlast", "name"]).Localize
        });
    }
    public override string Name() => "Max Blast";
    public int myDamage;
    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 0;
        data.exhaust = true;
        data.art = new Spr?(StableSpr.cards_Scattershot);
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        Upgrade upgrade = this.upgrade;
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AVariableHint()
                    {
                        hand = true,
                        handAmount = myDamage,
                        omitFromTooltips = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s,myDamage),
                        xHint = 0
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AVariableHint()
                    {
                        hand = true,
                        handAmount = myDamage,
                        omitFromTooltips = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s,myDamage),
                        piercing = true,
                        xHint = 0
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AVariableHint()
                    {
                        hand = true,
                        handAmount = myDamage,
                        omitFromTooltips = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s,myDamage),
                        stunEnemy = true,
                        xHint = 0
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
