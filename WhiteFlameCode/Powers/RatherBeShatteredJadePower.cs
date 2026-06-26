using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;

namespace WhiteFlame.WhiteFlameCode.Powers;

public sealed class RatherBeShatteredJadePower : WhiteFlamePowerTemplate
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCombatVictory(CombatRoom _)
    {
        if (!base.Owner.IsDead)
        {
            Flash();
            for (int i = 0; i < base.Amount; i++)
                await CreatureCmd.Heal(base.Owner, 20m);
        }
    }

    public override Task AfterCombatEnd(CombatRoom room)
    {
        for (int i = 0; i < base.Amount; i++)
        {
            room.AddExtraReward(base.Owner.Player, new CardRemovalReward(base.Owner.Player));
        }
        return Task.CompletedTask;
    }
}
