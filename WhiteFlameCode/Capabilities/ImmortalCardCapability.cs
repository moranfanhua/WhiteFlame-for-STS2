using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Models.Capabilities;
using System.Threading.Tasks;

namespace WhiteFlame.WhiteFlameCode.Capabilities;

[RegisterModelCapability]
public sealed class ImmortalCardCapability : CardPlayCapability
{
    private bool _used;

    protected override Task OnOwnerCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        _used = true;
        return Task.CompletedTask;
    }

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        if (base.Owner == null || card != base.Owner)
            return;

        if (_used)
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Discard, base.Owner.Owner);
        else
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, base.Owner.Owner);
    }
}
