using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traccine.Models;

namespace traccine.Helpers
{
    public class FirbaseDataBaseHelper
    {
       public FirebaseClient firebase { get; set; }
        public FirbaseDataBaseHelper()
        {
          firebase = new FirebaseClient("https://traccine.firebaseio.com/");

        }
        public async Task<List<UserProfile>> GetAllPersons()
        {

            return (await firebase
              .Child("UserProfile")
              .OnceAsync<UserProfile>()).Select(item => new UserProfile
              {
                  Name = item.Object.Name,
                  Email = item.Object.Email,
                  Picture = item.Object.Picture,
                  Id = item.Object.Id,
                  IsInfected = item.Object.IsInfected,
                  PhoneNumber = item.Object.PhoneNumber,
                  FcmToken = item.Object.FcmToken
              }).ToList();
        }

        public async Task AddPerson(string personId, string name,string email,string phonenumber,Uri picture)
        {

            await firebase
              .Child("UserProfile")
              .PostAsync(new UserProfile() { Id = personId, Name = name,Email=email,PhoneNumber=phonenumber,Picture=picture });
        }

        public async Task<UserProfile> GetPerson(string email)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("UserProfile")
              .OnceAsync<UserProfile>();
            return allPersons.Where(a => a.Email == email).FirstOrDefault();
        }
        public async Task<UserProfile> GetPersonByID(string id)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("UserProfile")
              .OnceAsync<UserProfile>();
            return allPersons.Where(a => a.Id == id).FirstOrDefault();
        }

        public async Task UpdatePerson(string id, string phonenumber)
        {
            var toUpdatePerson = (await firebase
              .Child("UserProfile")
              .OnceAsync<UserProfile>()).Where(a => a.Object.Id == id).FirstOrDefault();

            await firebase
              .Child("UserProfile")
              .Child(toUpdatePerson.Key)
              .PutAsync(new UserProfile() { Email = toUpdatePerson.Object.Email,
                  Id = toUpdatePerson.Object.Id,
                  Name=toUpdatePerson.Object.Name,
                  Picture=toUpdatePerson.Object.Picture,
                  IsInfected = toUpdatePerson.Object.IsInfected,
                  FcmToken = toUpdatePerson.Object.FcmToken,
                  PhoneNumber = phonenumber }) ;
        }
        public async Task Updateisinfected(string id, Boolean isinfected)
        {
            var toUpdatePerson = (await firebase
              .Child("UserProfile")
              .OnceAsync<UserProfile>()).Where(a => a.Object.Id == id).FirstOrDefault();

            await firebase
              .Child("UserProfile")
              .Child(toUpdatePerson.Key)
              .PutAsync(new UserProfile()
              {
                  Email = toUpdatePerson.Object.Email,
                  Id = toUpdatePerson.Object.Id,
                  Name = toUpdatePerson.Object.Name,
                  Picture = toUpdatePerson.Object.Picture,
                  IsInfected = isinfected,
                  FcmToken = toUpdatePerson.Object.FcmToken,
                  PhoneNumber = toUpdatePerson.Object.PhoneNumber
              });
        }
        public async Task UpdateFcmToken(string id, string FcmToken)
        {
            var toUpdatePerson = (await firebase
              .Child("UserProfile")
              .OnceAsync<UserProfile>()).Where(a => a.Object.Id == id).FirstOrDefault();

            await firebase
              .Child("UserProfile")
              .Child(toUpdatePerson.Key)
              .PutAsync(new UserProfile()
              {
                  Email = toUpdatePerson.Object.Email,
                  Id = toUpdatePerson.Object.Id,
                  Name = toUpdatePerson.Object.Name,
                  Picture = toUpdatePerson.Object.Picture,
                  PhoneNumber = toUpdatePerson.Object.PhoneNumber,
                  IsInfected = toUpdatePerson.Object.IsInfected,
                  FcmToken = FcmToken
              });
        }

        public async Task DeletePerson(string personId)
        {
            var toDeletePerson = (await firebase
              .Child("UserProfile")
              .OnceAsync<UserProfile>()).Where(a => a.Object.Id == personId).FirstOrDefault();
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}
