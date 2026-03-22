namespace MostWanted;

public partial class TestPage2 : ContentPage
{
	private int count;
	public TestPage2()
	{
		InitializeComponent();
	}


    private void Button_Clicked(object sender, EventArgs e)
    {

        count++;
        LblCounter.Text = $"Thank For Clicking {count} times.";

        SemanticScreenReader.Announce(LblCounter.Text); // This will update the LblCounter area on the page

    }


}