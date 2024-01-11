using KBraid.BraidEili.Actions;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class ExtraPlating : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ExtraPlating", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ExtraPlating", "name"]).Localize
        });
    }
    public override string Name() => "Extra Plating";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = upgrade == Upgrade.None ? 3 : 2;
        data.exhaust = true;
        data.art = ModEntry.Instance.BasicBackground.Sprite;
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
                    new AApplyTempArmor()
                    {
                        all = false,
                        onlyOneTurn = true,
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AApplyTempArmor
                    {
                        all = false,
                        onlyOneTurn = true,
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AApplyTempArmor
                    {
                        all = true,
                        onlyOneTurn = true,
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}