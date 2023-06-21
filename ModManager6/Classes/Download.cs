﻿using Fizzler;
using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModManager6.Classes
{
    public class Download
    {
        public static ModManager thisModManager;
        public static Boolean finished;
        public static string thisSentence;
        public static int thisOffset;
        public static int thisPercent;

        public static void load(ModManager modManager)
        {
            thisModManager = modManager;
        }

        public static async Task<String> downloadString(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Log.logError("ModManager", "Http Error", response.StatusCode.ToString());
                }
            }
            return "";
        }

        public static async Task<bool> downloadPath(string url, string path)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    File.Delete(path);
                    File.WriteAllText(path, result);
                    return true;
                }
                else
                {
                    Log.logError("ModManager", "Http Error", response.StatusCode.ToString());
                }
            }
            return false;
        }

        public static WebClient getClient()
        {
            WebClient client = new WebClient();
            client.Proxy = GlobalProxySelection.GetEmptyWebProxy();
            return client;
        }

        public static void download(string path, string destPath, string sentence, int offset, int percent)
        {
            finished = false;
            thisSentence = sentence;
            thisOffset = offset;
            thisPercent = percent;
            thisModManager.Invoke((MethodInvoker)delegate
            {
                if (!ModManager.silent)
                {
                    ModManagerUI.StatusLabel.Text = Translator.get(thisSentence).Replace("PERCENT", thisOffset.ToString() + "%");
                }
            });

            WebClient client = Download.getClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadFileAsync(new Uri(path), destPath);
            while (finished == false)
            {

            }
        }

        private static void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            thisModManager.Invoke((MethodInvoker)delegate {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = (bytesIn / totalBytes * thisPercent) + thisOffset;
                if (!ModManager.silent)
                {
                    string text = Translator.get(thisSentence).Replace("PERCENT", ((int)percentage).ToString() + "%");
                    if (ModManagerUI.StatusLabel.Text != text)
                    {
                        ModManagerUI.StatusLabel.Text = text;
                    }
                }
            });
        }
        private static void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            thisModManager.Invoke((MethodInvoker)delegate {
                if (!ModManager.silent)
                {
                    int currentPerc = thisOffset + thisPercent;
                    ModManagerUI.StatusLabel.Text = Translator.get(thisSentence).Replace("PERCENT", currentPerc.ToString() + "%");
                }
                finished = true;
            });
        }
    }
}
