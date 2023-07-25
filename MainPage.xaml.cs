using Android.OS;
using Android.Widget;
using CommunityToolkit.Maui.Alerts;
using System.Diagnostics;
using Debug = System.Diagnostics.Debug;
using Toast = CommunityToolkit.Maui.Alerts.Toast;

namespace ColorPicker;

public partial class MainPage : ContentPage
{
	bool isRandom = true;

    //will be used to store the hex value to be copied to the clipboard
    string hexValue;

	public MainPage()
	{
		InitializeComponent();
	}

	//event handler to handle the colors
	private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
	{
		if (isRandom is false)
		{
            var red = redSlider.Value;
            var green = greenSlider.Value;
            var blue = blueSlider.Value;

            var color = Color.FromRgb(red: red, green: green, blue: blue);

            //set modify the colors of the controls we want to modify
            SetColor(color);
        }
       
    }

	//method to modify the colors of the controls we want to modify
	private void SetColor(Color color)
	{
		randomButton.BackgroundColor = color;
		Container.BackgroundColor = color;

		//set the value of the text to appear on the button and the one to be copied to the clipboard
		hexValue = color.ToHex();

		hexControl.Text = hexValue;

		//copyButton.BackgroundColor = color;

		Debug.WriteLine(hexValue);
	}

	//method to generate a random color using the 'Generate Random Color' button
	private void randomButton_Clicked(object sender, EventArgs e)
	{
		isRandom = true;

		var random = new Random();

		// Generate random colors
		Color color = Color.FromRgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

		redSlider.Value = color.Red;
		greenSlider.Value = color.Green;
		blueSlider.Value = color.Blue;

        //set modify the colors of the controls we want to modify
        SetColor(color);

		isRandom = false;
	}

	private async void ImageButton_Clicked(object sender, EventArgs e)
	{
		// copy text to clipboard when the user clicks the 'copy' image
		await Clipboard.SetTextAsync(hexValue);

		// Show an alert specifying that the text has been copied
		var toast = Toast.Make("Color Code copied", CommunityToolkit.Maui.Core.ToastDuration.Short, 12);

		toast.Show();

	}
}

