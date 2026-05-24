namespace Common.Responses
{
    public class CustomResponse
    {
        public bool IsSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }// = Enumerable.Empty<string>();
        public IEnumerable<string> SuccessMessage { get; set; }// = Enumerable.Empty<string>();

        protected CustomResponse(
            bool isSuccessful,
            IEnumerable<string>? errors = null,
            IEnumerable<string>? successMessage = null)
        {
            IsSuccessful = isSuccessful;
            Errors = errors ?? [];
            SuccessMessage = successMessage ?? [];
        }

        public static CustomResponse Success(params string[] successMessage)
            => new CustomResponse(true, null, successMessage);
        public static CustomResponse Fail(params string[] errors)
            => new CustomResponse(false, errors);
        public static CustomResponse Fail(IEnumerable<string> errors)
            => new CustomResponse(false, errors);
    }
    public class CustomResponse<TResult> : CustomResponse
    {
        public TResult? Result { get; private set; }

        private CustomResponse(
            TResult? result,
            IEnumerable<string>? successMessages = null)
            : base(true, null, successMessages)
        {
            Result = result;
        }
        private CustomResponse(
            IEnumerable<string> errors,
            IEnumerable<string>? successMessage = null)
            : base(false, errors, successMessage)
        {
            Result = default;
        }

        public static CustomResponse<TResult> Success(TResult result, params string[] successMessages)
            => new CustomResponse<TResult>(result, successMessages);

        public static new CustomResponse<TResult> Fail(params string[] errors)
            => new CustomResponse<TResult>(errors);

        public static new CustomResponse<TResult> Fail(IEnumerable<string> errors)
            => new CustomResponse<TResult>(errors);

        public static CustomResponse<TResult> CreateFailed(IEnumerable<string> errors, IEnumerable<string>? successMessages = null)
        {
            return new CustomResponse<TResult>(errors, successMessages);
        }
    }
    public static class CustomResponseFactory
    {
        public static CustomResponse<List<T>> FromList<T>(IEnumerable<T>? data, string errorMessageIfEmpty)
        {
            if (data == null || !data.Any())
            {
                return CustomResponse<List<T>>.Fail(errorMessageIfEmpty);
            }
            return CustomResponse<List<T>>.Success(data.ToList());
        }

        //public static CustomResponse<PaginatedResult<T>> FromPagedResult<T>(
        //    PaginatedResult<T> paginatedData,
        //    string errorMessageIfEmpty = "No data found.")
        //{
        //    if (paginatedData == null || !paginatedData.Items.Any())
        //    {
        //        return CustomResponse<PaginatedResult<T>>.Fail(errorMessageIfEmpty);
        //    }

        //    return CustomResponse<PaginatedResult<T>>.Success(paginatedData);
        //}
    }
}
