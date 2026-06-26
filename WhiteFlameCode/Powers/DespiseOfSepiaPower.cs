using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;

namespace WhiteFlame.WhiteFlameCode.Powers;

public sealed class DespiseOfSepiaPower : WhiteFlamePowerTemplate
{
    private class InternalData
    {
        public int CardGeneratedCount;
        public int TriggerCount;
    }

    private const int CardGeneratedIncrement = 3;

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override int DisplayAmount => 3 - GetInternalData<InternalData>().CardGeneratedCount % 3;

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [HoverTipFactory.ForEnergy(this)];

    // 此能力使用 InitInternalData / GetInternalData，RitsuLib 会根据内部数据自动将其标记为 instanced。
    // 若仍需显式声明，可使用: public override bool IsInstanced => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(4)];

    protected override object InitInternalData()
    {
        return new InternalData();
    }

    public override async Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (creator != null && creator.Creature == base.Owner)
        {
            var data = GetInternalData<InternalData>();
            data.CardGeneratedCount++;

            int triggers = data.CardGeneratedCount / CardGeneratedIncrement - data.TriggerCount;
            if (triggers > 0)
            {
                Flash();
                await PlayerCmd.GainEnergy(2 * triggers, base.Owner.Player);
                data.TriggerCount += triggers;
            }

            InvokeDisplayAmountChanged();
        }
    }
}
