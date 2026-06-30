using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;

namespace WhiteFlame.WhiteFlameCode.Powers;

// 一次性施加的固定层数。每次受到攻击伤害时，额外受到相当于层数的伤害。
public sealed class FutureDebtPower : WhiteFlamePowerTemplate
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeDamageReceived(PlayerChoiceContext choiceContext, Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target == base.Owner && dealer != null && props.IsPoweredAttack())
        {
            Flash();
            await CreatureCmd.Damage(choiceContext, base.Owner, base.Amount, ValueProp.Unpowered | ValueProp.SkipHurtAnim, null, null);
        }
    }
}
