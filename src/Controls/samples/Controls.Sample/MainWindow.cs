using Microsoft.Maui;
using Microsoft.Extensions.DependencyInjection;

namespace Maui.Controls.Sample
{
	public class MainWindow : Window
	{
		public MainWindow()
		{
			Content = Application.Current.Services.GetRequiredService<IPage>();
		}
	}
}