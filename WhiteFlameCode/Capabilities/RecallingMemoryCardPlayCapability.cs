using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Models.Capabilities;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Powers;

namespace WhiteFlame.WhiteFlameCode.Capabilities;

[RegisterModelCapability]
public sealed class RecallingMemoryCardPlayCapability : CardPlayCapability
{
    protected override async Task OnOwnerCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.Owner == null)
            return;

        await PowerCmd.Apply<RecallingMemoryPower>(
            choiceContext,
            base.Owner.Owner.Creature,
            1m,
            base.Owner.Owner.Creature,
            base.Owner);
    }
}
