
using Microsoft.Maui.ApplicationModel.Communication;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CetContact.Model

{
    public class ContactRepository
    {
        private SQLiteAsyncConnection database;
        private string databaseName = "contacts2.db3";
        


        public ContactRepository() {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, databaseName);
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ContactInfo>(CreateFlags.AllImplicit | CreateFlags.AutoIncPK).GetAwaiter().GetResult();
            contacts = new List<ContactInfo>();
        }
       
       
        public async Task<List<ContactInfo>> GetAllContacts () { 
            return await database.Table<ContactInfo>().ToListAsync();
        }
        public async Task AddContact (ContactInfo contact)
        {
            await database.InsertAsync(contact);
        
        }

        public async Task<ContactInfo> GetContactById(int Id)
        {
            var contact = await database.Table<ContactInfo>().Where(c => c.Id == Id).FirstOrDefaultAsync();
            return contact;
        }

        public async Task Update(ContactInfo contact)
        {
            await database.UpdateAsync(contact);
        }

        private List<ContactInfo> contacts; // Assuming contacts is a list



        // Other members and methods...

        internal Task<bool> Delete(ContactInfo contactInfo)
        {
            try
            {
                // Find the contact in the list
                var contactToDelete = contacts.FirstOrDefault(c => c.Id == contactInfo.Id);

                if (contactToDelete != null)
                {
                    // Remove the contact from the list
                    contacts.Remove(contactToDelete);
                    // You may want to save changes to a database here

                    return Task.FromResult(true); // Deletion successful
                }
                else
                {
                    return Task.FromResult(false); // Contact not found
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (log, display error message, etc.)
                Console.WriteLine($"Error deleting contact: {ex.Message}");
                return Task.FromResult(false);
            }
        }

        // Other members and methods...
    }
}

    