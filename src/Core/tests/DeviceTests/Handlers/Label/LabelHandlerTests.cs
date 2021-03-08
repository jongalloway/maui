using System.Threading.Tasks;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Handlers;
using Xunit;

namespace Microsoft.Maui.DeviceTests
{
	[Category(TestCategory.Label)]
	public partial class LabelHandlerTests : HandlerTestBase<LabelHandler>
	{
		public LabelHandlerTests(HandlerTestFixture fixture) : base(fixture)
		{
		}

		[Fact(DisplayName = "Background Color Initializes Correctly")]
		public async Task BackgroundColorInitializesCorrectly()
		{
			var label = new LabelStub()
			{
				BackgroundColor = Color.Blue,
				Text = "Test"
			};

			await ValidateNativeBackgroundColor(label, Color.Blue);
		}

		[Fact(DisplayName = "Text Initializes Correctly")]
		public async Task TextInitializesCorrectly()
		{
			var label = new LabelStub()
			{
				Text = "Test"
			};

			await ValidatePropertyInitValue(label, () => label.Text, GetNativeText, label.Text);
		}

		[Fact(DisplayName = "Text Color Initializes Correctly")]
		public async Task TextColorInitializesCorrectly()
		{
			var label = new LabelStub()
			{
				Text = "Test",
				TextColor = Color.Red
			};

			await ValidatePropertyInitValue(label, () => label.TextColor, GetNativeTextColor, label.TextColor);
		}

		[Theory(DisplayName = "Font Size Initializes Correctly")]
		[InlineData(1)]
		[InlineData(10)]
		[InlineData(20)]
		[InlineData(100)]
		public async Task FontSizeInitializesCorrectly(int fontSize)
		{
			var label = new LabelStub()
			{
				Text = "Test",
				FontSize = fontSize
			};

			await ValidatePropertyInitValue(label, () => label.FontSize, GetNativeUnscaledFontSize, label.FontSize);
		}

		[Theory(DisplayName = "Font Attributes Initialize Correctly")]
		[InlineData(FontAttributes.None, false, false)]
		[InlineData(FontAttributes.Bold, true, false)]
		[InlineData(FontAttributes.Italic, false, true)]
		[InlineData(FontAttributes.Bold | FontAttributes.Italic, true, true)]
		public async Task AttributesInitializeCorrectly(FontAttributes attributes, bool isBold, bool isItalic)
		{
			var label = new LabelStub()
			{
				Text = "Test",
				FontAttributes = attributes
			};

			await ValidatePropertyInitValue(label, () => label.FontAttributes.HasFlag(FontAttributes.Bold), GetNativeIsBold, isBold);
			await ValidatePropertyInitValue(label, () => label.FontAttributes.HasFlag(FontAttributes.Italic), GetNativeIsItalic, isItalic);
		}

        [Fact(DisplayName = "LineBreakMode Initializes Correctly")]
        public async Task LineBreakModeInitializesCorrectly()
        {
            var xplatLineBreakMode = LineBreakMode.TailTruncation;

            var label = new LabelStub()
            {
                LineBreakMode = xplatLineBreakMode
            };

            var expectedValue = xplatLineBreakMode.ToNative();

            var values = await GetValueAsync(label, (handler) =>
            {
                return new
                {
                    ViewValue = label.LineBreakMode,
                    NativeViewValue = GetNativeLineBreakMode(handler)
                };
            });

            Assert.Equal(xplatLineBreakMode, values.ViewValue);
            Assert.Equal(expectedValue, values.NativeViewValue);
        }

        [Fact]
        public async Task LineBreakModeDoesNotAffectMaxLines()
        {
            var label = new LabelStub()
            {
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                MaxLines = 3,
                LineBreakMode = LineBreakMode.WordWrap,
            };

            var handler = await CreateHandlerAsync(label);
            var nativeLabel = GetNativeLabel(handler);

            await InvokeOnMainThreadAsync(() =>
            {
                Assert.Equal(3, GetNativeMaxLines(handler));
                Assert.Equal(LineBreakMode.WordWrap.ToNative(), GetNativeLineBreakMode(handler));

                label.LineBreakMode = LineBreakMode.CharacterWrap;
                nativeLabel.UpdateLineBreakMode(label);

                Assert.Equal(3, GetNativeMaxLines(handler));
                Assert.Equal(LineBreakMode.CharacterWrap.ToNative(), GetNativeLineBreakMode(handler));
            });
        }

        [Fact]
        public async Task SingleLineBreakModeChangesMaxLines()
        {
            var label = new LabelStub()
            {
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                MaxLines = 3,
                LineBreakMode = LineBreakMode.WordWrap,
            };

            var handler = await CreateHandlerAsync(label);
            var nativeLabel = GetNativeLabel(handler);

            await InvokeOnMainThreadAsync(() =>
            {
                Assert.Equal(3, GetNativeMaxLines(handler));
                Assert.Equal(LineBreakMode.WordWrap.ToNative(), GetNativeLineBreakMode(handler));

                label.LineBreakMode = LineBreakMode.HeadTruncation;
                nativeLabel.UpdateLineBreakMode(label);

                Assert.Equal(1, GetNativeMaxLines(handler));
                Assert.Equal(LineBreakMode.HeadTruncation.ToNative(), GetNativeLineBreakMode(handler));
            });
        }

        [Theory]
        [InlineData(LineBreakMode.HeadTruncation)]
        [InlineData(LineBreakMode.NoWrap)]
        public async Task UnsettingSingleLineBreakModeResetsMaxLines(LineBreakMode newMode)
        {
            var label = new LabelStub()
            {
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                MaxLines = 3,
                LineBreakMode = LineBreakMode.WordWrap,
            };

            var handler = await CreateHandlerAsync(label);
            var nativeLabel = GetNativeLabel(handler);

            await InvokeOnMainThreadAsync(() =>
            {
                Assert.Equal(3, GetNativeMaxLines(handler));
                Assert.Equal(LineBreakMode.WordWrap.ToNative(), GetNativeLineBreakMode(handler));

                label.LineBreakMode = newMode;
                nativeLabel.UpdateLineBreakMode(label);

                Assert.Equal(1, GetNativeMaxLines(handler));
                Assert.Equal(newMode.ToNative(), GetNativeLineBreakMode(handler));

                label.LineBreakMode = LineBreakMode.WordWrap;
                nativeLabel.UpdateLineBreakMode(label);

                Assert.Equal(3, GetNativeMaxLines(handler));
                Assert.Equal(LineBreakMode.WordWrap.ToNative(), GetNativeLineBreakMode(handler));
            });
        }
    }
}