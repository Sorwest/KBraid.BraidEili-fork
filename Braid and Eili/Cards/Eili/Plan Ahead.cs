
using System.Collections.Generic;

#nullable enable
[CardMeta(deck = Deck.eili, rarity = Rarity.common, upgradesTo = new Upgrade[] {Upgrade.A, Upgrade.B})]
public class PlanAhead : Card
{
  public override string Name() => "Plan Ahead";

  public override CardData GetData(State state)
  {
    CardData data = new CardData();
    Upgrade upgrade = this.upgrade;
    int num;
    switch (upgrade)
    {
      case Upgrade.None:
        num = 0;
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
    data.art = new Spr?(Spr.cards_ShuffleShot);
    return data;
  }

  public override List<CardAction> GetActions(State s, Combat c)
  {
    Upgrade upgrade = this.upgrade;
    List<CardAction> actions;
    switch (upgrade)
    {
      case Upgrade.None:
        List<CardAction> cardActionList1 = new List<CardAction>();
        cardActionList1.Add((CardAction) new AStatus()
        {
          status = Status.drawNextTurn,
          statusAmount = 1,
          targetPlayer = true
        });
        actions = cardActionList1;
        break;
      case Upgrade.A:
        List<CardAction> cardActionList2 = new List<CardAction>();
        cardActionList2.Add((CardAction) new AAttack()
        {
          status = Status.drawNextTurn,
          statusAmount = 2,
          targetPlayer = true
        });
      case Upgrade.B:
        List<CardAction> cardActionList3 = new List<CardAction>();
        cardActionList3.Add((CardAction) new AAttack()
        {
          status = Status.drawNextTurn,
          statusAmount = 1,
          targetPlayer = true
        });
        cardActionList3.Add((CardAction) new AAttack()
        {
          status = Status.energyNextTurn,
          statusAmount = 1,
          targetPlayer = true
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