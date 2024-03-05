using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Frontend.Models;
using Frontend.Utils;
using Newtonsoft.Json.Linq;
namespace Frontend.Services
{
    public class CommentService
    {
        private readonly string _baseURL;
        private readonly HttpClient _client;
        private readonly TokenServices _tokenServices;
        private readonly PopUpMessages _popUpMessages;

        public CommentService(TokenServices tokenServices,PopUpMessages popUpMessages)
        {
            _client = new HttpClient();
            _baseURL = "http://localhost:5215/api/v1/comments";
            _tokenServices = tokenServices;
            _popUpMessages=popUpMessages;
        }

        public async Task AddComment(int eventId, string userComment)
        {
            var userCommentData = new { pictureId = eventId, comment = userComment };
           await _tokenServices.AddTokenToRequest(_client);
            var jsonContent = new StringContent(JsonSerializer.Serialize(userCommentData), Encoding.UTF8, "application/json");
           var response= await _client.PostAsync(_baseURL, jsonContent);
           if(response.IsSuccessStatusCode)
           {
            await _popUpMessages.sweetAlert("Comment Added Successfully","Event","success");

           }else
           {
            await _popUpMessages.sweetAlert("Failed to Add comment...Try again later","Event","error");
           }
        }

        public async Task<List<CommentModel>> GetComments(int id)
        {
          var response= await _client.GetAsync(_baseURL+"/"+id);
            if(response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

                
                Console.WriteLine("nfsd dddddvf");
                string jsonString=await response.Content.ReadAsStringAsync();
            //    JsonObject jsonObject=JObject.Parse(jsonString);

                ResponseDTO responseDTO=JsonSerializer.Deserialize<ResponseDTO>(jsonString,options);
                Console.WriteLine("my data is here");
                
                List<CommentModel> comments=JsonSerializer.Deserialize<List<CommentModel>>(responseDTO.responseData.ToString(),options);
                // foreach(var comment in comments)
                // {
                //     Console.WriteLine(comment.Id);
                // }

                return comments;
                
            
                            
                 await _popUpMessages.sweetAlert("All comments got successfully","Comments","success");
            }else{
                return null;
               await _popUpMessages.sweetAlert("Failed to Fetch Comments","Comments","error");
            }
            
        }
    }
}
