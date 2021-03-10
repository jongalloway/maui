namespace Microsoft.Maui
{
	/// <summary>
	/// Represents a View that inputs a linear value.
	/// </summary>
	public interface ISlider : IView
	{
		/// <summary>
		/// Gets or sets the minimum selectable value for the Slider.
		/// </summary>
		double Minimum { get; }

		/// <summary>
		/// Gets or sets the maximum selectable value for the Slider.
		/// </summary>
		double Maximum { get; }

		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		double Value { get; set; }

		/// <summary>
		/// Gets or sets the color of the portion of the slider track that contains the minimum value of the slider.
		/// </summary>
		Color MinimumTrackColor { get; }

		/// <summary>
		/// Gets or sets the color of the portion of the slider track that contains the maximum value of the slider.
		/// </summary>
		Color MaximumTrackColor { get; }

		/// <summary>
		/// Gets or sets the color of the slider thumb button.
		/// </summary>
		Color ThumbColor { get; }

		/// <summary>
		/// Notify when drag starts.
		/// </summary>
		void DragStarted();

		/// <summary>
		/// Notify when drag is completed.
		/// </summary>
		void DragCompleted();
	}
}