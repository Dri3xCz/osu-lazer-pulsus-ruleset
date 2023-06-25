// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Pulsus.Objects;
using osuTK;

namespace osu.Game.Rulesets.Pulsus.Beatmaps
{
    public class PulsusBeatmapConverter : BeatmapConverter<PulsusHitObject>
    {
        public PulsusBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
        }

        public override bool CanConvert() => Beatmap.HitObjects.All(h => h is IHasPosition);

        protected override IEnumerable<PulsusHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap, CancellationToken cancellationToken)
        {
            yield return new PulsusHitObject
            {
                Samples = original.Samples,
                StartTime = original.StartTime,
                PositionIndex = convertedPositionIndex((original as IHasPosition)?.Position ?? Vector2.Zero),
                Position = convertPosition(convertedPositionIndex((original as IHasPosition)?.Position ?? Vector2.Zero))
            };
        }

        private int convertedPositionIndex(Vector2 position)
        {
            int newIndex = 0;

            const int column_width = 200;
            const int row_width = 150;

            newIndex += (int)(position.X / column_width) + 1;
            newIndex += (2 - (int)(position.Y / row_width)) * 3;

            return newIndex;
        }

        private Vector2 convertPosition(int positionIndex)
        {
            Vector2 newPosition = Vector2.Zero;

            int row = 2 - (positionIndex - 1) / 3;
            int column = (positionIndex - 1) % 3;
            const int offset_x = 200;
            const int offset_y = 100;

            newPosition.X = 200 * column + offset_x;
            newPosition.Y = 200 * row + offset_y;

            return newPosition;
        }
    }
}
