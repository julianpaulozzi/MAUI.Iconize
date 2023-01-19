using System.ComponentModel;
using Android.Content;
using Android.Views;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android.AppCompat;
using Plugin.Iconize;

[assembly: ExportRenderer(typeof(IconNavigationPage), typeof(IconNavigationRenderer))]

namespace Plugin.Iconize
{
    /// <summary>
    /// Defines the <see cref="IconNavigationPage" /> renderer.
    /// </summary>
    /// <seealso cref="NavigationPageRenderer" />
    public class IconNavigationRenderer : NavigationPageRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IconNavigationRenderer"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public IconNavigationRenderer(Context context)
            : base(context)
        {
            // Intentionally left blank
        }

        /// <inheritdoc />
        protected override void OnToolbarItemPropertyChanged(System.Object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == IconToolbarItem.IsVisibleProperty.PropertyName)
            {
                base.OnToolbarItemPropertyChanged(sender, new PropertyChangedEventArgs(nameof(MenuItem.IsEnabled)));
            }
            else
            {
                base.OnToolbarItemPropertyChanged(sender, e);
            }
        }

        /// <inheritdoc />
        protected override async void UpdateMenuItemIcon(Context context, IMenuItem menuItem, ToolbarItem toolBarItem)
        {
            if (toolBarItem is IconToolbarItem iconToolbarItem)
            {
                menuItem.SetVisible(iconToolbarItem.IsVisible);
                
                var icon = await iconToolbarItem.GetToolbarItemDrawable(context);
                if (icon is not null)
                {
                    menuItem.SetIcon(icon);
                    return;
                }
            }

            base.UpdateMenuItemIcon(context, menuItem, toolBarItem);
        }
    }
}