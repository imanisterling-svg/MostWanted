namespace MostWanted;

public class TestPage : ContentPage 
{
    //Place your varables within he main public class
    int count = 0;
    Label LblCounter;
    public TestPage()  //This is a constructor  
    {


        //Content = new StackLayout

        //{
        //    Children = { 
        //        new Label { Text="Welcome the the MOST WANTED APP" }
            
        //    }
        
        
        //};

        var scrollView = new ScrollView(); // This is the scroll view within the screen of the app it facilitate the content scroll capabilities

        var stackLayout = new StackLayout(); // creating a stacklayout to display we are initializing a StackLayout 

        scrollView.Content = stackLayout;//here i will load the content of the page

        //building out my controller

        LblCounter = new Label()
        {
            Text = "Count: 0",
            FontSize=44,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions=LayoutOptions.Center,
        };

        stackLayout.Children.Add(LblCounter);

        var btnCounter = new Button
        {
            Text = "Click To Count",
            HorizontalOptions = LayoutOptions.Center,
        };
        
        stackLayout.Children.Add(btnCounter);  // Childeren the makking reference to the Test Page  constructor
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        btnCounter.Clicked += OnClickedEvent;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).


        this.Content = scrollView; // this.content is making reference to the test page

	

	}
    //This is creating the function for the OnClick
    private void OnClickedEvent(object sender,EventArgs e) {

        count++;
        LblCounter.Text = $"Thank For Clicking { count} times.";

        SemanticScreenReader.Announce(LblCounter.Text); // This will update the LblCounter area on the page
   
    }
}