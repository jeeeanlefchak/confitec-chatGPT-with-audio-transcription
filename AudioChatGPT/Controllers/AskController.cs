using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;
using AudioChatGPT.Arguments;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace AudioChatGPT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AskController : ControllerBase
    {


        public AskController()
        {
        }

        [HttpPost]
        public async Task<string> test([FromBody] AskRequest askRequest)
        {
            try
            {
                //HttpClient client = new HttpClient();
                ////https://platform.openai.com/docs/api-reference/authentication
                //client.DefaultRequestHeaders.Add("authorization", "Bearer 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"); //
                //client.DefaultRequestHeaders.Add("OpenAI-Organization", "org-IFGUPIYUYGonzgmWfFsQA0aI");

                //var content = new StringContent("{\"model\": \"text-davinci-001\", \"prompt\": \"" + askRequest.Message + "\",\"temperature\": 0.7,\"max_tokens\": 100}",
                //    Encoding.UTF8, "application/json");


                //HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/completions", content);

                //string responseString = await response.Content.ReadAsStringAsync();

                //try
                //{
                //    var dyData = JsonConvert.DeserializeObject<dynamic>(responseString);
                //    try
                //    {
                //        string guess = GuessCommand(dyData!.choices[0].text);
                //    }
                //    catch
                //    {
                //        throw new Exception(dyData);
                //    }

                //}
                //catch (Exception ex)
                //{
                //    throw new Exception(ex.Message);
                //}

                OpenAI_API.APIAuthentication.Default = new OpenAI_API.APIAuthentication("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");
                var api = new OpenAI_API.OpenAIAPI();

                var results = api.Chat.CreateChatCompletionAsync(new ChatRequest()
                {
                    Model = Model.ChatGPTTurbo,
                    Temperature = 0.7,
                    MaxTokens = 350,
                    Messages = new ChatMessage[] {
                    new ChatMessage(ChatMessageRole.User, askRequest.Ask)
                },

                }).Result;


                return JsonConvert.SerializeObject(results.Choices[0].Message.Content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static string GuessCommand(string raw)
        {
            //Console.WriteLine("---> GPT-3 API Returned Text:");
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine(raw);

            var lastIndex = raw.LastIndexOf('\n');

            string guess = raw.Substring(lastIndex + 1);

            //Console.ResetColor();

            //TextCopy.ClipboardService.SetText(guess);

            return guess;
        }
    }
}