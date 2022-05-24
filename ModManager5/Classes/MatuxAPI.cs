﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModManager5.Classes
{
    public static class MatuxAPI
    {
        public static string token;
        public static Boolean logged;
        private static string path = ModManager.appDataPath + @"\token.txt";
        public static List<Mod> myMods;
        public static Account myAccount;
        public static string currentLg;

        public static Boolean Login(string login, string password)
        {
            Utils.log("Login START", "MatuxAPI");
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["login"] = login;
                values["password"] = password;

                try
                {
                    Utils.log("Login YES", "MatuxAPI");
                    var response = client.UploadValues("https://api.matux.fr/account/login", values);
                    var responseString = Encoding.Default.GetString(response);
                    token = responseString;
                    validLogin();
                    Update();
                    return true;
                }
                catch (WebException e)
                {
                    Utils.log("Login NO", "MatuxAPI");
                    return false;
                }
            }
            Utils.log("Login END", "MatuxAPI");
        }

        public static Boolean Register(string login, string password)
        {
            Utils.log("Register START", "MatuxAPI");
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["login"] = login;
                values["password"] = password;

                try
                {
                    var response = client.UploadValues("https://api.matux.fr/account/register", values);
                    var responseString = Encoding.Default.GetString(response);
                    token = responseString;
                    validLogin();
                    Update();
                    Utils.log("Register YES", "MatuxAPI");
                    return true;
                }
                catch (WebException e)
                {
                    Utils.log("Register NO", "MatuxAPI");
                    return false;
                }
            }
            Utils.log("Register END", "MatuxAPI");
        }

        public static void logout()
        {
            Utils.log("Logout START", "MatuxAPI");
            token = "";
            logged = false;
            myMods = new List<Mod>() { };
            Update();
            Utils.log("Logout END", "MatuxAPI");
            return;
        }

        public static Boolean isModder()
        {
            return myAccount.admin == "1" || myAccount.modder == "1";
        }

        public static Boolean isTranslator()
        {
            return myAccount.admin == "1" || myAccount.translator == "1";
        }

        public static List<Translation> getTranslationsForLg(string lg)
        {
            return Translator.languages.Find(l => l.code == lg).translations;
        }

        public static void updateTranslation(string code, string original, string translation)
        {
            if (token == "")
            {
                return;
            }
            Utils.log("Update translation START", "MatuxAPI");

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["token"] = token;
                values["lg"] = code;
                values["original"] = original;
                values["translation"] = translation;

                try
                {
                    Utils.log("Update translation YES", "MatuxAPI");
                    var response = client.UploadValues("https://api.matux.fr/translation/update", values);
                    var responseString = Encoding.Default.GetString(response);
                    return;
                }
                catch (WebException e)
                {
                    Utils.log("Update translation NO", "MatuxAPI");
                    return;
                }
            }
            Utils.log("Update translation END", "MatuxAPI");
        }

        public static void getMyAccount()
        {
            if (token == "")
            {
                return;
            }

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["token"] = token;

                try
                {
                    var response = client.UploadValues("https://api.matux.fr/account/myAccount", values);
                    var responseString = Encoding.Default.GetString(response);
                    myAccount = Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(responseString);
                    return;
                }
                catch (WebException e)
                {
                    return;
                }
            }
        }

        public static void getMyMods()
        {
            if (token == "")
            {
                return;
            }

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["token"] = token;

                try
                {
                    var response = client.UploadValues("https://api.matux.fr/account/myMods", values);
                    var responseString = Encoding.Default.GetString(response);
                    myMods = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Mod>>(responseString);
                    return;
                }
                catch (WebException e)
                {
                    return;
                }
            }
        }

        private static string getLogin()
        {
            if (token == "")
            {
                return "";
            }

            return token.Substring(0, token.IndexOf("&"));
        }

        public static void CheckLogin()
        {
            if (!File.Exists(path))
            {
                return;
            }

            token = File.ReadAllText(path);

            if (token == "")
            {
                logged = false;
                return;
            }

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["token"] = token;

                try
                {
                    var response = client.UploadValues("https://api.matux.fr/account/isLoggedIn", values);
                    var responseString = Encoding.Default.GetString(response);
                    validLogin();
                    return;
                }
                catch (WebException e)
                {
                    logged = false;
                    return;
                }
            }
        }

        private static void validLogin()
        {
            logged = true;
            getMyMods();
            getMyAccount();
        }

        public static void Update()
        {
            File.WriteAllText(path, token);
        }

    }
}
