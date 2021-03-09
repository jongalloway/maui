using System;
using Microsoft.Maui.Primitives;

namespace Microsoft.Maui
{
	public interface IView : IFrameworkElement
	{
		Thickness Margin { get; }
		LayoutOptions HorizontalLayoutOptions { get; }
	}
}