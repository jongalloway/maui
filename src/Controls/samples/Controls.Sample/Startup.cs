using System;
using Maui.Controls.Sample.Pages;
using Maui.Controls.Sample.Services;
using Maui.Controls.Sample.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

//#if __ANDROID__
//using Maui.Controls.Compatibility;
//#endif

namespace Maui.Controls.Sample
{
	public class Startup : IStartup
	{
		public void Configure(IAppHostBuilder appBuilder)
		{
			appBuilder
				#if __ANDROID__
				// These only work on NET6
				// .RegisterCompatibilityRenderer<Microsoft.Maui.Controls.ContentPage, Microsoft.Maui.Controls.Compatibility.Platform.Android.PageRenderer>()
				// .RegisterCompatibilityRenderer<Microsoft.Maui.Controls.Button, Microsoft.Maui.Controls.Compatibility.Platform.Android.FastRenderers.ButtonRenderer>()
				#endif
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.AddConsole();
				})
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					config.AddInMemoryCollection(new Dictionary<string, string>
					{
						{"MyKey", "Dictionary MyKey Value"},
						{":Title", "Dictionary_Title"},
						{"Position:Name", "Dictionary_Name" },
						{"Logging:LogLevel:Default", "Warning"}
					});
				})
				.UseServiceProviderFactory(new DIExtensionsServiceProviderFactory())
				.ConfigureServices((hostingContext, services) =>
				{
					services.AddSingleton<ITextService, TextService>();
					services.AddTransient<MainPageViewModel>();
					if (UseXamlPage)
						services.AddTransient<IPage, XamlPage>();
					else
						services.AddTransient<IPage, MainPage>();
					services.AddTransient<IWindow, MainWindow>();
#if __ANDROID__
			services.AddTransient<IAndroidLifecycleHandler, CustomAndroidLifecycleHandler>();
#endif

#if __IOS__
			services.AddTransient<IIosApplicationDelegateHandler, CustomIosLifecycleHandler>();
#endif
				})
				.ConfigureFonts((hostingContext, fonts) =>
				{
					fonts.AddFont("dokdo_regular.ttf", "Dokdo");
				});
#if __ANDROID__
				//.RegisterCompatibilityRenderer<Microsoft.Maui.Controls.Button, Microsoft.Maui.Controls.Button, Microsoft.Maui.Controls.Platform.Android.FastRenderers.ButtonRenderer>()
#endif
		}

		// To use DI ServiceCollection and not the MAUI one
		public class DIExtensionsServiceProviderFactory : IServiceProviderFactory<ServiceCollection>
		{
			public ServiceCollection CreateBuilder(IServiceCollection services)
				=> new ServiceCollection { services };

			public IServiceProvider CreateServiceProvider(ServiceCollection containerBuilder)
				=> containerBuilder.BuildServiceProvider();
		}
	}
}
