using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;

namespace App4.Models
{
    internal class ConexionFirebase
    {
        public static FirebaseAuthClient ConectarFirebase()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyBeoUbHVKGJKqZXBY6vCoGf1oJWbrvzWIU ",
                AuthDomain = "crud-bootstrap-ab39a.web.app",
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider()
                }
            };

            var client = new FirebaseAuthClient(config);
            return client;
        }

        public async Task<UserCredential> CargarUsuario(string Email, string Password)
        {
            var cliente = ConectarFirebase();
            var userCredential = await cliente.SignInWithEmailAndPasswordAsync(Email, Password);

            return userCredential;
        }

        public async Task<UserCredential> CrearUsuario(string Email, string Password, string Nombre)
        {
            var cliente = ConectarFirebase();
            var userCredential = await cliente.CreateUserWithEmailAndPasswordAsync(Email, Password, Nombre);
            return userCredential;
        }
    }
}
