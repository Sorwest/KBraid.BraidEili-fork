using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class EiliDumpPower : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("DumpPower", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "DumpPower", "name"]).Localize
        });
    }
    public override string Name() => "Dump Power";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 0;
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
                    ModEntry.Instance.KokoroApi.Actions.MakeEnergyX(new AVariableHint()),
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = c.energy * 2,
                        targetPlayer = true,
                        xHint = 1,
                    },
                    ModEntry.Instance.KokoroApi.Actions.MakeEnergy(new AStatus()
                    {
                        statusAmount = 0,
                        mode = AStatusMode.Set
                    }),
                    new AEndTurn()
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    ModEntry.Instance.KokoroApi.Actions.MakeEnergyX(new AVariableHint()),
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = c.energy * 3,
                        targetPlayer = true,
                        xHint = 3,
                    },
                    ModEntry.Instance.KokoroApi.Actions.MakeEnergy(new AStatus()
                    {
                        statusAmount = 0,
                        mode = AStatusMode.Set
                    }),
                    new AEndTurn()
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    ModEntry.Instance.KokoroApi.Actions.MakeEnergyX(new AVariableHint()),
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = c.energy * 2,
                        targetPlayer = true,
                        xHint = 1,
                    },
                    ModEntry.Instance.KokoroApi.Actions.MakeEnergy(new AStatus()
                    {
                        statusAmount = 0,
                        mode = AStatusMode.Set
                    }),
                    new AEndTurn()
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}