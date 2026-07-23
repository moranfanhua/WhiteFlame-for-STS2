using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;

namespace WhiteFlame.WhiteFlameCode.Powers;

public sealed class RecallingMemoryPower : WhiteFlamePowerTemplate
{
    private const int MaxAmount = 30;

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature applier, CardModel cardSource)
    {
        // 超出上限时裁剪回 30 层
        if (power == this && base.Amount > MaxAmount)
        {
            await PowerCmd.ModifyAmount(new ThrowingPlayerChoiceContext(), this, -(base.Amount - MaxAmount), null, null);
        }
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != base.Owner.Side)
            return;

        int decrease = (int)Math.Floor(base.Amount / 3m);
        if (decrease > 0)
        {
            await PowerCmd.ModifyAmount(choiceContext, this, -decrease, null, null);
        }
    }
}
