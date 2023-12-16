using CetContact.Model;

namespace CetContact.Views;

public partial class ContactsPage : ContentPage
{
    ContactRepository contactRepository;
	public ContactsPage()
	{
		InitializeComponent();
		//List<string> contacts2 = new List<string> {"Hüseyin Şimşek", "Ufuk Adanır" ,"Selim Tetik" , "Talha Zengin"};

		//List<ContactInfo> contacts = new List<ContactInfo>() { 
		//	new ContactInfo {
		//              Id=1,
		//		Name = "Hüseyin Şimşek", 
		//		Email="huseyin.simsek@boun.edu.tr",
		//		Phone="905333003030",
		//		Address=""
		//		},
		//          new ContactInfo {
		//              Id=2,
		//              Name = "Ufuk Adanır",
		//              Email="ufuk.adanir@gmail.com",
		//              Phone="905333004040",
		//              Address=""
		//              }
		//      };
		contactRepository = new ContactRepository();
		
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var contacts =await contactRepository.GetAllContacts();
        ContactsList.ItemsSource = contacts;

    }
    private void AddContactButton_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync(nameof(AddContactPage));
       
    }

   

    private void ContactsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		if(ContactsList.SelectedItem != null)
		{
            
			ContactInfo selectedContact= ContactsList.SelectedItem as ContactInfo;
            int selectedID= selectedContact.Id; 

           // DisplayAlert("You Clicked", $"//{nameof(EditContactPage)}?id={selectedID}", "ok");
            Shell.Current.GoToAsync($"{nameof(EditContactPage)}?id={selectedID}"); //EditContactPage?id=2

            ContactsList.SelectedItem = null;


        }
    }

	
}