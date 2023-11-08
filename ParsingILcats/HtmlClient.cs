namespace ParsingILcats
{
    public class HtmlClient
    {
        private int _requestCount;
        private HttpClient _httpClient;

        public HtmlClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<string> GetHtmlContent(string url)
        {
            _requestCount++;

            return _httpClient.GetStringAsync(url);
         }

        public void ShowRequestCount()
        {
            Console.WriteLine($"Number of requests: {_requestCount}");
        }

        public async Task SaveImage(string imageUrl, string imageName)
        {
            var data = await _httpClient.GetByteArrayAsync($"https:{imageUrl}");
            string path = @$"C:\Users\Admin\Documents\Visual Studio Projects\ParsingILcats\ParsingILcats\Images\{imageName}.jpg";

            await File.WriteAllBytesAsync(path, data);
        }
    }
}