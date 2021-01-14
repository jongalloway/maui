using Maui.Controls.Sample.ViewModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Extensions.DependencyInjection;

namespace Maui.Controls.Sample.Pages
{

	public class MainPage : ContentPage, IPage
	{
		MainPageViewModel _viewModel;
		public MainPage() : this(App.Current.Services.GetService<MainPageViewModel>())
		{

		}

		public MainPage(MainPageViewModel viewModel)
		{
			BindingContext = _viewModel = viewModel;
			SetupMauiLayout();
			//SetupCompatibilityLayout();
		}

		void SetupMauiLayout()
		{

			var verticalStack = new VerticalStackLayout() { Spacing = 5, BackgroundColor = Color.AntiqueWhite };
			var horizontalStack = new HorizontalStackLayout() { Spacing = 2, BackgroundColor = Color.CornflowerBlue };

			var label = new Label { Text = "This will disappear in ~5 seconds", BackgroundColor = Color.Fuchsia };
			label.Margin = new Thickness(15, 10, 20, 15);

			verticalStack.Add(label);
			verticalStack.Add(new Label { Text = "This should be BIG text!", FontSize = 24 });
			verticalStack.Add(new Label { Text = "This should be BOLD text!", FontAttributes = FontAttributes.Bold });
			verticalStack.Add(new Label { Text = "This should be a CUSTOM font!", FontFamily = "Dokdo" });
			verticalStack.Add(new Label { Text = "This should have padding", Padding = new Thickness(40), BackgroundColor = Color.LightBlue });

			var button = new Button() { Text = _viewModel.Text, WidthRequest = 200 };
			var button2 = new Button()
			{
				TextColor = Color.Green,
				Text = "Hello I'm a button",
				BackgroundColor = Color.Purple,
				Margin = new Thickness(12)
			};

			horizontalStack.Add(button);
			horizontalStack.Add(button2);
			horizontalStack.Add(new Label { Text = "And these buttons are in a HorizontalStackLayout" });

			verticalStack.Add(horizontalStack);

			verticalStack.Add(new Entry());
			verticalStack.Add(new Entry { Text = "Entry", TextColor = Color.DarkRed });
			verticalStack.Add(new Entry { IsPassword = true, TextColor = Color.Black });
			verticalStack.Add(new Entry { IsTextPredictionEnabled = false });

			verticalStack.Add(new Slider());

			verticalStack.Add(new Switch());
			verticalStack.Add(new Switch() { OnColor = Color.Green });
			verticalStack.Add(new Switch() { ThumbColor = Color.Yellow });
			verticalStack.Add(new Switch() { OnColor = Color.Green, ThumbColor = Color.Yellow });
			verticalStack.Add(new DatePicker());
			verticalStack.Add(new TimePicker());
			verticalStack.Add(new Image() { Source = "https://github.com/dotnet/maui/blob/main/src/ControlGallery/src/Xamarin.Forms.ControlGallery.Android/Resources/drawable/FlowerBuds.jpg?raw=true" });

			verticalStack.Add(CreateSampleGrid());

			Content = verticalStack;
		}

		IView CreateSampleGrid()
		{
			var layout = new Microsoft.Maui.Controls.Layout2.GridLayout() { ColumnSpacing = 5, RowSpacing = 8 };

			layout.AddRowDefinition(new RowDefinition() { Height = new GridLength(40) });
			layout.AddRowDefinition(new RowDefinition() { Height = new GridLength(40) });

			layout.AddColumnDefinition(new ColumnDefinition() { Width = new GridLength(100) });
			layout.AddColumnDefinition(new ColumnDefinition() { Width = new GridLength(100) });

			var topLeft = new Label { Text = "Top Left", BackgroundColor = Color.LightBlue };
			layout.Add(topLeft);

			var bottomLeft = new Label { Text = "Bottom Left", BackgroundColor = Color.Lavender };
			layout.Add(bottomLeft);
			layout.SetRow(bottomLeft, 1);

			var topRight = new Label { Text = "Top Right", BackgroundColor = Color.Orange };
			layout.Add(topRight);
			layout.SetColumn(topRight, 1);

			var bottomRight = new Label { Text = "Bottom Right", BackgroundColor = Color.MediumPurple };
			layout.Add(bottomRight);
			layout.SetRow(bottomRight, 1);
			layout.SetColumn(bottomRight, 1);

			layout.BackgroundColor = Color.Chartreuse;

			return layout;
		}

		void SetupCompatibilityLayout()
		{
			var verticalStack = new StackLayout() { Spacing = 5, BackgroundColor = Color.AntiqueWhite };
			var horizontalStack = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 2, BackgroundColor = Color.CornflowerBlue };

			var label = new Label { Text = "This will disappear in ~5 seconds", BackgroundColor = Color.Fuchsia };
			label.Margin = new Thickness(15, 10, 20, 15);

			verticalStack.Add(label);

			var button = new Button() { Text = _viewModel.Text, WidthRequest = 200 };
			var button2 = new Button()
			{
				TextColor = Color.Green,
				Text = "Hello I'm a button",
				BackgroundColor = Color.Purple,
				Margin = new Thickness(12)
			};

			horizontalStack.Add(button);
			horizontalStack.Add(button2);
			horizontalStack.Add(new Label { Text = "And these buttons are in a HorizontalStackLayout" });

			verticalStack.Add(horizontalStack);
			verticalStack.Add(new Slider());
			verticalStack.Add(new Switch());
			verticalStack.Add(new Switch() { OnColor = Color.Green });
			verticalStack.Add(new Switch() { ThumbColor = Color.Yellow });
			verticalStack.Add(new Switch() { OnColor = Color.Green, ThumbColor = Color.Yellow });
			verticalStack.Add(new DatePicker());
			verticalStack.Add(new TimePicker());
			verticalStack.Add(new Image()
			{
				Source =
				new UriImageSource()
				{
					Uri = new System.Uri("https://github.com/dotnet/maui/blob/main/src/ControlGallery/src/Xamarin.Forms.ControlGallery.Android/Resources/drawable/FlowerBuds.jpg?raw=true")
				}
			});

			Content = verticalStack;
		}

		public IView View { get => (IView)Content; set => Content = (View)value; }
	}
}
