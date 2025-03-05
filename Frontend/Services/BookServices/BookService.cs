using Library.Mvc.Dtos;
using Library.Mvc.Dtos.BookDtos;

namespace Library.Mvc.Services.BookServices;
    public class BookService : IBookService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResponse<List<BookDto>>> GetAvailableBooks()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client
                .GetFromJsonAsync<ApiResponse<List<BookDto>>>("http://localhost:5097/api/Books/GetAvailableBooks");

            if(response is null)
            {
                throw new Exception("api response is null");
            }

            return response;
        }

        public async Task<ApiResponse<List<BookDto>>> GetBooksByTitle()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client
                .GetFromJsonAsync<ApiResponse<List<BookDto>>>("http://localhost:5097/api/Books/SearchBookByName");
            if(response is null)
            {
                throw new FileNotFoundException("api response is null");
            }

            return response;
        }
    }
