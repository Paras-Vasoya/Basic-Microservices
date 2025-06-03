namespace UserAPI.Services.Dto
{
    public class ResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic? Data { get; set; }

        public ResponseDto(bool success, string message, dynamic? data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
