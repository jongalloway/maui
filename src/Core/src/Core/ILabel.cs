namespace Microsoft.Maui
{
	public interface ILabel : IView, IText, IFont
	{
		int MaxLines { get; }
		LineBreakMode LineBreakMode { get; }
		Thickness Padding { get; }
	}
}