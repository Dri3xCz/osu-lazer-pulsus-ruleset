using osu.Framework.Graphics.Sprites;
using osuTK;

namespace osu.Game.Rulesets.Pulsus.Objects.Drawables
{
    public partial class DrawablePulsusApproachCircle : Sprite
    {
        public void ScaleSize(Vector2 arg)
        {
            Size += arg;
        }
    }
}
