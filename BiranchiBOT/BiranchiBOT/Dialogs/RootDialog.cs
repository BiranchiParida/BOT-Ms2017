using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

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
            if (activity.Text.Contains("morning"))
            {
                await context.PostAsync("Good Morning , Have a nice Day");
            }
            //test 
            else if (activity.Text.Contains("night"))
            {
                await context.PostAsync("Good night and Sweetest Dreams");
            }
            else if (activity.Text.Contains("who are you"))
            {
                await context.PostAsync("I am a Bot created by Biranch don");
            }
            else if (activity.Text.Contains("date"))
            {
                await context.PostAsync(DateTime.Now.ToString());
            }
            else
            {
                await context.PostAsync($"You sent {activity.Text} which was {length} characters");
            }
            context.Wait(MessageReceivedAsync);
        }
    }
}