using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Hosting;

namespace Microsoft.Maui.DeviceTests
{
	public class HandlerTestFixture : IDisposable
	{
		readonly StartupStub _startup;
		ApplicationStub _application;
		IHost _host;
		IMauiContext _context;

		public HandlerTestFixture()
		{
			_startup = new StartupStub();

			var appBuilder = AppHostBuilder
				.CreateDefaultAppBuilder()
				.ConfigureFonts((ctx, fonts) =>
				{
					fonts.AddFont("dokdo_regular.ttf", "Dokdo");
				})
				.ConfigureServices((ctx, services) =>
				{
					services.AddSingleton(_context);
				});

			_startup.Configure(appBuilder);

			_host = appBuilder.Build();

			_application = new ApplicationStub();

			appBuilder.SetServiceProvider(_application);

			_context = new ContextStub(_application);
		}

		public void Dispose()
		{
			_host.Dispose();
			_host = null;

			_application.Dispose();
			_application = null;

			_context = null;
		}

		public IApplication App => _application;
	}
}