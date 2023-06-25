// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Pulsus.UI
{
    [Cached]
    public partial class PulsusPlayfield : Playfield
    {
        protected override GameplayCursorContainer CreateCursor() => new PulsusCursorContainer();

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRangeInternal(new Drawable[]
            {
                HitObjectContainer,
            });
        }
    }
}
