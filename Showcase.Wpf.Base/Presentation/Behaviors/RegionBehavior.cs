using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Showcase.Wpf.Base.Presentation.Behaviors
{
    public class RegionBehavior : Behavior<ContentControl>
    {
        public string RegionName { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}