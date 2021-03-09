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
			[nameof(ILabel.Padding)] = MapPadding,
			[nameof(ILabel.TextDecorations)] = MapTextDecorations
		};

		public LabelHandler() : base(LabelMapper)
		{

		}

		public LabelHandler(PropertyMapper mapper) : base(mapper ?? LabelMapper)
		{

		}
	}
}