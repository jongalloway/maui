namespace Microsoft.Maui
{
	public interface ILabel : IView, IText, IFont
	{
		TextDecorations TextDecorations { get; }
		Thickness Padding { get; }
	}
}