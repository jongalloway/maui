namespace Microsoft.Maui.DeviceTests.Stubs
{
	public partial class LabelStub : StubBase, ILabel
	{
		public string Text { get; set; }

		public Color TextColor { get; set; }


		public Thickness Padding { get; set; }
		public Font Font { get; set; }
	}
}