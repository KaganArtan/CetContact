using CetContact.Model;
using Microsoft.Maui.ApplicationModel.Communication;

namespace CetContact.Views;

[QueryProperty(nameof(ContactId),"id")]
public partial class EditContactPage : ContentPage
{
    ContactInfo contactInfo;
    ContactRepository contactRepository;
    public EditContactPage()
	{
		InitializeComponent();
        contactRepository = new ContactRepository();
	}

    public string ContactId { get; set; }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        contactInfo = await contactRepository.GetContactById(Int32.Parse(ContactId));
        if (contactInfo != null)
        {

            NameEntry.Text = contactInfo.Name;
            PhoneEntry.Text = contactInfo.Phone;
            EmailEntry.Text = contactInfo.Email;
            AdressEntry.Text = contactInfo.Address;
        }
        else
        {
           await DisplayAlert("Hata", "Kişi Bulunamadı", "Tamam");
        }
    }
   
    private  async void SaveButton_Clicked(object sender, EventArgs e)
    {

        contactInfo.Name = NameEntry.Text;
        contactInfo.Phone = PhoneEntry.Text;
        contactInfo.Address = AdressEntry.Text;
        contactInfo.Email = EmailEntry.Text;
       
        await contactRepository.Update(contactInfo);
        await Shell.Current.GoToAsync("..");

    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteContactButton_Clicked(object sender, EventArgs e)
    {
        var ans = await Application.Current.MainPage.DisplayAlert("Uyarı", "Kişiyi silmek istediğinize emin misiniz?", "Evet", "Hayır");
        var contactToDelete = contactInfo;
       
        if (ans)
        {
            await contactRepository.Delete(contactInfo);
            await DisplayAlert("Bilgi", "Kişi başarıyla silindi", "Tamam");

            await Shell.Current.Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Bilgi", "Kişi silme iptal edildi", "Tamam");
        }
    }

}