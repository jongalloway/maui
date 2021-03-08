namespace Microsoft.Maui
{
	public interface ILabel : IView, IText, IFont
	{
		int MaxLines { get; }
		Thickness Padding { get; }
	}
}