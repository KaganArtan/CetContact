using CetContact.Model;
//using Contacts;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Xml.Linq;

namespace CetContact.Views;

public partial class AddContactPage : ContentPage
{
    ContactRepository contactRepository;
	public AddContactPage()
	{
		InitializeComponent();
        contactRepository = new ContactRepository();
	}

    private void BackButton_Clicked(object sender, EventArgs e)
    {
		//Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
        //Shell.Current.GoToAsync("//"+nameof(ContactsPage));
       
        Shell.Current.GoToAsync("..");

    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {

        ContactInfo contact = new ContactInfo
        {
            Name = NameEntry.Text,
            Phone = PhoneEntry.Text,
            Address = AdressEntry.Text,
            Email = EmailEntry.Text,
        };

        if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            await DisplayAlert("UYARI", "Isim ve e-posta bos olamaz.", "Tamam");
            return;
        }

        await contactRepository.AddContact(contact);
        await Shell.Current.GoToAsync("..");
    }



   
}