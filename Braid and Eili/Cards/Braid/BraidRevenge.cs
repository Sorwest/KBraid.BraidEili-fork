using Nickel;
using System.Collections.Generic;
using System.Reflection;
using KBraid.BraidEili.Actions;

namespace KBraid.BraidEili.Cards;
public class BraidRevenge : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Revenge", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Revenge", "name"]).Localize
        });
    }
    public override string Name() => "Revenge";
    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = upgrade == Upgrade.B ? 0 : 3;
        data.exhaust = upgrade == Upgrade.B ? false : true;
        data.singleUse = upgrade == Upgrade.B ? true : false;
        data.retain = upgrade == Upgrade.A ? true : false;
        data.art = new Spr?(StableSpr.cards_Scattershot);
        data.description = ModEntry.Instance.Localizations.Localize(["card", "Revenge", "description", upgrade.ToString()]);
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, s.ship.hullMax - s.ship.hull),
                        piercing = true,
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, s.ship.hullMax - s.ship.hull),
                        piercing = true,
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, s.ship.hullMax - s.ship.hull),
                        piercing = true,
                        brittle = true,
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
