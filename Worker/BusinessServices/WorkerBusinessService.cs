using Newtonsoft.Json;
using TaskExecutor.Models;

namespace Worker.BusinessServices
{
    public class MemeResponse
    {
        public string url { get; set; }
        public bool nsfw { get; set; }
    }

    public class WorkerBusinessService
    {
        private readonly string _allocatorUri;

        public WorkerBusinessService(IConfiguration configuration)
        {
            _allocatorUri = configuration.GetValue<string>("AllocatorUri");
        }

        private async Task UpdateTaskStatusAsync(string taskId, bool isCompleted)
        {
            if (_allocatorUri != null)
            {
                using (HttpClient client = new HttpClient())
                {
                    string status = isCompleted ? "mark-completed" : "mark-failed";
                    string url = $"{_allocatorUri}/api/task/{status}";
                    Console.WriteLine(url);
                    var response = await client.PostAsJsonAsync(url, taskId);
                    Console.WriteLine(response);
                }
            }
        }

        public async Task ExecuteTaskAsync(Work task)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://meme-api.com/gimme/wholesomememes");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var memeData = JsonConvert.DeserializeObject<MemeResponse>(json);
                    if (memeData?.nsfw ?? true)
                    {
                        Console.WriteLine("Error: NSFW content not allowed.");
                        await UpdateTaskStatusAsync(task.Id.ToString(), false);
                    }
                    else
                    {
                        await DownloadMeme(memeData.url);
                        await UpdateTaskStatusAsync(task.Id.ToString(), true);
                    }
                }
                else
                {
                    Console.WriteLine($"Error: Unable to fetch meme data. Status code: {response.StatusCode}");
                    await UpdateTaskStatusAsync(task.Id.ToString(), false);
                }
            }
        }

        private async Task DownloadMeme(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string imageFileName = imageUrl.Substring(imageUrl.LastIndexOf('/') + 1);
                    using (HttpResponseMessage imageResponse = await client.GetAsync(imageUrl))
                    using (var imageStream = await imageResponse.Content.ReadAsStreamAsync())
                    using (var fileStream = System.IO.File.Create(imageFileName))
                    {
                        imageStream.CopyTo(fileStream);
                    }
                    Console.WriteLine($"Image downloaded and saved as {imageFileName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}