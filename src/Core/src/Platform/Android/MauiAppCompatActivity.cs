using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using System;
using AndroidX.CoordinatorLayout.Widget;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Android.Content.Res;
using System.Collections.Generic;
using Android.Runtime;
using Android.Content;
using MauiApplication = Microsoft.Maui.Application;

namespace Microsoft.Maui
{
	public class MauiAppCompatActivity : AppCompatActivity
	{
		MauiApplication? _app;
		IWindow? _window;
		Bundle? _savedInstanceState;

		AndroidApplicationLifecycleState _currentState;
		AndroidApplicationLifecycleState _previousState;

		public MauiAppCompatActivity()
		{
			_previousState = AndroidApplicationLifecycleState.Uninitialized;
			_currentState = AndroidApplicationLifecycleState.Uninitialized;
		}

		protected override void OnCreate(Bundle? savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			_savedInstanceState = savedInstanceState;

			if (MauiApplication.Current == null)
				throw new InvalidOperationException($"App is not {nameof(Application)}");

			_app = MauiApplication.Current;

			if (_app == null || _app.Services == null)
				throw new InvalidOperationException("App was not intialized");

			_previousState = _currentState;
			_currentState = AndroidApplicationLifecycleState.OnCreate;

			var mauiContext = new MauiContext(_app.Services, this);
			var state = new ActivationState(mauiContext, savedInstanceState);
			_window = _app.CreateWindow(state);

			_window.MauiContext = mauiContext;

			// Hack for now we set this on the App Static but this should be on IFrameworkElement
			MauiApplication.Current?.SetHandlerContext(_window.MauiContext);

			var content = (_window.Content as IView) ??
				_window.Content?.View;

			CoordinatorLayout parent = new CoordinatorLayout(this);

			SetContentView(parent, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));

			parent.AddView(content?.ToNative(_window.MauiContext), new CoordinatorLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
		}
				
		protected override void OnStart()
		{
			base.OnStart();

			_previousState = _currentState;
			_currentState = AndroidApplicationLifecycleState.OnStart;

			UpdateApplicationLifecycleState();
		}

		protected override void OnPause()
		{
			base.OnPause();

			_previousState = _currentState;
			_currentState = AndroidApplicationLifecycleState.OnPause;

			UpdateApplicationLifecycleState();
		}

		protected override void OnResume()
		{
			base.OnResume();

			_previousState = _currentState;
			_currentState = AndroidApplicationLifecycleState.OnResume;

			UpdateApplicationLifecycleState();
		}

		protected override void OnRestart()
		{
			_previousState = _currentState;
			_currentState = AndroidApplicationLifecycleState.OnRestart;

			UpdateApplicationLifecycleState();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			_previousState = _currentState;
			_currentState = AndroidApplicationLifecycleState.OnDestroy;

			UpdateApplicationLifecycleState();
		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			base.OnSaveInstanceState(outState);

			foreach (var androidLifecycleHandler in GetAndroidLifecycleHandler())
				androidLifecycleHandler.OnSaveInstanceState(this, outState);
		}

		protected override void OnRestoreInstanceState(Bundle savedInstanceState)
		{
			base.OnRestoreInstanceState(savedInstanceState);

			foreach (var androidLifecycleHandler in GetAndroidLifecycleHandler())
				androidLifecycleHandler.OnRestoreInstanceState(this, savedInstanceState);
		}

		public override void OnConfigurationChanged(Configuration newConfig)
		{
			base.OnConfigurationChanged(newConfig);

			foreach (var androidLifecycleHandler in GetAndroidLifecycleHandler())
				androidLifecycleHandler.OnConfigurationChanged(this, newConfig);
		}

		protected override void OnActivityResult(int requestCode, [GeneratedEnum] Android.App.Result resultCode, Intent? data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			foreach (var androidLifecycleHandler in GetAndroidLifecycleHandler())
				androidLifecycleHandler.OnActivityResult(this, requestCode, resultCode, data);
		}

		void UpdateApplicationLifecycleState()
		{
			var androidLifecycleHandlers = GetAndroidLifecycleHandler();

			if (_previousState == AndroidApplicationLifecycleState.OnCreate && _currentState == AndroidApplicationLifecycleState.OnStart)
			{
				_app?.OnCreated();
				_window?.OnCreated();

				foreach (var androidLifecycleHandler in androidLifecycleHandlers)
					androidLifecycleHandler.OnCreate(this, _savedInstanceState);
			}
			else if (_previousState == AndroidApplicationLifecycleState.OnRestart && _currentState == AndroidApplicationLifecycleState.OnStart)
			{
				_app?.OnResumed();
				_window?.OnResumed();

				foreach (var androidLifecycleHandler in androidLifecycleHandlers)
					androidLifecycleHandler.OnResume(this);
			}
			else if (_previousState == AndroidApplicationLifecycleState.OnPause && _currentState == AndroidApplicationLifecycleState.OnStop)
			{
				_app?.OnPaused();
				_window?.OnPaused();

				foreach (var androidLifecycleHandler in androidLifecycleHandlers)
					androidLifecycleHandler.OnPause(this);
			}
			else if (_currentState == AndroidApplicationLifecycleState.OnDestroy)
			{
				_app?.OnStopped();
				_window?.OnStopped();

				foreach (var androidLifecycleHandler in androidLifecycleHandlers)
					androidLifecycleHandler.OnStop(this);
			}
		}

		IEnumerable<IAndroidLifecycleHandler> GetAndroidLifecycleHandler() =>
			MauiApplication.Current?.Services?.GetServices<IAndroidLifecycleHandler>() ?? Enumerable.Empty<IAndroidLifecycleHandler>();
	}
}