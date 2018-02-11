using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace BiranchiBOT.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        //private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        //{
        //    var activity = await result as Activity;

        //    // calculate something for us to return
        //    int length = (activity.Text ?? string.Empty).Length;

        //    // return our reply to the user
        //    await context.PostAsync($"You sent {activity.Text} which was {length} characters");

        //    context.Wait(MessageReceivedAsync);
        //}
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            // calculate something for us to return 
            int length = (activity.Text ?? string.Empty).Length;
            // return our reply to the user 
            //test 
            //if (activity.Text.Contains("morning"))
            //{
            //    await context.PostAsync("Good Morning , Have a nice Day");
            //}
            ////test 
            //else if (activity.Text.Contains("night"))
            //{
            //    await context.PostAsync("Good night and Sweetest Dreams");
            //}
            //else if (activity.Text.Contains("who are you"))
            //{
            //    await context.PostAsync("I am a Bot created by Biranch don");
            //}
            //else if (activity.Text.Contains("date"))
            //{
            //    await context.PostAsync(DateTime.Now.ToString());
            //}
            //else
            //{
                string meaning = await ConsumeWordFromDictonaryAsync(activity.Text);
                meaning= meaning==string.Empty?"Not Found Please try one more time":meaning;
                await context.PostAsync($"Your Enter word meaning is:\n" + meaning);
                //await context.PostAsync($"You sent {activity.Text} which was {length} characters");
           // }
            context.Wait(MessageReceivedAsync);
        }
         private async Task<string> ConsumeWordFromDictonaryAsync(string word)
        {
           
            string Meaning = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("	https://od-api.oxforddictionaries.com/api/v1/entries/en/");
                    client.DefaultRequestHeaders.Add("app_id", "b2f09ffb");
                    client.DefaultRequestHeaders.Add("app_key", "b5738c265830fb5873601113d7e181b4");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method  
                    HttpResponseMessage response = await client.GetAsync(word);
                    if (response.IsSuccessStatusCode)
                    {
                        RootObject rootObject  = await response.Content.ReadAsAsync<RootObject>();
                         Meaning = rootObject.results[0].lexicalEntries[0].entries[0].senses[0].definitions[0];

                    }

                }
            }
            catch (Exception ex)
            {

            }
            return Meaning;
        }
        public class Metadata
        {
            public string provider { get; set; }
        }

        public class Derivative
        {
            public string id { get; set; }
            public string text { get; set; }
        }

        public class GrammaticalFeature
        {
            public string text { get; set; }
            public string type { get; set; }
        }

        public class Note
        {
            public string text { get; set; }
            public string type { get; set; }
        }

        public class Example
        {
            public string text { get; set; }
        }

        public class Example2
        {
            public string text { get; set; }
        }

        public class CrossReference
        {
            public string id { get; set; }
            public string text { get; set; }
            public string type { get; set; }
        }

        public class Subsens
        {
            public List<string> definitions { get; set; }
            public List<Example2> examples { get; set; }
            public string id { get; set; }
            public List<string> registers { get; set; }
            public List<string> domains { get; set; }
            public List<string> crossReferenceMarkers { get; set; }
            public List<CrossReference> crossReferences { get; set; }
        }

        public class Note2
        {
            public string text { get; set; }
            public string type { get; set; }
        }

        public class Sens
        {
            public List<string> definitions { get; set; }
            public List<Example> examples { get; set; }
            public string id { get; set; }
            public List<Subsens> subsenses { get; set; }
            public List<string> domains { get; set; }
            public List<Note2> notes { get; set; }
            public List<string> regions { get; set; }
        }

        public class Entry
        {
            public List<string> etymologies { get; set; }
            public List<GrammaticalFeature> grammaticalFeatures { get; set; }
            public string homographNumber { get; set; }
            public List<Note> notes { get; set; }
            public List<Sens> senses { get; set; }
        }

        public class Pronunciation
        {
            public string audioFile { get; set; }
            public List<string> dialects { get; set; }
            public string phoneticNotation { get; set; }
            public string phoneticSpelling { get; set; }
        }

        public class LexicalEntry
        {
            public List<Derivative> derivatives { get; set; }
            public List<Entry> entries { get; set; }
            public string language { get; set; }
            public string lexicalCategory { get; set; }
            public List<Pronunciation> pronunciations { get; set; }
            public string text { get; set; }
        }

        public class Result
        {
            public string id { get; set; }
            public string language { get; set; }
            public List<LexicalEntry> lexicalEntries { get; set; }
            public string type { get; set; }
            public string word { get; set; }
        }

        public class RootObject
        {
            public Metadata metadata { get; set; }
            public List<Result> results { get; set; }
        }
    }
}