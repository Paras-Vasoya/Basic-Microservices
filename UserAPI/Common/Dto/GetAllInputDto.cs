namespace UserAPI.Common.Dto
{
    public class GetAllInputDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public bool? IsActive { get; set; }
    }
}
