﻿using System;
using System.Collections.Specialized;
using System.Linq;
using Android.App;
using Android.Text;
using Android.Text.Style;
using AResource = Android.Resource;

namespace Microsoft.Maui.Handlers
{
	public partial class PickerHandler : AbstractViewHandler<IPicker, NativePicker>
	{
		AlertDialog? _dialog;

		protected override NativePicker CreateNativeView() =>
			new NativePicker(Context);

		protected override void ConnectHandler(NativePicker nativeView)
		{
			nativeView.FocusChange += OnFocusChange;
			nativeView.Click += OnClick;

			if (VirtualView != null && VirtualView.Items is INotifyCollectionChanged notifyCollection)
				notifyCollection.CollectionChanged += OnCollectionChanged;

			base.ConnectHandler(nativeView);
		}

		protected override void DisconnectHandler(NativePicker nativeView)
		{
			nativeView.FocusChange -= OnFocusChange;
			nativeView.Click -= OnClick;

			if (VirtualView != null && VirtualView.Items is INotifyCollectionChanged notifyCollection)
				notifyCollection.CollectionChanged -= OnCollectionChanged;

			base.DisconnectHandler(nativeView);
		}
		public static void MapTitle(PickerHandler handler, IPicker picker)
		{
			handler.TypedNativeView?.UpdateTitle(picker);
		}

		public static void MapSelectedIndex(PickerHandler handler, IPicker picker)
		{
			handler.TypedNativeView?.UpdateSelectedIndex(picker);
		}

		void OnFocusChange(object? sender, global::Android.Views.View.FocusChangeEventArgs e)
		{
			if (TypedNativeView == null)
				return;

			if (e.HasFocus)
			{
				if (TypedNativeView.Clickable)
					TypedNativeView.CallOnClick();
				else
					OnClick(TypedNativeView, EventArgs.Empty);
			}
			else if (_dialog != null)
			{
				_dialog.Hide();
				TypedNativeView.ClearFocus();
				_dialog = null;
			}
		}

		void OnClick(object? sender, EventArgs e)
		{
			if (VirtualView != null && _dialog == null)
			{
				using (var builder = new AlertDialog.Builder(Context))
				{
					builder.SetTitle(VirtualView.Title ?? string.Empty);
					
					string[] items = VirtualView.Items.ToArray();

					builder.SetItems(items, (s, e) =>
					{
						var selectedIndex = e.Which;
						VirtualView.SelectedIndex = selectedIndex;
						TypedNativeView?.UpdatePicker(VirtualView);
					});

					builder.SetNegativeButton(AResource.String.Cancel, (o, args) => { });

					_dialog = builder.Create();
				}

				if (_dialog == null)
					return;

				_dialog.SetCanceledOnTouchOutside(true);

				_dialog.DismissEvent += (sender, args) =>
				{
					_dialog.Dispose();
					_dialog = null;
				};

				_dialog.Show();
			}
		}

		void OnCollectionChanged(object? sender, EventArgs e)
		{
			if (VirtualView == null || TypedNativeView == null)
				return;

			TypedNativeView.UpdatePicker(VirtualView);
		}
	}
}