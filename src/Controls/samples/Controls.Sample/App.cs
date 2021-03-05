using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using System.Diagnostics;
using Application = Microsoft.Maui.Application;

namespace Maui.Controls.Sample
{
	public class App : Application
	{
		public override IWindow CreateWindow(IActivationState state)
		{
#if (__ANDROID__ || __IOS__)
			// This will probably go into a compatibility app or window
			Microsoft.Maui.Controls.Compatibility.Forms.Init(state);
#endif
			return Services.GetRequiredService<IWindow>();
		}

		public override void OnCreated()
		{
			Debug.WriteLine("Application Created.");
		}

		public override void OnPaused()
		{
			Debug.WriteLine("Application Paused.");
		}

		public override void OnResumed()
		{
			Debug.WriteLine("Application Resumed.");
		}

		public override void OnStopped()
		{
			Debug.WriteLine("Application Stopped.");
		}
	}
}