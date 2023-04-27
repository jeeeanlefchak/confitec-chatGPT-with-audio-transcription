using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Speech.V1;
using Google.Cloud.TextToSpeech.V1;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using AudioChatGPT.Arguments;

namespace AudioChatGPT.Controllers
{
    [ApiController]
    [Route("AudioTransection")]
    public class AudioTransectionController : Controller
    {

        [HttpGet]
        public string get()
        {
            return JsonConvert.SerializeObject("sucessessss");
        }

        [HttpPost]
        public string Transcription([FromBody] string base64)
        {
            try
            {
                //AskRequest askRequest = new AskRequest();
                string m_Dirbk = System.IO.Path.GetTempPath();// @"C:\inetpub\wwwroot\ServicoAPI-Texto-Voz\";
                                                              //var data = post["ValueKing"];
                                                              // string base64 = "";
                var filePath = System.IO.Path.GetTempPath();
                if (!new DirectoryInfo(m_Dirbk).Exists)
                    new DirectoryInfo(m_Dirbk).Create();

                var m_DirAudio = m_Dirbk + "audio.raw";

                if (System.IO.File.Exists(m_DirAudio))
                    System.IO.File.Delete(m_DirAudio);


                if (base64 != null)
                {
                    // Converta a string base64 em um array de bytes
                    byte[] bytes = Convert.FromBase64String(base64);

                    // Escreva o array de bytes em um arquivo .raw
                    System.IO.File.WriteAllBytes(m_DirAudio, bytes);
                }


                var speech = SpeechClient.Create();
                var response = speech.Recognize(new RecognitionConfig()
                {
                    Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                    SampleRateHertz = 16000,
                    LanguageCode = "pt-BR",
                }, RecognitionAudio.FromFile(m_DirAudio));
                //}, RecognitionAudio.FromFile("C:\\Users\\Antônio Pantoja\\Music\\Gravando.m4a"));

                //var audio = RecognitionAudio.FromFile("C:\\Users\\Antônio Pantoja\\Music\\Gravando.m4a");

                try
                {
                    foreach (var result in response.Results)
                    {
                        foreach (var alternative in result.Alternatives)
                        {

                            //var client = new RestClient("http://45.230.200.186:5555/ask");
                            //client.Timeout = -1;
                            //var request = new RestRequest(Method.POST);
                            //request.AddHeader("Content-Type", "application/json");
                            //var body = @"{""Ask"": " + result.Alternatives + "} ";
                            //request.AddParameter("application/json", body, ParameterType.RequestBody);
                            //IRestResponse resposta = client.Execute(request);      
                            //return Json(resposta.Content);
                            return JsonConvert.SerializeObject(alternative.Transcript);
                        }
                    }
                    return JsonConvert.SerializeObject("");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }

                var retornoError = new
                {
                    StatusCode = 500,
                    StatusMessage = "ERRO no Serviço"
                };

                return JsonConvert.SerializeObject(retornoError);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
    }
}
