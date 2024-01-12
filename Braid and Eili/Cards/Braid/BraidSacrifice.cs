using Nickel;
using System.Collections.Generic;
using System.Reflection;
using KBraid.BraidEili.Actions;

namespace KBraid.BraidEili.Cards;
public class BraidSacrifice : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Sacrifice", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Sacrifice", "name"]).Localize
        });
    }
    public override string Name() => "Sacrifice";
    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = upgrade == Upgrade.B ? 4 : 2;
        data.exhaust = true;
        data.art = new Spr?(StableSpr.cards_Scattershot);
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                var sacrifice_energy1 = new ASacrifice()
                {
                    destroy = false,
                    multiplier = 2
                };
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, sacrifice_energy1.myDamage)
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                var sacrifice_energy2 = new ASacrifice()
                {
                    destroy = false,
                    multiplier = 2
                };
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, sacrifice_energy2.myDamage)
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                var sacrifice_energy3 = new ASacrifice()
                {
                    destroy = true,
                    multiplier = 3
                };
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, sacrifice_energy3.myDamage)
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
