
using System.Collections.Generic;

#nullable enable
[CardMeta(deck = Deck.braid, rarity = Rarity.common, upgradesTo = new Upgrade[] {Upgrade.A, Upgrade.B})]
public class Haymaker : Card
{
  public override string Name() => "Haymaker";

  public override CardData GetData(State state)
  {
    CardData data = new CardData();
    Upgrade upgrade = this.upgrade;
    int num;
    switch (upgrade)
    {
      case Upgrade.None:
        num = 1;
        break;
      case Upgrade.A:
        num = 1;
        break;
      case Upgrade.B:
        num = 2;
        break;
      default:
        // ISSUE: reference to a compiler-generated method
        \u003CPrivateImplementationDetails\u003E.ThrowSwitchExpressionException((object) upgrade);
        break;
    }
    data.cost = num;
      art = new Spr?(Spr.cards_colorless),
    };

  public override List<CardAction> GetActions(State s, Combat c)
  {
    Upgrade upgrade = this.upgrade;
    List<CardAction> actions;
    switch (upgrade)
    {
      case Upgrade.None:
        List<CardAction> cardActionList1 = new List<CardAction>();
        cardActionList1.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 4),
        });
        cardActionList1.Add((CardAction) new AStatus()
        {
          targetPlayer = true,
          status = Status.energyLessNextTurn,
          statusAmount = 1
        });
        actions = cardActionList1;
        break;
      case Upgrade.A:
        List<CardAction> cardActionList2 = new List<CardAction>();
        cardActionList2.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 5),
        });
        cardActionList2.Add((CardAction) new AStatus()
        {
          targetPlayer = true,
          status = Status.energyLessNextTurn,
          statusAmount = 1
        });
        actions = cardActionList2;
        break;
      case Upgrade.B:
        List<CardAction> cardActionList3 = new List<CardAction>();
        cardActionList3.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 8),
        });
        cardActionList3.Add((CardAction) new AStatus()
        {
          targetPlayer = true,
          status = Status.energyLessNextTurn,
          statusAmount = 2
        });
        actions = cardActionList3;
        break;
      default:
        // ISSUE: reference to a compiler-generated method
        \u003CPrivateImplementationDetails\u003E.ThrowSwitchExpressionException((object) upgrade);
        break;
    }
    return actions;
  }
}
