namespace Microsoft.Maui.Handlers
{
	public partial class LabelHandler
	{
		public static PropertyMapper<ILabel, LabelHandler> LabelMapper = new PropertyMapper<ILabel, LabelHandler>(ViewHandler.ViewMapper)
		{
			[nameof(ILabel.TextColor)] = MapTextColor,
			[nameof(ILabel.Text)] = MapText,
			[nameof(ILabel.FontFamily)] = MapFontFamily,
			[nameof(ILabel.FontSize)] = MapFontSize,
			[nameof(ILabel.FontAttributes)] = MapFontAttributes,
			[nameof(ILabel.MaxLines)] = MapMaxLines,
			[nameof(ILabel.Padding)] = MapPadding
		};

		public LabelHandler() : base(LabelMapper)
		{

		}

		public LabelHandler(PropertyMapper mapper) : base(mapper ?? LabelMapper)
		{

		}
	}
}